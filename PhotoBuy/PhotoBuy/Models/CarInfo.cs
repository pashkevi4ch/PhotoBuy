using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhotoBuy.Models
{
    public class CarInfo
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Model { get; set; }
        public string OwnTitle { get; set; }
        public int MinPrice { get; set; }
        public string RenderPhotos { get; set; }
        public int ColorsCount { get; set; }
        public string SizesPhotos { get; set; }
        public string Probability { get; set; }

    }
}
