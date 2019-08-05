using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public class GenericDataStore<TEntity> : IDataStore<TEntity> where TEntity : BaseEntitySimpleModel
    {
        protected HttpClient client;
        protected IEnumerable<TEntity> entities;
        protected readonly String controllerUrl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemUrl">example: api/item</param>
        public GenericDataStore(String controllerUrl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            entities = new List<TEntity>();

            this.controllerUrl = controllerUrl;
        }

        public bool IsConnected { get { return Connectivity.NetworkAccess == NetworkAccess.Internet; } }
        public async Task<IEnumerable<TEntity>> GetEntityAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync(controllerUrl);
                entities = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json));
            }

            return entities;
        }

        public async Task<TEntity> GetEntityAsync(long id)
        {
            if (IsConnected)
            {
                var json = await client.GetStringAsync($"{controllerUrl}/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<TEntity>(json));
            }

            return null;
        }

        public async Task<bool> AddEntityAsync(TEntity item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync(controllerUrl, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEntityAsync(TEntity entity)
        {
            if (entity == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(entity);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"{controllerUrl}/{entity.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEntityAsync(long id)
        {
            if (!IsConnected)
                return false;

            var response = await client.DeleteAsync($"{controllerUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
