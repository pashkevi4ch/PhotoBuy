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
            int i = 0;
            foreach (var im in App.CurrentCar.RenderPhotos.Split(new string[] { "," }, StringSplitOptions.None))
            {
                if (i > 0)
                {
                    Image image = new Image() { Source = im };
                    imageStackLayout.Children.Add(image);
                }
                i += 1;
            }

            CarNameLabel.Text = App.CurrentCar.Name;
            CarPriceLabel.Text = App.CurrentCar.MinPrice.ToString();
            CountryLabel.Text = App.CurrentCar.Country;
            ModelLabel.Text = App.CurrentCar.Model;
            OwnerLabel.Text = App.CurrentCar.OwnTitle;
            ColorsLabel.Text = App.CurrentCar.ColorsCount.ToString();
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

        private async void confirmButton_Clicked(object sender, EventArgs e)
        {
            var client = new RestClient("https://gw.hackathon.vtb.ru/vtb/hackathon/carloan");
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-ibm-client-id", "b3996f788ffa2e39e01197b60036d30b");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");
            request.AddParameter("application/json", "{\"comment\":\"Комментарий\",\"customer_party\":{\"email\":\"" + emailEntry.Text + "\",\"income_amount\":" + incomeAmountEntry.Text + ",\"person\":{\"birth_date_time\":\"" + birthDateEntry.Text + "\",\"birth_place\":\"" + birthPlaceEntry.Text + "\",\"family_name\":\"" + familyNameEntry.Text + "\",\"first_name\":\"" + firstNameEntry.Text + "\",\"gender\":\"" + genderEntry.Text + "\",\"middle_name\":\"" + middleNameEntry.Text + "\",\"nationality_country_code\":\"RU\"},\"phone\":\"" + phoneEntry.Text + "\"},\"datetime\":\"2020-10-10T08:15:47Z\",\"interest_rate\":15.7,\"requested_amount\":" + App.CurrentCar.MinPrice + ",\"requested_term\":" + (int)Slider2.Value*12 + ",\"trade_mark\":\"" + App.CurrentCar.Name + "\",\"vehicle_cost\":" + App.CurrentCar.MinPrice + "}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            emailEntry.Text = "";
            incomeAmountEntry.Text = "";
            birthDateEntry.Text = "";
            birthPlaceEntry.Text = "";
            familyNameEntry.Text = "";
            firstNameEntry.Text = "";
            genderEntry.Text = "";
            middleNameEntry.Text = "";
            phoneEntry.Text = "";
            var check = response.Content.Split(new string[] { "\"application_status\"" }, StringSplitOptions.None)[1].Split(new string[] { "," }, StringSplitOptions.None)[0] == ":\"prescore_approved\"";
            if (check)
            {
                await DisplayAlert("Ваша заявка предварительно одобрена", $"Вы можете обратиться по поводу официального оформления до {response.Content.Split(new string[] { "\"decision_end_date\":\""}, StringSplitOptions.None)[1].Split(new string[] { "\","}, StringSplitOptions.None)[0]}", "Oк");
            }
            else
            {
                await DisplayAlert("Ваша заявка предварительно не одобрена", "", "Oк");
            }

        }
    }
}