using ASP.NET_HoangCongSon.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_HoangCongSon.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity {  get; set; }
    }
}