using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rocky_Models
{
    public class Message
    {
      
        [Key]
        public int Id { get; set; }
        [Required]
        public string  UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }

        public string UserID { get; set; }  

        public virtual AppUser Sender { get; set; }
    }
}
