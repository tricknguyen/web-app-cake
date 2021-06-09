using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Rocky_Models
{
    public class Product
    {
        public Product()
        {
            TempSqFt = 1;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,int.MaxValue)]
        public double Price { get; set; }
        public string Image { get; set; }


        [Display(Name ="Category Type")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category{ get; set; }

        [Display(Name = "Application Type")]
        public int ApplicationTypeID { get; set; }
        [ForeignKey("ApplicationTypeID")]
        public virtual ApplicationType ApplicationType { get; set; }

        [NotMapped] //giups cho temsqft khi add minigration k bi pushe vao database
        [Range(1,1000, ErrorMessage = "Sqft must be greater than 0")]
        public int TempSqFt { get; set; }
    }
}
