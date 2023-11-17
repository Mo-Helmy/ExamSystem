using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.UnitOfWorks.Contract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        public IExamRepository ExamRepository { get;}


        Task<int> CompleteAsync();
    }
}
