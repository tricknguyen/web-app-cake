using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Rocky_Models
{
    public class Comment
    {
    
        [Key]
        public int Id { get; set; }
        
        public string Content { get; set; }
      

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public string CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public ApplicationUser CreatedBy { get; set; }


    }
}
