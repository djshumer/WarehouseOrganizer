using System;
using WarehouseOrganizer.Models;
using Xamarin.Forms;

namespace WarehouseOrganizer.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        
        public Item Item { get; protected set; }

        public ItemDetailViewModel(Item item = null)
        {
            if (item != null)
                Title = $"{item.ItemType} - {item.Id}";

            Item = item;
        }

        public long ItemId
        {
            get { return Item.Id; }
            set { Item.Id = value; OnPropertyChanged("ItemId"); }
        }

        public String ItemType
        {
            get { return Item.ItemType;  }
            set { Item.ItemType = value; OnPropertyChanged("ItemType");  Title = $"{Item.ItemType}";  }
        }
        public Double SizeWidth
        {
            get { return Item.SizeWidth; }
            set { Item.SizeWidth = value; OnPropertyChanged("Width"); OnPropertyChanged("SizeTextView"); }
        }
        public Double SizeHeight
        {
            get { return Item.SizeHeight; }
            set { Item.SizeHeight = value; OnPropertyChanged("Height"); OnPropertyChanged("SizeTextView"); }
        }

        public Double SizeDepth
        {
            get { return Item.SizeDepth; }
            set { Item.SizeDepth = value; OnPropertyChanged("Depth"); OnPropertyChanged("SizeTextView"); }
        }

        public DateTime DateOfProduction
        {
            get { return Item.DateOfProduction; }
            set { Item.DateOfProduction = value; OnPropertyChanged("DateOfProduction"); }
        }

        public long? WarehousePlaceId
        {
            get { return Item.WarehousePlaceId; }
            set { Item.WarehousePlaceId = value; OnPropertyChanged("WarehousePlaceId"); }
        }

        public String SizeTextView {
            get { return $"Size: {String.Format("{0:f2}", SizeWidth)}x{String.Format("{0:f2}", SizeHeight)}x{String.Format("{0:f2}", SizeDepth)}"; }
        }

        public void AddNewItemMessage()
        {
            MessagingCenter.Send(this, MessageConst.AddNewItemMessage, Item);
        }

    }
}
