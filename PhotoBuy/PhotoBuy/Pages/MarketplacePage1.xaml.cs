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
    public partial class MarketplacePage1 : ContentPage
    {

        public MarketplacePage1()
        {
            InitializeComponent();
            //firstCarNameLabel.Text = App.TopCars[0].Name;
        }

        private async void T()
        {
            await DisplayAlert(App.DatabaseTopCars.GetCarsAsync().Result[0].Name, App.DatabaseTopCars.GetCarsAsync().Result.Count.ToString(), "OK");
        }

        private void firstCarDetailButton_Clicked(object sender, EventArgs e)
        {
            App.DatabaseCurrentCar.DeleteAll();
            App.DatabaseCurrentCar.SaveCarAsync(App.TopCars[0]);
            NextPage();
        }

        private void marketplaceListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CarInfo car = e.Item as CarInfo;
            App.DatabaseCurrentCar.DeleteAll();
            App.DatabaseCurrentCar.SaveCarAsync(car);
            NextPage();
        }

        private async void NextPage()
        {
            await Shell.Current.GoToAsync("cardetailpage");
        }
    }
}