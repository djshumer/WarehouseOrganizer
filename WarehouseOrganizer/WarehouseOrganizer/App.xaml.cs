using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WarehouseOrganizer.Services;
using WarehouseOrganizer.Views;
using WarehouseOrganizer.Models;
using System.Threading.Tasks;
using WarehouseOrganizer.ViewModels;
using System.Net;

namespace WarehouseOrganizer
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IItemDataStore, ItemDataStore>();
            DependencyService.Register<IDataStore<WarehousePlace>, WarehousePlacesDataStore>();
            DependencyService.Register<BindItemToPlaceViewModel>();
            DependencyService.Register<ItemsByPlaceViewModel>();

            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
