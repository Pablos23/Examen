using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Prueba.Models;
using Prueba.Views;
using Prism.Navigation;
using System.Linq;
using System.Collections.Generic;

namespace Prueba.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<UserInfo> Items { get; set; }
        private List<UserInfo> localList { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddItem { get; set; }

        public ItemsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Usuarios";
            Items = new ObservableCollection<UserInfo>();
            localList = new List<UserInfo>();
            LoadItemsCommand = new Command(async () => { await LoadItemsExecute(); });
            AddItem = new Command(async () => { await AddItemExecute(); });
        }

        private async Task AddItemExecute()
        {
            await NavigationService.NavigateAsync(nameof(NewItemPage));
        }

        private async Task LoadItemsExecute()
        {
            await GetInfoData();
        }

        private async Task GetInfoData()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            var userResponse = await ApiService.Get<User>("", "?results=50");

            Items.Clear();
            foreach (var userInfo in userResponse.Results)
            {
                Items.Add(new UserInfo
                {
                    Name = $"{userInfo.Name.Title.ToString()} {userInfo.Name.First}",
                    LastName = userInfo.Name.Last,
                    Email = userInfo.Email,
                    ProfileImage = userInfo.Picture.Thumbnail,
                    Phone = userInfo.Phone
                });
            }


            IsBusy = false;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            await GetInfoData();
            if (parameters.ContainsKey("items"))
            {
                var item = parameters.GetValue<UserInfo>("items");
                if (item != null)
                {
                    localList.Add(item);
                    if (localList.Any())
                    {
                        foreach (var userInfo in localList)
                        {
                            Items.Add(userInfo);
                        }
                    }
                }
            }

        }
    }
}