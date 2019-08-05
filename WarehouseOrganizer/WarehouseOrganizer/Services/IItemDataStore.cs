using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public interface IItemDataStore : IDataStore<Item>
    {
        Task<bool> BindItemToPlaceAsync(long itemId, long placeId);
        Task<IEnumerable<Item>> GetItemsByPlaceId(long placeId);
    }
}