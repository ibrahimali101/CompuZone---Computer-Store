using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.Domain.Entities;

namespace CompuZone.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAllAsync();
        Task<TEntity> GetByIDAsync(int id);
        void AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void ArchivedAsync(TEntity entity);
        void UnArchivedAsync(TEntity entity);

        Task<bool> SaveChangesAsync();
    }
}
