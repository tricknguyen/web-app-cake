using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky_Models.ViewModel
{
    public class DetailsVM
    {
        //Khi nhận đc tt details nó sẽ tự động khởi tạo Product với object Product 
        //ctor
        public Product Product { get; set; }
        public DetailsVM()
        {
            Product = new Product();
        }//k cần làm trong controller
        
        public bool ExistsInCart { get; set; }
    }
}
