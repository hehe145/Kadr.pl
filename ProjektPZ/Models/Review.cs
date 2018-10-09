using System.ComponentModel.DataAnnotations;

namespace ProjektPZ.Models
{
    public class Review
    {
        public int ID { get; set; }

        [StringLength(4000, ErrorMessage = "Recenzja nie może być dłuższa niż 4000 znaków!")]
        [Required(ErrorMessage = "Musisz coś napisać")]
        public string Content { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Film Film { get; set; }

    }
}