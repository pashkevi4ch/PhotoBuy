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

namespace PhotoBuy.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            //InitializeComponent();
            Button takePhotoBtn = new Button { Text = "Сделать фото" };
            Button getPhotoBtn = new Button { Text = "Выбрать фото" };
            Xamarin.Forms.Image image = new Xamarin.Forms.Image();
            List<AlocatedCar> topAlocatedCars = new List<AlocatedCar>();
            //// выбор фото
            getPhotoBtn.Clicked += async (o, e) =>
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    image.Source = ImageSource.FromFile(photo.Path);
                    topAlocatedCars = GetTopCars(photo);
                }
            };

            //// съемка фото
            //takePhotoBtn.Clicked += async (o, e) =>
            //{
            //    await CrossMedia.Current.Initialize();
            //    if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            //    {
            //        MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //        {
            //            SaveToAlbum = true,
            //            Directory = "Sample",
            //            Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
            //        });

            //        if (file == null)
            //            return;

            //        img.Source = ImageSource.FromFile(file.Path);
            //    }
            //};
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new StackLayout
                    {
                         Children = {takePhotoBtn, getPhotoBtn},
                         Orientation =StackOrientation.Horizontal,
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    },
                    image
                }
            };

            takePhotoBtn.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    //DisplayAlert("No Camera", ":( No camera available.", "OK");
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

                await DisplayAlert("File Location", file.Path, "OK");

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            };
        }
        public List<AlocatedCar> GetTopCars(MediaFile photo)
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
            var ListResponse = response.Content.ToString().Replace(@"\", "").Replace('"', ' ').Replace("}}", "").Replace("{ probabilities :{", "").Trim().Split(',');
            List<AlocatedCar> topCars = new List<AlocatedCar>();
            foreach (var i in ListResponse)
            {
                var newCar = new AlocatedCar();
                newCar.Name = i.Split(':')[0];
                newCar.Probability = decimal.Parse(i.Split(':')[1]);
                topCars.Add(newCar);
            }
            return topCars.OrderByDescending(s => s.Probability).Take(5).ToList();
        }
    }
}