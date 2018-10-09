using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektPZ.DAL;
using ProjektPZ.Models;
using PagedList;
using System.Globalization;

namespace ProjektPZ.Controllers
{
    public class FilmsController : Controller
    {
        private ProjectDb db = new ProjectDb();

        // GET: ListOfFilms

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, int? genre)
        {
            ViewBag.NameSortPackParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DatePackParam = sortOrder == "date" ? "date_desc" : "date";

            if (genre == null)
                genre = -1;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var films = from p in db.ListOfFilms select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (genre != -1)
                {
                    films = films.Where(x => (x.Title.Contains(searchString) || x.OrgTitle.Contains(searchString)) && (x.Genre.ID == genre));
                }else
                    films = films.Where(x => x.Title.Contains(searchString) || x.OrgTitle.Contains(searchString));

            }else
            {
                if (genre != -1)
                    films = films.Where(x => x.Genre.ID == genre);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    {
                        films = films.OrderByDescending(x => x.Title);
                        break;
                    }
                case "date":
                    {
                        films = films.OrderBy(x => x.YearOfPremiere);
                        break;
                    }
                case "date_desc":
                    {
                        films = films.OrderByDescending(x => x.YearOfPremiere);
                        break;
                    }
                default:
                    {
                        films = films.OrderBy(x => x.Title);
                        break;
                    }
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(films.ToPagedList(pageNumber, pageSize));
        }

        // GET: ListOfFilms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.ListOfFilms.Find(id);

            bool fav = db.Favorites.Where(x => x.Film.ID == film.ID && x.User.UserName.Equals(User.Identity.Name)).Any();
            if (fav)
            {
                ViewBag.ID = db.Favorites.Where(x => x.Film.ID == film.ID && x.User.UserName.Equals(User.Identity.Name)).Single().ID;
            }
            else
                ViewBag.ID = -1;

            if (film == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            List<Persona> listOfPersonas = new List<Persona>();

            foreach (FilmHasPersonas f in film.ListOfPersonas)
            {
                listOfPersonas.Add(f.Persona);
            }


            PersonaAll p = new PersonaAll() { ListOfPersona = listOfPersonas, Film = film };

            return View(p);
        }

        // GET: ListOfFilms/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }



        // POST: ListOfFilms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,OrgTitle,YearOfPremiere,Description,ListOfGenres,ListOfAwards,ListOfPersonas")] Film film)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            int id = Convert.ToInt32(TempData["ID"]);

            if (ModelState.IsValid)
            {
                db.ListOfFilms.Add(film);
                db.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + "f_" + id + ".jpg");
                }

                return Redirect("/Films/Details/" + id);
            }

            return View(film);
        }

        // GET: ListOfFilms/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.ListOfFilms.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: ListOfFilms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Film film)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            int id = Convert.ToInt32(TempData["ID"]);

            if (ModelState.IsValid)
            {
                Film f = db.ListOfFilms.Find(id);
                f.Description = film.Description;
                f.Title = film.Title;
                f.OrgTitle = film.OrgTitle;
                f.YearOfPremiere = film.YearOfPremiere;

                db.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + "f_" + id + ".jpg");
                }

                return Redirect("/Films/Details/" + id);
            }
            return View(film);
        }

        // GET: ListOfFilms/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.ListOfFilms.Find(id);

            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: ListOfFilms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.ListOfFilms.Find(id);
            List<Award> a = (List<Award>)film.ListOfAwards;
            List<Review> b = (List<Review>)film.ListOfReviews;
            List<FilmHasPersonas> c = (List<FilmHasPersonas>)film.ListOfPersonas;

            foreach (Award i in a.ToList())
            {
                db.Awards.Remove(i);
            }
            foreach (Review i in b.ToList())
            {
                db.Reviews.Remove(i);
            }
            foreach (FilmHasPersonas i in c.ToList())
            {
                db.Personas.Find(i.Persona.ID).ListOfFilms.Remove(i);
            }
            foreach (FilmHasPersonas i in c.ToList())
            {
                db.FilmPersonas.Remove(i);
            }
            db.ListOfFilms.Remove(film);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private Persona DefaultPersona(string firstName, string lastName)
        {
            return new Persona() { FirstName = firstName, LastName = lastName, BirthDate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", CultureInfo.InvariantCulture), PlaceOfBirth = "?" };
        }

        [Authorize]
        public ActionResult AddRating(int id, int ranting)
        {
            Film f = db.ListOfFilms.Find(id);
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Single();

            if (db.Ratings.Where(x => x.Film.ID == f.ID && x.User.Id == user.Id).Any())
            {
                db.Ratings.Where(x => x.Film.ID == f.ID && x.User.Id == user.Id).Single().Rate = ranting * 10;
            }else
            {
                Rating r = new Rating() { Film = f, Rate = (ranting * 10), User = user };
                db.Ratings.Add(r);
            }

           
            db.SaveChanges();

            return Redirect("/Films/Details/" + id);
        }

        [Authorize]
        public ActionResult RemoveFromFavorite(int id)
        {
            Favorite f = db.Favorites.Find(id);
            int i = f.Film.ID;
            db.Favorites.Remove(f);
            db.SaveChanges();

            return Redirect("/Films/Details/" + i);

        }
        [Authorize]
        public ActionResult AddToFavorite(int id)
        {
            Favorite f = new Favorite()
            {
                Film = db.ListOfFilms.Find(id),
                User = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Single()
            };
            db.Favorites.Add(f);
            db.SaveChanges();
            
            return Redirect("/Films/Details/" + id);
        }
    }
}
