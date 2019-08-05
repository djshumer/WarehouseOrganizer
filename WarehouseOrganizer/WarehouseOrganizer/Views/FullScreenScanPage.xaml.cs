using System;
using System.ComponentModel;
using WarehouseOrganizer.Models;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace WarehouseOrganizer.Views
{
    [DesignTimeVisible(false)]
    public partial class FullScreenScanPage : ZXingScannerPage
    {
        
        public FullScreenScanPage(EnumScanType scanType)
        {
            InitializeComponent();

            this.BindingContext = this;

            this.ScanType = scanType;

            Title = "Scanner";
        }

        public EnumScanType ScanType { get; protected set; }

        private async void Handler_OnScanResultAsync(Result result)
        {
            if (AdvPageOnScanResult != null)
            {
                Device.BeginInvokeOnMainThread(()=> AdvPageOnScanResult.Invoke(this, result));
            }
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            this.IsAnalyzing = true;
            this.IsScanning = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            this.IsScanning = false;
            this.IsAnalyzing = false;
            base.OnDisappearing();
        }

        public event EventHandler<Result> AdvPageOnScanResult;

    }
}