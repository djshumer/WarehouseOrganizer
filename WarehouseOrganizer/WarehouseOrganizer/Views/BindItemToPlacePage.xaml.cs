using System;
using System.ComponentModel;
using WarehouseOrganizer.Models;
using WarehouseOrganizer.ViewModels;
using Xamarin.Forms;

namespace WarehouseOrganizer.Views
{
    [DesignTimeVisible(false)]
    public partial class BindItemToPlacePage : ContentPage
    {
        private BindItemToPlaceViewModel _viewModel;
        
        public BindItemToPlacePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = DependencyService.Get<BindItemToPlaceViewModel>();
        }

        async void ScanItemId(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FullScreenScanPage(EnumScanType.Item));
        }

        async void ScanPlaceId(object sender, EventArgs e)
        {
            FullScreenScanPage page = new FullScreenScanPage(EnumScanType.WarehousePlace);
            page.AdvPageOnScanResult += Page_AdvPageOnScanResult; ;
            await Navigation.PushModalAsync(page);
        }

        private async void Page_AdvPageOnScanResult(object sender, ZXing.Result result)
        {
            FullScreenScanPage page = (FullScreenScanPage)sender;
            page.AdvPageOnScanResult -= Page_AdvPageOnScanResult;

            if (result == null || _viewModel == null) return;

            long id = 0;

            try
            {
                id = int.Parse(result.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Inforamtion", "Incorrect format of the bar code or qr code", "Cancel");
                return;
            }

            switch (page.ScanType)
            {
                case EnumScanType.Item:
                    _viewModel.ItemId = id;
                    if (_viewModel.LoadItemCommand.CanExecute(id))
                        _viewModel.LoadItemCommand.Execute(id);
                    break;
                case EnumScanType.WarehousePlace:
                    _viewModel.PlaceId = id;
                    if (_viewModel.LoadPlaceCommand.CanExecute(id))
                        _viewModel.LoadPlaceCommand.Execute(id);
                    break;
            }
        }

    }
}