using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public class WarehousePlacesDataStore : GenericDataStore<WarehousePlace>
    {
        public WarehousePlacesDataStore() : base("api/warehouseplace")
        {

        }

    }
}
