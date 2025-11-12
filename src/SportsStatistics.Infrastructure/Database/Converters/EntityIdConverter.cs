using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal sealed class EntityIdConverter : ValueConverter<EntityId, Guid>
{
    public static readonly EntityIdConverter Instance = new();

    private EntityIdConverter() : base(id => id.Value, value => EntityId.Create(value)) { }
}
