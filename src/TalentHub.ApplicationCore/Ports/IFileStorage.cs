namespace TalentHub.ApplicationCore.Ports;

public interface IFileStorage 
{
    Task<string> SaveAsync(
        string bucket,
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default
    ); 

    Task<Stream> GetAsync(
        string bucket,
        string fileName, 
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string bucket,
        string fileName, 
        CancellationToken cancellationToken = default);
    void DeleteAsync(string userProfilePicture, object profilePictureFileName);
}
