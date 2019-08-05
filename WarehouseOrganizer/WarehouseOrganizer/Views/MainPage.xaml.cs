using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WarehouseOrganizer.Models;
using WarehouseOrganizer.ViewModels;

namespace WarehouseOrganizer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.ItemsByPlace, (NavigationPage)Detail);

            MessagingCenter.Subscribe<BaseViewModel, InfoUserMessage>(this, MessageConst.InfoUserMessage, async (v,m) => await ShowInfoUserMessageAsync(v,m));
            MessagingCenter.Subscribe<BaseViewModel, ActionUserMessage>(this, MessageConst.ActionUserMessage, async (v,m) => await ShowActionUserMessageAsync(v,m));
        }

        protected async Task ShowInfoUserMessageAsync(BaseViewModel viewModel, InfoUserMessage userMessage)
        {
            if (userMessage == null)
                return;

            await DisplayAlert(userMessage.Title, userMessage.MessageText, userMessage.ButtonCancelText);
        }

        protected async Task ShowActionUserMessageAsync(BaseViewModel viewModel, ActionUserMessage userMessage)
        {
            if (userMessage == null)
                return;

            bool result = await DisplayAlert(userMessage.Title, userMessage.MessageText, userMessage.ButtonOkText, userMessage.ButtonCancelText);

            if (userMessage.CallBack != null)
            {
                userMessage.CallBack.Invoke(result);
            }
        }


        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.ItemsByPlace:
                        MenuPages.Add(id, new NavigationPage(new ItemsByPlacePage()));
                        break;
                    case (int)MenuItemType.BindItemToPlace:
                        MenuPages.Add(id, new NavigationPage(new BindItemToPlacePage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}