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

namespace ProjektPZ.Controllers
{
    public class PersonasController : Controller
    {
        private ProjectDb db = new ProjectDb();

        // GET: Personas
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.NameSortPackParam = String.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
            ViewBag.LastNamePackParam = sortOrder == "lastName" ? "lastName_desc" : "lastName";

            if (searchString != null)
            {
                page = 1;
            }else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var personas = from p in db.Personas select p;

            if (! String.IsNullOrEmpty(searchString))
            {
                personas = personas.Where(x => x.LastName.Contains(searchString) || x.FirstName.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "firstName_desc":
                    {
                        personas = personas.OrderByDescending(x => x.FirstName);
                        break;
                    }
                case "lastName":
                    {
                        personas = personas.OrderBy(x => x.LastName);
                        break;
                    }
                case "lastName_desc":
                    {
                        personas = personas.OrderByDescending(x => x.LastName);
                        break;
                    }
                default:
                    {
                        personas = personas.OrderBy(x => x.FirstName);
                        break;
                    }
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View( personas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Persona persona = db.Personas.Find(id);

            if (persona == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Film> listOfFilms = new List<Film>();

            foreach (FilmHasPersonas f in persona.ListOfFilms)
                listOfFilms.Add(db.ListOfFilms.Find(f.Film.ID));

            PersonaAll p = new PersonaAll() { Persona = persona, ListOfFilms = listOfFilms };

            return View(p);
        }

        // GET: Personas/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create( Persona persona)
        {
            HttpPostedFileBase file = Request.Files["Image"];

            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + "p_" + persona.ID + ".jpg");
                }

                return RedirectToAction("Details", new { persona.ID});
            }

            return View(persona);
        }



        // GET: Personas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Persona persona)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            int id = Convert.ToInt32(TempData["ID"]);

            if (ModelState.IsValid)
            {
                Persona p = db.Personas.Find(id);
                p.FirstName = persona.FirstName;
                p.LastName = persona.LastName;
                p.PlaceOfBirth = persona.PlaceOfBirth;
                p.BirthDate = persona.BirthDate;
                db.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + "p_" + id + ".jpg");
                }

                return Redirect("/Personas/Details/" + id);
            }
            return View(persona);
        }
       
       
        // GET: Personas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Personas.Find(id);
            List<Biography> b =(List<Biography>) persona.Biographys;
            List<FilmHasPersonas> f = (List<FilmHasPersonas>)persona.ListOfFilms;
            List<Award> a = (List<Award>)persona.Awards;

            foreach (Biography i in b.ToList())
            {
                db.Biographies.Remove(i);
            }
            foreach (FilmHasPersonas i in f.ToList())
            {
                db.FilmPersonas.Remove(i);
            }
            foreach (Award i in a.ToList())
            {
                db.Awards.Remove(i);
            }
            
            db.Personas.Remove(persona);
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
    }
}
