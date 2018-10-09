using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPZ.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Film> ListOfListOfFilms { get; set; }

    }
}