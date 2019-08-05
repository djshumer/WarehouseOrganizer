using System;
using System.ComponentModel;
using Xamarin.Forms;

using WarehouseOrganizer.Models;
using WarehouseOrganizer.ViewModels;

namespace WarehouseOrganizer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsByPlacePage : ContentPage
    {
        private ItemsByPlaceViewModel _viewModel;

        public ItemsByPlacePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = DependencyService.Get<ItemsByPlaceViewModel>();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ItemDetailViewModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item.Item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void ScanPlaceId(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.IsEnabled = false;
            FullScreenScanPage page = new FullScreenScanPage(EnumScanType.WarehousePlace);
            page.AdvPageOnScanResult += Page_AdvPageOnScanResult; ;
            await Navigation.PushModalAsync(page);
            btn.IsEnabled = true;
        }

        private async void Page_AdvPageOnScanResult(object sender, ZXing.Result result)
        {
            FullScreenScanPage page = (FullScreenScanPage)sender;
            page.AdvPageOnScanResult -= Page_AdvPageOnScanResult;

            if (result == null || _viewModel == null) return;

            int placeId = 0;

            try
            {
                placeId = int.Parse(result.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Inforamtion", "Incorrect format of the bar code or qr code", "Cancel");
                return;
            }

            if (_viewModel.LoadItemsByPlaceIdCommand.CanExecute(placeId))
            {
                _viewModel.WarehousePlaceId = placeId;
                _viewModel.LoadItemsByPlaceIdCommand.Execute(placeId);
            }
        }

    }
}