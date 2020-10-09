using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoBuy.Pages;
using System.IO;
using PhotoBuy.DataBase;
using System.Collections.Generic;

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
                    fairProjects = App.DatabasePreviousRequests.GetRequestsAsync().Result;
                }
                return requests;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
