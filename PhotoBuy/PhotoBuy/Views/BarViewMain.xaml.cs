using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoBuy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarViewMain : ContentView
    {
        public BarViewMain()
        {
            InitializeComponent();
        }

        private async void AccountButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("accountpage");
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("settingspage");
        }
    }
}