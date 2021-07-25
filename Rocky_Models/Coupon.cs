using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Rocky_Models
{
    public class Coupon
    {   
        [Key]
        public int Id { get; set; }

        public string CouponCode { get; set; }
        public int Discount { get; set; }
        public int MaxDiscount { get; set; }
        public DateTime ExpirationDate { get; set; }


    }
}
