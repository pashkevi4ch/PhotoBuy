using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoBuy.Pages;
using System.IO;
using PhotoBuy.DataBase;
using System.Collections.Generic;
using PhotoBuy.Models;

namespace PhotoBuy
{
    public partial class App : Application
    {
        public const string DATABASE_NAME_PreviousRequests = "PreviousRequests.db";
        static RequestDatabase databasePreviousRequests;
        public static RequestDatabase DatabasePreviousRequests
        {
            get
            {
                if (databasePreviousRequests == null)
                {
                    databasePreviousRequests = new RequestDatabase(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME_PreviousRequests));
                }
                return databasePreviousRequests;
            }
        }

        public static IList<Request> PreviousRequests
        {
            get
            {
                List<Request> requests = new List<Request>();
                if (databasePreviousRequests != null)
                {
                    requests = App.DatabasePreviousRequests.GetRequestsAsync().Result;
                }
                return requests;
            }
        }

        public const string DATABASE_NAME_TopCars = "TopCars.db";
        static TopCarsDatabase databaseTopCars;
        public static TopCarsDatabase DatabaseTopCars
        {
            get
            {
                if (databaseTopCars == null)
                {
                    databaseTopCars = new TopCarsDatabase(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME_TopCars));
                }
                return databaseTopCars;
            }
        }

        public static IList<AlocatedCar> TopCars
        {
            get
            {
                List<AlocatedCar> cars = new List<AlocatedCar>();
                if (databasePreviousRequests != null)
                {
                    cars = App.databaseTopCars.GetCarsAsync().Result;
                }
                return cars;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainShellPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
