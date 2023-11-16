using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Data;
using ExamSystem.Infrastructure.Repositories;
using ExamSystem.Infrastructure.Repositories.Contract;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly Hashtable _repositories;
        
        public IExamRepository ExamRepository {  get; private set; }
        
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            _repositories = new Hashtable();
            
            this.ExamRepository = new ExamRepository(dbContext);
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                _repositories.Add(type, new GenericRepository<TEntity>(dbContext));
            }

            return _repositories[type] as IGenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }
    }
}
