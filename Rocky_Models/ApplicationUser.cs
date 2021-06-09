using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky_Models
{
    public class ApplicationUser : IdentityUser
    {   
        [Required]
        //khi thêm thuộc tính nào ở đây thì nó sẽ chuyển qua identityuser tức là cái cột trong register hoặc login
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public string Cmnd { get; set; }
        [NotMapped]
        public string Name2 { get; set; }

      
    }
}
