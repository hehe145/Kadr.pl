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
    public class AwardsController : Controller
    {
        private ProjectDb db = new ProjectDb();

        //GET: Awards
        //public ActionResult Index()
        //{
        //    return View(db.Awards.ToList());
        //}

        ////GET: Awards/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Award award = db.Awards.Find(id);
        //    if (award == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(award);
        //}

        // GET: Awards/Create
        [Authorize]
        public ActionResult CreateFAward(int idf)
        {
            Film f = null;

            Award award = new Award();

            f = db.ListOfFilms.Find( idf);

            if (f == null)
                award.Film = new Film();
            else
                award.Film = f;

            award.Persona = new Persona();

            return View(award);
        }


        [Authorize]
        public ActionResult CreatePAward(int idp)
        {
            Persona p = null;

            Award award = new Award();

            p = db.Personas.Find(idp);

            if (p == null)
                award.Persona = new Persona();
            else
                award.Persona = p;

            award.Film = new Film();

            return View(award);
        }
        // POST: Awards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateFAward( Award award)
        {
            int idf = Convert.ToInt32(TempData["IDF"]);

            string firstName = award.Persona.FirstName;
            string lastName = award.Persona.LastName;

            bool p = db.Personas.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).Any();

            if (!p)
                award.Persona = DefaultPersona(firstName, lastName);
            else
                award.Persona = db.Personas.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).Single();

            Film f = db.ListOfFilms.Find(idf);

            if (f == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
                award.Film = f;

            if (ModelState.IsValid)
            {
                db.Awards.Add(award);
                db.SaveChanges();
                return Redirect("/Films/Details/" + idf);
            }

            return View(award);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreatePAward(Award award)
        {
            int idp = Convert.ToInt32(TempData["IDP"]);

            string title = award.Film.Title;
            int yearOfPremiere = award.Film.YearOfPremiere;

            bool p = db.ListOfFilms.Where(x => x.Title.Equals(title) && x.YearOfPremiere == yearOfPremiere).Any();

            if (!p)
                award.Film = new Film() { Title = title, OrgTitle = title, Description = "_" , YearOfPremiere= yearOfPremiere, Genre = new Genre() { Name = "?" } };
            else
                award.Film = db.ListOfFilms.Where(x => x.Title.Equals(title) && x.YearOfPremiere == yearOfPremiere).Single();

            Persona pers = db.Personas.Find(idp);

            if (pers == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
                award.Persona = pers;

            if (ModelState.IsValid)
            {
                db.Awards.Add(award);
                db.SaveChanges();
                return Redirect("/Personas/Details/" + idp);
            }

            return View(award);
        }
        private Persona DefaultPersona(string firstName, string lastName)
        {
            return new Persona() { FirstName = firstName, LastName = lastName, BirthDate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", CultureInfo.InvariantCulture), PlaceOfBirth = "?" };
        }

        // GET: Awards/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // POST: Awards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Name,YearOfWining,ForWhat")] Award award)
        {
            if (ModelState.IsValid)
            {
                db.Entry(award).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(award);
        }

        // GET: Awards/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Award award = db.Awards.Find(id);
            db.Awards.Remove(award);
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
