namespace AuthApi.Domain.Adapters.Services.File;

public interface IFileService
{
    Task<string> UploadAvatar(byte[] content, string filename);
}
