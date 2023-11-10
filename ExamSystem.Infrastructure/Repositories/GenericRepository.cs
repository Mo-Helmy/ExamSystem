using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Data;
using ExamSystem.Infrastructure.Repositories.Contract;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Infrastructure.Specifications.Contract;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Query Methods
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();

        }

        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        #endregion

        #region Command Methods

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        #endregion


    }
}
