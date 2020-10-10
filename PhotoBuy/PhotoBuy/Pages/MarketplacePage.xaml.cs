using PhotoBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoBuy.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarketplacePage : ContentPage
    {
        public MarketplacePage()
        {
            InitializeComponent();
            T();
        }

        private void marketplaceCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void T()
        {
            await DisplayAlert(App.DatabaseTopCars.GetCarsAsync().Result[0].Name, App.DatabaseTopCars.GetCarsAsync().Result.Count.ToString(), "OK");
        }
    }
}