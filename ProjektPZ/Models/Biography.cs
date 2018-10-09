using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPZ.Models
{
    public class Biography
    {
        public int ID { get; set; }

        [StringLength(4000, ErrorMessage = "Biografia nie może być dłuższy niż 4000 znaków!")]
        [Required(ErrorMessage = "Musisz coś napisać")]
        public string Content { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual Persona Persona { get; set; }

    }
}