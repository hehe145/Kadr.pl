using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPZ.Models
{
    public class PersonaAll
    {
        public virtual Film Film { get; set;}
        public virtual Persona Persona { get; set; }
        public virtual List<Film> ListOfFilms { get; set; }
        public virtual List<Persona> ListOfPersona { get; set; }
        //public Comment Comment { get; set; }
        
    }
}