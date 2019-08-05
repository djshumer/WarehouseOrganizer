using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public class ItemDataStore : GenericDataStore<Item>, IItemDataStore
    {
        public ItemDataStore() : base("api/item")
        {

        }

        /// <summary>
        /// Bind item To warehouse place
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="placeId"></param>
        /// <returns>true if success</returns>
        public async Task<bool> BindItemToPlaceAsync(long itemId, long placeId)
        {
            if (itemId < 0 || placeId < 0 || !IsConnected)
                return false;

            var response = await client.PutAsync($"{controllerUrl}/itemId={itemId}&&bindPlaceId={placeId}", content: null);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Item>> GetItemsByPlaceId(long placeId)
        {
            if (placeId < 0 || !IsConnected)
                return null;

            var json = await client.GetStringAsync($"{controllerUrl}/placeId={placeId}");

            entities = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));

            return entities;
        }
    }
}
