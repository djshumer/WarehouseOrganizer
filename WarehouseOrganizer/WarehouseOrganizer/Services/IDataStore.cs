using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public interface IDataStore<TEntity> where TEntity : BaseEntitySimpleModel
    {
        Task<bool> AddEntityAsync(TEntity Entity);
        Task<bool> UpdateEntityAsync(TEntity Entity);
        Task<bool> DeleteEntityAsync(long id);
        Task<TEntity> GetEntityAsync(long id);
        Task<IEnumerable<TEntity>> GetEntityAsync(bool forceRefresh = false);
    }
}
