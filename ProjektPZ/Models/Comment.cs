using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektPZ.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Musisz coś napisać")]
        public string Content { get; set; }
        public virtual Film Film { get; set; }
        
        public virtual Persona Person { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}