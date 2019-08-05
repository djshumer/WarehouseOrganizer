using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public class MockItemDataStore : MockGenericDataStore<Item>, IItemDataStore
    {

        public MockItemDataStore()
        {
           
            for (int i = 1; i < 20; i++)
            {
                var item = new Item();
                item.Id = i;
                item.ItemType = $"Detail Type - {i}";
                item.SizeWidth = i * 2;
                item.SizeHeight = i * 4;
                item.SizeDepth = i * 3;
                item.DateOfProduction = DateTime.Now.Date;
                item.WarehousePlaceId = i % 2 + 1;
               
                entities.Add(item);
            }

        }

        public async Task<bool> BindItemToPlaceAsync(long itemId, long placeId)
        {
            if (placeId < 0)
            {
                await Task.FromResult(false);
            }

            var item = entities.FirstOrDefault((q) => q.Id == itemId);
            if (item == null)
                return await Task.FromResult(false);

            if (placeId == 0)
                item.WarehousePlaceId = null;
            else
                item.WarehousePlaceId = placeId;

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Item>> GetItemsByPlaceId(long placeId)
        {
            var result = entities.Where((q) => q.WarehousePlaceId == placeId).ToList();
            return await Task.FromResult(result);
        }
    }
}
