using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky_Models.ViewModel
{
    public class CartVM
    {      
        public CartVM()
        {
            Coupon = new Coupon();
            ProdList = new List<Product>();
            Total = new int();
        }
        public List<Product> ProdList { get; set; }
        public Coupon Coupon { get; set; }

        public double Total { get; set; }
    }
}
