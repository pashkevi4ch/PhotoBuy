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
    public partial class PopularRequestsPage : ContentPage
    {
        public PopularRequestsPage()
        {
            InitializeComponent();
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

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    App.Current.MainPage = new MainShellPage();
        //    Shell.Current.GoToAsync("//pop");
        //}
    }
}