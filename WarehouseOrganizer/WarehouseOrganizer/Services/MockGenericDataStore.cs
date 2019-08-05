using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public class MockGenericDataStore<TEntity> : IDataStore<TEntity> where TEntity : BaseEntitySimpleModel
    {
        protected List<TEntity> entities = new List<TEntity>();

        public MockGenericDataStore()
        {
            
        }

        public async Task<bool> AddEntityAsync(TEntity entity)
        {
            entities.Add(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteEntityAsync(long id)
        {
            var oldItem = entities.Where((q) => q.Id == id).FirstOrDefault();
            if(oldItem != null)
            entities.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TEntity> GetEntityAsync(long id)
        {
            return await Task.FromResult(entities.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TEntity>> GetEntityAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(entities);
        }

        public async Task<bool> UpdateEntityAsync(TEntity entity)
        {
            var oldItem = entities.Where((q) => q.Id == entity.Id).FirstOrDefault();
            entities.Remove(oldItem);
            entities.Add(entity);

            return await Task.FromResult(true);
        }
    }
}
