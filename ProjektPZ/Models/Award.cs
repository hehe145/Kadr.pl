using System.ComponentModel.DataAnnotations;

namespace ProjektPZ.Models
{
    public class Award
    {
        public int ID { get; set; }

        [StringLength(100, ErrorMessage = "Nazwa nagrody nie może być dłuższa niż 100 znaków!")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
        
        [Range(1800, 2050, ErrorMessage = "Niepoprawna data!")]
        [Required(ErrorMessage = "Rok otrzymania nagrody jest wymagany")]
        public int YearOfWining { get; set; }

        [StringLength(100, ErrorMessage = "Nazwa kategorii nie może być dłuższa niż 100 znaków!")]
        [Required(ErrorMessage = "W jakiej kategorii jest wymagane")]
        public string ForWhat { get; set; }
        public virtual Film Film { get; set; }
        public virtual Persona Persona { get; set; }
    }
}