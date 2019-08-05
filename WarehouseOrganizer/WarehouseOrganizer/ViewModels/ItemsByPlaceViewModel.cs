using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WarehouseOrganizer.Models;
using WarehouseOrganizer.Services;

using System.Linq;
using System.Net.Http;

namespace WarehouseOrganizer.ViewModels
{
    public class ItemsByPlaceViewModel : BaseViewModel
    {
        
        protected IItemDataStore ItemsDataStore = null;
        protected IDataStore<WarehousePlace> PlaceDataStore = null;
        private long _warehousePlaceId;
        private WarehousePlaceDetailViewModel _place;
        private ObservableCollection<ItemDetailViewModel> _items;

        public ItemsByPlaceViewModel()
        {
            Title = "Browse Items";
            ItemsView = new ObservableCollection<ItemDetailViewModel>();

            ItemsDataStore = DependencyService.Resolve<IItemDataStore>();
            PlaceDataStore = DependencyService.Resolve<IDataStore<WarehousePlace>>();

            LoadItemsByPlaceIdCommand = new Command(async () => await ExecuteLoadItemsByPlaceIdCommand());
        }

        public long WarehousePlaceId { get => _warehousePlaceId; set => SetProperty(ref _warehousePlaceId, value); }

        public WarehousePlaceDetailViewModel PlaceView { get => _place; set => SetProperty(ref _place, value); }

        public ObservableCollection<ItemDetailViewModel> ItemsView { get => _items; set => SetProperty(ref _items, value); }

        public Command LoadItemsByPlaceIdCommand { get; protected set; }


        async Task ExecuteLoadItemsByPlaceIdCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            ItemsView.Clear();
            PlaceView = null;

            try
            {

                try
                {
                    var place = await PlaceDataStore.GetEntityAsync(_warehousePlaceId);
                    if (place == null) {
                        ShowInformationUserMessage(this, "Warehouse place not found", "Cancel");
                        return;
                    };
                    PlaceView = new WarehousePlaceDetailViewModel(place);
                }
                catch (HttpRequestException ex)
                {
                    if (ex.Message.Contains("404"))
                    {
                        ShowInformationUserMessage(this, "Warehouse place not found", "Cancel");
                        return;
                    }
                }


                var items = await ItemsDataStore.GetItemsByPlaceId(_warehousePlaceId);

                if (items == null || items.Any() == false)
                {
                    ShowInformationUserMessage(this, "Items by place not found", "Cancel");
                    return;
                }

                foreach (var item in items)
                {
                    ItemsView.Add(new ItemDetailViewModel(item));
                }
            }
            catch (Exception ex)
            {
                ShowErrorUserMessage(this, "Error on load Data", "Cancel");
                //TODO: Logger
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}