using System;
using System.Collections.Generic;
using System.Text;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.Services
{
    public class MockWarehouseDataStore : MockGenericDataStore<WarehousePlace>
    {
        public MockWarehouseDataStore()
        {
            for (int i = 1; i < 10; i++)
            {
                var place = new WarehousePlace();
                place.Id = i;
                place.PlaceColumn = i * 3;
                place.PlaceRow = i * 2;
                place.PlaceName = $"Place Name - {i}";
                entities.Add(place);
            }
        }

    }
}
