namespace Database.Infrastructure.Exceptions;

public sealed class DatabaseQueryExecutionException : DatabaseTierException
{
    public DatabaseQueryExecutionException(string message) : base(message)
    {
    }

    public DatabaseQueryExecutionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}