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
    public partial class CarDetailPage : ContentPage
    {
        Label Header1;
        Label Header2;

        public CarDetailPage()
        {
            InitializeComponent();
            Header1 = new Label();
            Header2 = new Label();
            Slider slider1 = new Slider { Minimum = 0, Maximum = 50, Value = 30, ThumbColor = Color.Blue };
            slider1.ValueChanged += slider1_ValueChanged;
            Slider slider2 = new Slider { Minimum = 0, Maximum = 100, Value = 10, ThumbColor = Color.Blue };
            slider2.ValueChanged += slider2_ValueChanged;
            SliderStackLayout.Children.Add(Header1);
            SliderStackLayout.Children.Add(slider1);
            SliderStackLayout.Children.Add(Header2);
            SliderStackLayout.Children.Add(slider2);
        }

        void slider1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Header1.Text = String.Format("Выбрано: {0:F1}", e.NewValue);
        }

        void slider2_ValueChanged(Object sender, ValueChangedEventArgs e)
        {
            Header2.Text = String.Format("Выбрано: {0:F1}", e.NewValue);
        }
    }
}