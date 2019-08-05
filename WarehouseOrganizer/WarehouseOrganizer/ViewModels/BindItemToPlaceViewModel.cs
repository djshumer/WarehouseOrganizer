using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using WarehouseOrganizer.Models;
using WarehouseOrganizer.Services;
using Xamarin.Forms;

namespace WarehouseOrganizer.ViewModels
{
    public class BindItemToPlaceViewModel : BaseViewModel
    {

        private long _itemId;
        private long _placeId;
        private WarehousePlaceDetailViewModel _place;
        private ItemDetailViewModel _item;
        private IDataStore<WarehousePlace> _warehousePlacesDataStore;
        private IItemDataStore _itemDataStore;
        
        public BindItemToPlaceViewModel()
        {
            Title = "Bind Item";

            LoadItemCommand = new Command((q) => ExecuteLoadItemById());
            LoadPlaceCommand = new Command((q) => ExecuteLoadWarehousePlaceId());
            BindItemToPlaceCommand = new Command((q) => ExecuteBindItemToPlace());

            this._itemDataStore = DependencyService.Resolve<IItemDataStore>();
            this._warehousePlacesDataStore = DependencyService.Resolve<IDataStore<WarehousePlace>>();
        }

        public long ItemId
        {
            get { return _itemId;  }
            set { _itemId = value; OnPropertyChanged("ItemId"); }
        }

        public long PlaceId
        {
            get { return _placeId; }
            set { _placeId = value; OnPropertyChanged("PlaceId"); }
        }

        public ItemDetailViewModel Item { get => _item; protected set => SetProperty(ref _item, value); }

        public WarehousePlaceDetailViewModel Place { get => _place; protected set => SetProperty(ref _place, value); }


        public ICommand LoadItemCommand { get; protected set; }

        public ICommand LoadPlaceCommand { get; protected set; }

        public ICommand BindItemToPlaceCommand { get; protected set; }


        async void ExecuteLoadItemById()
        {
            if (IsBusy || ItemId < 0) return;

            IsBusy = true;
            Item = null;
            Item foundedItem = null;

            try
            {
                try
                {
                    foundedItem = await _itemDataStore.GetEntityAsync(ItemId);
                }
                catch (HttpRequestException ex)
                {
                    if (ex.Message.Contains("404") == false)
                    {
                        ShowErrorUserMessage(this, "Error while get Item", "Cancel");
                        //TODO: Logger
                        Debug.WriteLine(ex);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorUserMessage(this, "Error while get Item", "Cancel");
                    //TODO: Logger
                    Debug.WriteLine(ex);
                    return;
                }

                if (foundedItem == null)
                {
                    ShowInformationUserMessage(this, "Item not found", "Cancel");
                    return;
                }

                Item = new ItemDetailViewModel(foundedItem);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void ExecuteLoadWarehousePlaceId()
        {
            if (IsBusy || PlaceId < 0) return;

            IsBusy = true;

            Place = null;

            WarehousePlace foundedPlace = null;
            try
            {
                try
                {
                    foundedPlace = await _warehousePlacesDataStore.GetEntityAsync(PlaceId);
                }
                catch (HttpRequestException ex)
                {
                    if (ex.Message.Contains("404") == false)
                    {
                        ShowErrorUserMessage(this, "Error while get Place", "Cancel");
                        //TODO: Logger
                        Debug.WriteLine(ex);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorUserMessage(this, "Error while get Place", "Cancel");
                    //TODO: Logger
                    Debug.WriteLine(ex);
                    return;
                }

                if (foundedPlace == null)
                {
                    ShowInformationUserMessage(this, "Place not found", "Cancel");
                    //TODO: Message User
                    return;
                }

                Place = new WarehousePlaceDetailViewModel(foundedPlace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void ExecuteBindItemToPlace()
        {
            if (IsBusy || Item == null || Place == null)
                return;

            IsBusy = true;

            try
            {
                bool result = await _itemDataStore.BindItemToPlaceAsync(_item.ItemId, _place.PlaceId);
                ShowInformationUserMessage(this, "Place binded successfully", "Ok");
            }
            catch (Exception ex)
            {
                ShowErrorUserMessage(this, "Error while bind item to place", "Cancel");
                //TODO: Logger
                Debug.WriteLine(ex);
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
