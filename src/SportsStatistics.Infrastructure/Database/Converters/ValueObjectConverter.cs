using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SportsStatistics.Infrastructure.Database.Converters;

internal abstract class ValueObjectConverter<T> : ValueConverter<T, string>
{
    protected ValueObjectConverter(
        Expression<Func<T, string>> toProviderExpression,
        Expression<Func<string, T>> fromProviderExpression)
        : base(toProviderExpression, fromProviderExpression)
    {
    }
}
