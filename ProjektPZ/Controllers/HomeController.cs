using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektPZ.DAL;
using ProjektPZ.ViewModels;
using ProjektPZ.Models;

namespace ProjektPZ.Controllers
{
    public class HomeController : Controller
    {
        ProjectDb db = new ProjectDb();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from film in db.ListOfFilms
                                                   group film by film.Genre into genre
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       Genre = genre.Key,
                                                       GenreCount = genre.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult Favorite()
        {
            ApplicationUser u = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Single();

            var fav = db.Favorites.Where(x => x.User.Id == u.Id);
            List<Favorite> fa = new List<Favorite>();
            foreach (var f in fav)
            {
                fa.Add((Favorite)f);
            }

            return View(fa);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Favorite f = db.Favorites.Find(id);
            db.Favorites.Remove(f);
            db.SaveChanges();

            return RedirectToAction("Favorite");
        }
    }

}