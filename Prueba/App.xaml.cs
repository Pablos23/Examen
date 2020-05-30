using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prueba.Services;
using Prueba.Views;
using Prism.Unity;
using Prism.Mvvm;
using Prism.Ioc;
using Newtonsoft.Json;
using Prueba.ViewModels;

namespace Prueba
{
    public partial class App : PrismApplication
    {
        public static App CurrentInstance { get; private set; }
        public App()
        {       
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ItemDetailPage, ItemDetailViewModel>();
            containerRegistry.RegisterForNavigation<ItemsPage, ItemsViewModel>();
            containerRegistry.RegisterForNavigation<NewItemPage, NewItemViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>();
            //Services
            containerRegistry.Register<IDataStore, DataBaseService>();
            containerRegistry.Register<IApiService, ApiService>();
        }
        protected override void OnInitialized()
        {
            InitializeComponent();
            CurrentInstance = this;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            NavigationService.NavigateAsync("MainPage?SelectTab=ItemsPage");
        }
    }
}
