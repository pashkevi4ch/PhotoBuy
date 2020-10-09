using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PhotoBuy.DataBase
{
    public class Request
    {
        [PrimaryKey]
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
