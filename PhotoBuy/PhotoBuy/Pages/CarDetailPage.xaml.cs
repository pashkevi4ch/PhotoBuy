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
        Slider Slider1;
        Slider Slider2;


        public CarDetailPage()
        {
            InitializeComponent();
            CarImage.Source = App.CurrentCar.RenderPhotos.Split(new string[] { "," }, StringSplitOptions.None)[0];
            CarNameLabel.Text = App.CurrentCar.Name;
            Header1 = new Label();
            Header2 = new Label();
            Slider1 = new Slider { Minimum = 0, Maximum = 50, Value = 30, ThumbColor = Color.Blue };
            Slider1.ValueChanged += slider1_ValueChanged;
            Slider2 = new Slider { Minimum = 0, Maximum = 7, Value = 3,  ThumbColor = Color.Blue};
            Slider2.ValueChanged += slider2_ValueChanged;
            SliderStackLayout.Children.Add(Header1);
            SliderStackLayout.Children.Add(Slider1);
            SliderStackLayout.Children.Add(Header2);
            SliderStackLayout.Children.Add(Slider2);
        }

        void slider1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Header1.Text = String.Format("Выбрано: {0:F1}", e.NewValue);
        }

        void slider2_ValueChanged(Object sender, ValueChangedEventArgs e)
        {
            Header2.Text = String.Format("Выбрано: {0:F1}", e.NewValue);
        }

        private void T()
        {
            var smth = Slider1.Value;   
        }
    }
}