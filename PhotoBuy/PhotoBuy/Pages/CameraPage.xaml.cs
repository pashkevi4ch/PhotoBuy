using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using PhotoBuy.Models;
using PhotoBuy.DataBase;

namespace PhotoBuy.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();
            T();
            Xamarin.Forms.Image image = new Xamarin.Forms.Image();
            List<AlocatedCar> topAlocatedCars = new List<AlocatedCar>();
            uploadButton.Clicked += async (o, e) =>
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    image.Source = ImageSource.FromFile(photo.Path);
                    topAlocatedCars = GetTopCars(photo);
                }
            };

            cameraButton.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
                topAlocatedCars = GetTopCars(file);
                if (file == null)
                    return;

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            };
        }

        private List<AlocatedCar> GetTopCars(MediaFile photo)
        {
            var client = new RestClient("https://gw.hackathon.vtb.ru/vtb/hackathon/car-recognize");
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-ibm-client-id", "b3996f788ffa2e39e01197b60036d30b");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("accept", "application/json");
            using (SixLabors.ImageSharp.Image tmp_image = SixLabors.ImageSharp.Image.Load(File.ReadAllBytes(photo.Path)))
            {
                int width = tmp_image.Width / 5;
                int height = tmp_image.Height / 5;
                tmp_image.Mutate(x => x.Resize(width, height));
                tmp_image.Save(photo.Path);
            }
            var base64Image = Convert.ToBase64String(File.ReadAllBytes(photo.Path));
            request.AddParameter("application/json", $"{{\"content\":\"{base64Image}\"}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var listResponse = response.Content.ToString().Replace(@"\", "").Replace('"', ' ').Replace("}}", "").Replace("{ probabilities :{", "").Trim().Split(',');
            List<AlocatedCar> topCars = new List<AlocatedCar>();
            foreach (var i in listResponse)
            {
                var newCar = new AlocatedCar();
                newCar.Name = i.Split(':')[0];
                newCar.Probability = decimal.Parse(i.Split(':')[1]);
                topCars.Add(newCar);
            }
            try
            {
                var req = App.DatabasePreviousRequests.GetRequestAsync(topCars.First(s => s.Probability == topCars.Max(e => e.Probability)).Name);
                req.Result.Quantity += 1;
                App.DatabasePreviousRequests.UpdateAsync(req.Result);
            }
            catch
            {
                var req = new Request();
                req.Name = topCars.First(s => s.Probability == topCars.Max(e => e.Probability)).Name;
                req.Quantity = 1;
                App.DatabasePreviousRequests.SaveRequestAsync(req);
            }
            //БД
            foreach (var car in topCars.OrderByDescending(s => s.Probability).Take(5).ToList())
            {
                App.DatabaseTopCars.SaveCarAsync(car);
            }
            T();
            return topCars.OrderByDescending(s => s.Probability).Take(5).ToList();
        }
        private async void T()
        {
            await Shell.Current.GoToAsync("marketplacepage");
        }
    }
}