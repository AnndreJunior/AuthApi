namespace AuthApi.Domain.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(string message) : base(message)
    {
    }
}
