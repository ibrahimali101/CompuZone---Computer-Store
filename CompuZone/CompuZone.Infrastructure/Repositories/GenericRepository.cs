using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using CompuZone.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompUZone.Models;

namespace CompuZone.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompuZoneContext _dbContext;

        public GenericRepository(CompuZoneContext dbContext)
        {
            _dbContext = dbContext;
        }
        public   IQueryable<TEntity> GetAllAsync()
        {
            return  _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIDAsync(int id)
        {
            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(a => a.ID == id);
        }
        public void AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity); 
        }

        public void ArchivedAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void UpdateAsync(TEntity entity)
        {

        }

        public void UnArchivedAsync(TEntity entity)
        {
            entity.ArchivedById = null;
            entity.ArchivedDateTime = null;
            entity.ArchivedByName = null;
            entity.IArchived = false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            int rowsAffected = await _dbContext.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
