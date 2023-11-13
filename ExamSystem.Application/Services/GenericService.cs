using ExamSystem.Application.Helper;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            var entityList = await _unitOfWork.Repository<TEntity>().GetAllAsync();
            return entityList;
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
            return entity;
        }

        public virtual async Task<TEntity?> CreateAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _unitOfWork.Repository<TEntity>().AddAsync(entity);
            var count = await _unitOfWork.CompleteAsync();

            return count > 0 ? entity : null;
        }

        public virtual async Task<TEntity?> UpdateAsync(int id, object updateCommand)
        {
            var currentEntity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);

            if (currentEntity is null) throw new KeyNotFoundException("Id Key Not Exist");

            UpdateObjectHelper.UpdateObject(currentEntity, updateCommand);

            currentEntity.UpdatedAt = DateTime.Now;

            var count = await _unitOfWork.CompleteAsync();

            return count > 0 ? currentEntity : null;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var currentEntity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);

            if (currentEntity is null) throw new KeyNotFoundException("Id Key Not Exist");

            currentEntity.DeletedAt = DateTime.Now;
            currentEntity.IsDeleted = true;

            var count = await _unitOfWork.CompleteAsync();

            return count > 0 ? true : false;
        }


        
    }
}
