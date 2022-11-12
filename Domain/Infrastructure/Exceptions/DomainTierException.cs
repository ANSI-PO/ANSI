namespace Domain.Infrastructure.Exceptions;

public abstract class DomainTierException : Exception
{
    protected DomainTierException(string message) : base(message)
    {
    }

    protected DomainTierException(string message, Exception innerException) : base(message, innerException)
    {
    }
}