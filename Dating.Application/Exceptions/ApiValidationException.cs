namespace Dating.Application.Exceptions;

public class ApiValidationException : Exception
{
    public ApiValidationException(string message) : base(message)
    {
        
    }
}