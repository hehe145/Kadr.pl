using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjektPZ.ViewModels;

namespace ProjektPZ.Models
{
    public class Film
    {
        public int ID { get; set; }


        [StringLength(100, ErrorMessage = "Tytuł nie może być dłuższy niż 100 znaków!")]
        public string Title { get; set; }

        [StringLength(100, ErrorMessage ="Tytuł nie może być dłuższy niż 100 znaków!")]
        [Required(ErrorMessage = "Oryginalny tytuł jest wymagany")]
        public string OrgTitle { get; set; }
        
        [Required(ErrorMessage = "Rok premiery jest wymagany")]
        public int YearOfPremiere { get; set; }

        [StringLength(500, ErrorMessage = "Opis nie może być dłuższy niż 500 znaków!")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Description { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Award> ListOfAwards { get; set; }

        [Required(ErrorMessage = "Dodaj co najmniej jedną osobistość")]
        public virtual ICollection<FilmHasPersonas> ListOfPersonas { get; set; }

        public virtual  ICollection<Review> ListOfReviews { get; set; }
        //public virtual  ICollection<Comment> ListOfComments { get; set; }

        public Film()
        {
            ListOfAwards = new List<Award>();
            //ListOfComments = new List<Comment>();
            ListOfReviews = new List<Review>();
            ListOfPersonas = new List<FilmHasPersonas>();

        }
    }
}