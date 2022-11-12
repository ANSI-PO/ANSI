namespace Database.Infrastructure.Exceptions;

public abstract class DatabaseTierException : Exception
{
    protected DatabaseTierException(string message) : base(message)
    {
    }

    protected DatabaseTierException(string message, Exception innerException) : base(message, innerException)
    {
    }
}