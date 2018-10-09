using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPZ.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Film Film { get; set; }
        public int Rate { get; set; }
    }
}