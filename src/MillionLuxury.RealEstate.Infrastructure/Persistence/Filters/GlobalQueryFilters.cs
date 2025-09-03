using Microsoft.EntityFrameworkCore;
using MillionLuxury.RealEstate.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Filters;
public sealed class GlobalQueryFilters
{
    public static void AddSoftDeleteQueryFilter(ModelBuilder builder)
    {
        var softDeleteEntities = typeof(ISoftDeletable).Assembly.GetTypes()
                .Where(type => typeof(ISoftDeletable)
                                .IsAssignableFrom(type)
                                && type.IsClass
                                && !type.IsAbstract);

        foreach (var softDeleteEntity in softDeleteEntities)
        {
            builder.Entity(softDeleteEntity).HasQueryFilter(GenerateQueryFilterLambda(softDeleteEntity));
        }
    }

    private static LambdaExpression? GenerateQueryFilterLambda(Type type)
    {
        var parameter = Expression.Parameter(type, "w");
        var falseConstantValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDeletable.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, falseConstantValue);
        var lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }
}
