namespace AuthApi.Domain.Exceptions;

public class ErrorOnValidationException : BaseException
{
    public ErrorOnValidationException(string message) : base(message)
    {
    }
}
