using System;
using System.Collections.Generic;
using WarehouseOrganizer.Models;

namespace WarehouseOrganizer.ViewModels
{
    public class WarehousePlaceDetailViewModel : BaseViewModel
    {
        public WarehousePlace Place { get; protected set; }

        public WarehousePlaceDetailViewModel(WarehousePlace place = null)
        {
            Title = place?.PlaceName;
            Place = place;
        }

        public long PlaceId
        {
            get { return Place.Id; }
            set { Place.Id = value; OnPropertyChanged("PlaceId"); }
        }

        public String PlaceName
        {
            get { return Place.PlaceName; }
            set { Place.PlaceName = value; OnPropertyChanged("PlaceName"); }
        }

        public int PlaceColumn
        {
            get { return Place.PlaceColumn; }
            set { Place.PlaceColumn = value; OnPropertyChanged("PlaceColumn"); OnPropertyChanged("PlaceTextView"); }
        }

        public int PlaceRow
        {
            get { return Place.PlaceRow; }
            set { Place.PlaceRow = value; OnPropertyChanged("PlaceRow"); OnPropertyChanged("PlaceTextView"); }
        }

        public String PlaceTextView { get { return $"{PlaceName} - col: {PlaceColumn} row: {PlaceRow}"; } }

        public List<Item> ItemsOnPlace
        {
            get { return Place.ItemsOnPlace; }
            set { Place.ItemsOnPlace = value; OnPropertyChanged("ItemsOnPlace"); }
        }
    }
}
