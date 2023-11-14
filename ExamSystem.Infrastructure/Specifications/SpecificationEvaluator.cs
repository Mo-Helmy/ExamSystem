using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications.Contract;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Infrastructure.Specifications
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.CriteriaList.Any()) //.where(criteriaList[1]).where(criteriaList[2])...
                query = spec.CriteriaList.Aggregate(query, (currentQuery, criteriaExpression) => currentQuery.Where(criteriaExpression));

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
