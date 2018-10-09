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
using System.Globalization;

namespace ProjektPZ.Controllers
{
    public class FilmHasPersonasController : Controller
    {
        private ProjectDb db = new ProjectDb();

        // GET: FilmHasPersonas
        public ActionResult Index()
        {
            return View(db.FilmPersonas.ToList());
        }

        // GET: FilmHasPersonas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmHasPersonas filmHasPersonas = db.FilmPersonas.Find(id);
            if (filmHasPersonas == null)
            {
                return HttpNotFound();
            }
            return View(filmHasPersonas);
        }

        // GET: FilmHasPersonas/Create
        [Authorize]
        public ActionResult CreateF(int idf)
        {
            FilmHasPersonas fHP = new FilmHasPersonas();
            Film f = db.ListOfFilms.Find(idf);

            if (f == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
                fHP.Film = f;

            fHP.Persona = new Persona();

            return View(fHP);
        }
        [Authorize]
        public ActionResult CreateP(int idp)
        {
            FilmHasPersonas fHP = new FilmHasPersonas();
            Persona p = db.Personas.Find(idp);

            if (p == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
                fHP.Persona = p;

            fHP.Film = new Film();

            return View(fHP);
        }

        // POST: FilmHasPersonas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateF(FilmHasPersonas filmHasPersonas)
        {
            int idf = Convert.ToInt32(TempData["IDF"]);

            string firstName = filmHasPersonas.Persona.FirstName;
            string lastName = filmHasPersonas.Persona.LastName;

            bool p = db.Personas.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).Any();

            if (!p)
                filmHasPersonas.Persona = DefaultPersona(firstName, lastName);
            else
                filmHasPersonas.Persona = db.Personas.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).Single();

            Film f = db.ListOfFilms.Find(idf);

            if (f == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
                filmHasPersonas.Film = f;

            if (ModelState.IsValid)
            {
                db.FilmPersonas.Add(filmHasPersonas);
                db.SaveChanges();
                return Redirect("/Films/Details/" + idf);
            }

            return View(filmHasPersonas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateP(FilmHasPersonas filmHasPersonas)
        {
            int idp = Convert.ToInt32(TempData["IDP"]);

            string title = filmHasPersonas.Film.Title;
            int yearOfPremiere = filmHasPersonas.Film.YearOfPremiere;

            bool p = db.ListOfFilms.Where(x => x.Title.Equals(title) && x.YearOfPremiere == yearOfPremiere).Any();

            if (!p)
                filmHasPersonas.Film = new Film() { Title = title, OrgTitle = title, Description = "_", YearOfPremiere = yearOfPremiere , Genre= new Genre() { Name ="?"} };
            else
                filmHasPersonas.Film = db.ListOfFilms.Where(x => x.Title.Equals(title) && x.YearOfPremiere == yearOfPremiere).Single();

            Persona pers = db.Personas.Find(idp);

            if (pers == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
                filmHasPersonas.Persona = pers;

            if (ModelState.IsValid)
            {
                db.FilmPersonas.Add(filmHasPersonas);
                db.SaveChanges();
                return Redirect("/Personas/Details/" + idp);
            }

            return View(filmHasPersonas);
        }
        // GET: FilmHasPersonas/Edit/5
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmHasPersonas filmHasPersonas = db.FilmPersonas.Find(id);
            if (filmHasPersonas == null)
            {
                return HttpNotFound();
            }
            return View(filmHasPersonas);
        }

        // POST: FilmHasPersonas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Function")] FilmHasPersonas filmHasPersonas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filmHasPersonas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filmHasPersonas);
        }

        // GET: FilmHasPersonas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmHasPersonas filmHasPersonas = db.FilmPersonas.Find(id);
            if (filmHasPersonas == null)
            {
                return HttpNotFound();
            }
            return View(filmHasPersonas);
        }

        // POST: FilmHasPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            FilmHasPersonas filmHasPersonas = db.FilmPersonas.Find(id);
            db.FilmPersonas.Remove(filmHasPersonas);
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
    }
}
