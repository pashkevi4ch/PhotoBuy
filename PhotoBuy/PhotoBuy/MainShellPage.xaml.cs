using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoBuy.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoBuy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShellPage : Shell
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }
        public MainShellPage()
        {
            InitializeComponent();
            RegisterRoutes();
            T();
        }

        void RegisterRoutes()
        {
            routes.Add("marketplacepage", typeof(MarketplacePage1));
            routes.Add("cardetailpage", typeof(CarDetailPage));
            routes.Add("accountpage", typeof(AccountPage));
            routes.Add("settingspage", typeof(SettingsPage));
            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private async void T()
        {
            await DisplayAlert(App.DatabasePreviousRequests.GetRequestsAsync().Result.Count.ToString(), "", "OK");
        }
    }
}