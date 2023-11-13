using ExamSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.Contract
{
    public interface IGenericService<T> where T : BaseEntity
    {
        public Task<IReadOnlyList<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(int id);

        public Task<T?> CreateAsync(T entity);

        public Task<T?> UpdateAsync(int id, object updateCommand);

        public Task<bool> DeleteAsync(int id);

    }
}
