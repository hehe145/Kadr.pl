using System;
using System.ComponentModel.DataAnnotations;
using ProjektPZ.Models;


namespace ProjektPZ.ViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public virtual Genre Genre { get; set; }

        public int GenreCount { get; set; }
    }
}