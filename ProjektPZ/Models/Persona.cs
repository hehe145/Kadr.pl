using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProjektPZ.Models
{
    public class Persona
    {
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Imie nie może być dłuższe niż 50 znaków!")]
        [Required(ErrorMessage = "Imie jest wymagane")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków!")]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }
        [StringLength(200, ErrorMessage = "Miejsce urodzenia nie może być dłuższe niż 200 znaków")]
        [Required(ErrorMessage = "Miejsce urodzenia jest wymaganee")]
        public string PlaceOfBirth { get; set; }

        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{d:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
        public virtual ICollection<Biography> Biographys { get; set; }
        public virtual ICollection<FilmHasPersonas> ListOfFilms { get; set; }
       

        public Persona()
        {
            ListOfFilms = new List<FilmHasPersonas>();
            Awards = new List<Award>();
            Biographys = new List<Biography>();
            //Comments = new List<Comment>();
            
        }
    }
}