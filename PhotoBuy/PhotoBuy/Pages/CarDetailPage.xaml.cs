using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
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
        Label LabelFee;


        public CarDetailPage()
        {
            InitializeComponent();
            CarImage.Source = App.CurrentCar.RenderPhotos.Split(new string[] { "," }, StringSplitOptions.None)[0];
            CarNameLabel.Text = App.CurrentCar.Name;
            Header1 = new Label() { Text = "Первый взнос"};
            Header2 = new Label() {Text = "Период"};
            LabelFee = new Label();
            Slider1 = new Slider { Minimum = 0, Maximum = App.CurrentCar.MinPrice, Value = 100001, ThumbColor = Color.Blue };
            Slider1.ValueChanged += slider1_ValueChanged;
            Slider2 = new Slider { Minimum = 0, Maximum = 7, Value = 3,  ThumbColor = Color.Blue};
            Slider2.ValueChanged += slider2_ValueChanged;
            SliderStackLayout.Children.Add(Header1);
            SliderStackLayout.Children.Add(Slider1);
            SliderStackLayout.Children.Add(Header2);
            SliderStackLayout.Children.Add(Slider2);
            SliderStackLayout.Children.Add(LabelFee);
        }

        void slider1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Header1.Text = String.Format("Выбрано: {0:F1}", e.NewValue);
            var client = new RestClient("https://gw.hackathon.vtb.ru/vtb/hackathon/calculate");
            var request = new RestRequest(Method.POST);
            var term = (int)Slider2.Value;
            var mincost = App.CurrentCar.MinPrice;
            var initialFee = (int)Slider1.Value;
            request.AddHeader("x-ibm-client-id", "b3996f788ffa2e39e01197b60036d30b");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");
            request.AddParameter("application/json", "{\"clientTypes\":[\"ac43d7e4-cd8c-4f6f-b18a-5ccbc1356f75\"],\"cost\":" + mincost +
            ",\"initialFee\":" + initialFee + ",\"kaskoValue\":0,\"language\":\"en\",\"residualPayment\":0,\"settingsName\":\"Haval\",\"specialConditions\":[\"57ba0183-5988-4137-86a6-3d30a4ed8dc9\",\"b907b476-5a26-4b25-b9c0-8091e9d5c65f\",\"cbfc4ef3-af70-4182-8cf6-e73f361d1e68\"],\"term\":" + term + "}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var payment = response.Content.Split(new string[] { "\"payment\":" }, StringSplitOptions.None)[1].Split(new string[] { "," }, StringSplitOptions.None)[0];
            LabelFee.Text = $"Ежемесячный платеж {payment}";
        }

        void slider2_ValueChanged(Object sender, ValueChangedEventArgs e)
        {
            Header2.Text = String.Format("Выбрано: {0:F1}", e.NewValue);
            var client = new RestClient("https://gw.hackathon.vtb.ru/vtb/hackathon/calculate");
            var request = new RestRequest(Method.POST);
            var term = (int)Slider2.Value;
            var mincost = App.CurrentCar.MinPrice;
            var initialFee = (int)Slider1.Value;
            request.AddHeader("x-ibm-client-id", "b3996f788ffa2e39e01197b60036d30b");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");
            request.AddParameter("application/json", "{\"clientTypes\":[\"ac43d7e4-cd8c-4f6f-b18a-5ccbc1356f75\"],\"cost\":" + mincost +
            ",\"initialFee\":" + initialFee + ",\"kaskoValue\":0,\"language\":\"en\",\"residualPayment\":0,\"settingsName\":\"Haval\",\"specialConditions\":[\"57ba0183-5988-4137-86a6-3d30a4ed8dc9\",\"b907b476-5a26-4b25-b9c0-8091e9d5c65f\",\"cbfc4ef3-af70-4182-8cf6-e73f361d1e68\"],\"term\":" + term + "}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var payment = response.Content.Split(new string[] { "\"payment\":" }, StringSplitOptions.None)[1].Split(new string[] { "," }, StringSplitOptions.None)[0];
            LabelFee.Text = $"Ежемесячный платеж {payment}";
        }

        private void T()
        {
            var smth = Slider1.Value;   
        }

        private void confirmButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}