using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.File;

public sealed class MinIOFileStorage : IFileStorage
{
    private readonly IAmazonS3 _amazonS3;
    private readonly string _serviceUrl; 

    public MinIOFileStorage()
    {
        var config = new AmazonS3Config()
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

    public Task DeleteAsync(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default) =>
        _amazonS3.DeleteAsync(
            bucket,
            fileName,
            new Dictionary<string, object>(),
            cancellationToken);

    public async Task<Stream> GetAsync(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default
    )
    {
        var obj = await _amazonS3.GetObjectAsync(new GetObjectRequest
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
        await _amazonS3.EnsureBucketExistsAsync(bucket);

        var response = await _amazonS3.PutObjectAsync(new PutObjectRequest
        {
            BucketName = bucket,
            InputStream = fileStream,
            ContentType = contentType,
            Key = fileName,
            AutoCloseStream = true,
            CannedACL = S3CannedACL.PublicRead
        }, cancellationToken);

        if(response.HttpStatusCode != HttpStatusCode.OK)
            throw new Exception();

        return $"{_serviceUrl}/{bucket}/{fileName}";
    }
}
