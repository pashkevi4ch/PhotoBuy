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
        }

        private void marketplaceCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}