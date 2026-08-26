using Licitaciones.Domain.Common;
using System.Diagnostics.CodeAnalysis;
namespace Licitaciones.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}