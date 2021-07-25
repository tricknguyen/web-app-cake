using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky_Models.ViewModel
{
    public class MyorderVM
    {   
        public MyorderVM()
        {
            ProductList = new List<Product>();
            OrderHList = new List<OrderHeader>();
            OrderDList = new List<OrderDetail>();
            ApplicationUser = new ApplicationUser();
        }
        public List<Product> ProductList { get; set; }
        public List<OrderHeader> OrderHList { get; set; }
        public List<OrderDetail> OrderDList { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
