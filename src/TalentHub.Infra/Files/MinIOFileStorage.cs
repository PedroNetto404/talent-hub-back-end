using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.Files;

public sealed class MinIoFileStorage : IFileStorage
{
    private readonly IAmazonS3 _amazonS3;
    private readonly string _serviceUrl;

    public MinIoFileStorage()
    {
        var config = new AmazonS3Config
        {
            ServiceURL = "http://localhost:9000",
            ForcePathStyle = true
        };

        _amazonS3 = new AmazonS3Client(
            "talent-hub_admin",
            "talent-hub_admin_11231233",
            config);

        _serviceUrl = config.ServiceURL;
    }

    public async Task DeleteAsync(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        if (!await BucketExistsAsync(bucket, cancellationToken))
        {
            return;
        }

        await _amazonS3.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = bucket,
            Key = fileName
        }, cancellationToken);
    }

    public void DeleteAsync(string userProfilePicture, object profilePictureFileName)
    {
        throw new NotImplementedException();
    }


    public async Task<Stream> GetAsync(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        if (!await BucketExistsAsync(bucket, cancellationToken))
        {
            throw new FileNotFoundException($"Bucket '{bucket}' not found.");
        }

        GetObjectResponse? obj = await _amazonS3.GetObjectAsync(new GetObjectRequest
        {
            BucketName = bucket,
            Key = fileName
        }, cancellationToken);

        return obj.ResponseStream;
    }

    public async Task<string> SaveAsync(
        string bucket,
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if (!await BucketExistsAsync(bucket, cancellationToken))
        {
            await CreateBucketAsync(bucket, cancellationToken);
        }

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = bucket,
            InputStream = fileStream,
            ContentType = contentType,
            Key = fileName,
            AutoCloseStream = true,
            CannedACL = S3CannedACL.PublicRead
        };

        PutObjectResponse? response = await _amazonS3.PutObjectAsync(putObjectRequest, cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Failed to save file '{fileName}' to bucket '{bucket}'.");
        }

        return $"{_serviceUrl}{bucket}/{fileName}";
    }

    private async Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken)
    {
        ListBucketsResponse? response = await _amazonS3.ListBucketsAsync(cancellationToken);
        return response.Buckets.Any(bucket => bucket.BucketName == bucketName);
    }

    private async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        await _amazonS3.PutBucketAsync(new PutBucketRequest
        {
            BucketName = bucketName,
            CannedACL = S3CannedACL.PublicRead
        }, cancellationToken);

        string bucketPolicy = $$"""
                                {
                                            "Version": "2012-10-17",
                                            "Statement": [
                                                {
                                                    "Effect": "Allow",
                                                    "Principal": "*",
                                                    "Action": "s3:GetObject",
                                                    "Resource": "arn:aws:s3:::{{bucketName}}/*"
                                                }
                                            ]
                                        }
                                """;

        await _amazonS3.PutBucketPolicyAsync(new PutBucketPolicyRequest
        {
            BucketName = bucketName,
            Policy = bucketPolicy
        }, cancellationToken);
    }
}
