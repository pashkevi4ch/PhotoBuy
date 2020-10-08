﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoBuy.Pages;

namespace PhotoBuy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CameraPage();
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
