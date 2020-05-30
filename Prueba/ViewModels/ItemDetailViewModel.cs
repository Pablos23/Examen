using System;
using Prism.Navigation;
using Prueba.Models;

namespace Prueba.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "";
        }
    }
}
