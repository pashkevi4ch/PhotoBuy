using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PhotoBuy.DataBase
{
    public class Request
    {
        public int ID { get; set; }
        public Uri URiImage { get; set; }
    }
}
