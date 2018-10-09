using Microsoft.AspNet.Identity.EntityFramework;
using ProjektPZ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProjektPZ.DAL
{
    public class ProjectDb : DbContext
    {
        public ProjectDb() : base("DefaultConnection")
            { }
        //public DbSet<Comment> Comments { get; set; }
        public DbSet<Biography> Biographies { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<FilmHasPersonas> FilmPersonas { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Film> ListOfFilms { get; set; }

        public DbSet<Persona>Personas { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });


            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    
}