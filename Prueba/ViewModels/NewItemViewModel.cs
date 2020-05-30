using Prism.Navigation;
using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prueba.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private UserInfo item;
        public Command CancelCommand { get; set; }
        public Command SaveCommand { get; set; }
        private bool emailErro;

        public bool EmailError
        {
            get { return emailErro; }
            set { SetProperty(ref emailErro, value); }
        }


        private bool emailIsValid;

        public bool EmailIsValid
        {
            get { return emailIsValid; }
            set { SetProperty(ref emailIsValid, value); }
        }

        public UserInfo Item
        {
            get { return item; }
            set { item = value; }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                SetProperty(ref email, value)
;
            }
        }
        private string city;
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }


        public NewItemViewModel(INavigationService navigationService) : base(navigationService)
        {
            item = new UserInfo();
            CancelCommand = new Command(() => { CancelCommandExecute(); });
            SaveCommand = new Command(() => { SaveCommandExecute(); });
        }

        private async void SaveCommandExecute()
        {
            if (ValidateData())
            {
                var parameters = new NavigationParameters();
                parameters.Add("items", item);
                await NavigationService.GoBackAsync(parameters);
            }

        }

        private bool ValidateData()
        {
            if ((string.IsNullOrEmpty(item.Email) || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.LastName) ))
            {
                EmailError = true;
                ShowError = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        private async void CancelCommandExecute()
        {
            await NavigationService.GoBackAsync();
        }

    }
}
