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
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjektPZ.Controllers
{
    public class BiographiesController : Controller
    {

        private ProjectDb db = new ProjectDb();

       
        // GET: Biographies
        //public ActionResult Index()
        //{
        //    return View(db.Biographies.ToList());
        //}

        // GET: Biographies/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biography biography = db.Biographies.Find(id);
            if (biography == null)
            {
                return HttpNotFound();
            }
            return View(biography);
        }

        // GET: Biographies/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            Persona persona = db.Personas.Find(id);

            if (persona == null)
            {
                return HttpNotFound();
            }

            ViewBag.FirstName = persona.FirstName;
            ViewBag.LastName = persona.LastName;
            ViewBag.ID = persona.ID;

            return View();
        }

        // POST: Biographies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Content")] Biography biography)
        {
            int id = Convert.ToInt32(TempData["ID"]);
            Persona f = db.Personas.Find(id);
            UserManager<ApplicationUser> userManager1 = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Single();
            biography.User = user;
            f.Biographys.Add(biography);
            biography.Persona = f;

            if (ModelState.IsValid)
            {
                db.Biographies.Add(biography);
                db.SaveChanges();
                return Redirect("/Personas/Details/" + id);
            }

            return View(biography);
        }



        // GET: Biographies/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biography biography = db.Biographies.Find(id);
            if (biography == null)
            {
                return HttpNotFound();
            }
            return View(biography);
        }

        // POST: Biographies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Content")] Biography biography)
        {
            if (ModelState.IsValid)
            {
                db.Biographies.Find(biography.ID).Content = biography.Content;
                db.SaveChanges();
                return Redirect("/Personas/Details/" + db.Biographies.Find(biography.ID).Persona.ID);
            }
            return View(biography);
        }

        // GET: Biographies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biography biography = db.Biographies.Find(id);
            if (biography == null)
            {
                return HttpNotFound();
            }
            return View(biography);
        }

        // POST: Biographies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Biography biography = db.Biographies.Find(id);
            int i = biography.Persona.ID;
            db.Biographies.Remove(biography);
            db.SaveChanges();
            return Redirect("/Personas/Details/" + i);
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
