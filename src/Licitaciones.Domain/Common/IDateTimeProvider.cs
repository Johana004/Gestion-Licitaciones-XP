namespace Licitaciones.Domain.Common;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}