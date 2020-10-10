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
        public List<string> RenderPhotos { get; set; }
        public int ColorsCount { get; set; }
        public List<string> SizesPhotos { get; set; }
        public decimal Probability { get; set; }

    }
}
