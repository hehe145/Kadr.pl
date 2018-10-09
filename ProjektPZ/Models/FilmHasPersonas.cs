using System.ComponentModel.DataAnnotations;

namespace ProjektPZ.Models
{
    public class FilmHasPersonas
    {
        public int ID { get; set; }
        public virtual Film Film { get; set; }
        public virtual Persona Persona { get; set; }
        [Required(ErrorMessage = "Funkcja jest wymagana")]
        public string Function { get; set; }
    }
}