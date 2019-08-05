using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseOrganizer.Models
{
    public enum MenuItemType
    {
        ItemsByPlace,
        BindItemToPlace,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
