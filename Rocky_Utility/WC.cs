using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky_Utility
{
    public static class WC
    {
        public const string ImagePath = @"\image\product\";
        public const string SessionCart = "ShoppingCartSession"; //key để truy cập session
        public const string SessionInquiryId = "InquirySession";


        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";

        public const string CategoryName = "Category";
        public const string ApplicationTypeName = "ApplicationType";

        public const string Sucess = "Sucess";
        public const string Error = "Error";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShiped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        //chỉ được đọc bên trong List đc chỉ định
        public static readonly IEnumerable<string> listStatus = new ReadOnlyCollection<string>
        (
            new List<string>
            {
                StatusApproved, StatusPending, StatusInProcess, StatusShiped, StatusCancelled, StatusRefunded
            }
        );
        
    }
}
