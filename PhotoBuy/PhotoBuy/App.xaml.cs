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

        public static IList<CarInfo> TopCars
        {
            get
            {
                List<CarInfo> cars = new List<CarInfo>();
                if (databaseTopCars != null)
                {
                    cars = App.databaseTopCars.GetCarsAsync().Result;
                }
                return cars;
            }
        }

        public static IList<CarInfo> TopFourCars
        {
            get
            {
                List<CarInfo> cars = new List<CarInfo>();
                if (databaseTopCars != null)
                {
                    for (int i = 1;i < App.TopCars.Count; i++)
                    {
                        cars.Add(App.TopCars[i]);
                    }
                }
                return cars;
            }
        }

        public const string DATABASE_NAME_CurrentCar = "CurrentCar.db";
        static CurrentCarDatabase databaseCurrentCar;
        public static CurrentCarDatabase DatabaseCurrentCar
        {
            get
            {
                if (databaseCurrentCar == null)
                {
                    databaseCurrentCar = new CurrentCarDatabase(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME_CurrentCar));
                }
                return databaseCurrentCar;
            }
        }

        public static CarInfo CurrentCar
        {
            get
            {
                CarInfo car = new CarInfo();
                if (databaseCurrentCar != null)
                {
                    car = App.DatabaseCurrentCar.GetCarsAsync().Result[0];
                }
                return car;
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
