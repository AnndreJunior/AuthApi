namespace AuthApi.Domain.Adapters.Validators;

public interface IFileValidator
{
    bool IsImage(Stream image);
}
