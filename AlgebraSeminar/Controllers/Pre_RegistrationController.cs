using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlgebraSeminar.Models;

namespace AlgebraSeminar.Controllers
{
    public class Pre_RegistrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var pre_Registration = db.Pre_Registration.Include(p => p.Course);
            return View(pre_Registration.ToList());
        }

        public ActionResult IndexAccepted()
        {
            var pre_Registration = db.Pre_Registration.Include(p => p.Course);
            return View(pre_Registration.ToList().Where(x => x.Status == true));
        }

        public ActionResult IndexDeclineded()
        {
            var pre_Registration = db.Pre_Registration.Include(p => p.Course);
            return View(pre_Registration.ToList().Where(x => x.Status != true && x.Status != null));
        }

        public ActionResult IndexUncommitted()
        {
            var pre_Registration = db.Pre_Registration.Include(p => p.Course);
            return View(pre_Registration.ToList().Where(x => x.Status == null));
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pre_Registration pre_Registration = db.Pre_Registration.Find(id);
            if (pre_Registration == null)
            {
                return HttpNotFound();
            }
            return View(pre_Registration);
        }

        
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.Filled != true), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Status,CourseId")] Pre_Registration pre_Registration)
        {
            if (ModelState.IsValid)
            {
                db.Pre_Registration.Add(pre_Registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.Filled != true), "Id", "Name", pre_Registration.CourseId);
            return View(pre_Registration);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pre_Registration pre_Registration = db.Pre_Registration.Find(id);
            if (pre_Registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.Filled != true), "Id", "Name", pre_Registration.CourseId);
            return View(pre_Registration);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Status,CourseId")] Pre_Registration pre_Registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pre_Registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Course.Where(x => x.Filled != true), "Id", "Name", pre_Registration.CourseId);
            return View(pre_Registration);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pre_Registration pre_Registration = db.Pre_Registration.Find(id);
            if (pre_Registration == null)
            {
                return HttpNotFound();
            }
            return View(pre_Registration);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pre_Registration pre_Registration = db.Pre_Registration.Find(id);
            db.Pre_Registration.Remove(pre_Registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
