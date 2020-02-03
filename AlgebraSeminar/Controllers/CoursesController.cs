using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AlgebraSeminar.Models;

namespace AlgebraSeminar.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            CurrentNumberOfStudents();
            FilledProperty();
            return View(db.Course.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,DateOfBeginning,MaxNumberOfStudents,CurrentNumberOfStudents,Filled")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,DateOfBeginning,MaxNumberOfStudents,CurrentNumberOfStudents,Filled")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Course.Find(id);
            db.Course.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public void CurrentNumberOfStudents()
        {
            int numberOfStudent = 0;
            List<int> courses = db.Course.Select(x => x.Id).ToList();
            var StudentsCourses = (from e in db.Pre_Registration
                                 where e.Status != false && e.Status != null
                                 select e.CourseId).ToList();
            for (int i = 1; i <= courses.Count; i++)
            {
                for (int j = 0; j < StudentsCourses.Count; j++)
                {
                    int student = StudentsCourses[j];
                    if (i == student)
                    {
                            numberOfStudent++;
                            db.Course.Find(i).CurrentNumberOfStudents = numberOfStudent;
                            db.SaveChanges();
                    }
                }
                numberOfStudent = 0;
            }
        }


        public void FilledProperty()
        {
            bool Filled = false;
            List<int> courses = db.Course.Select(x => x.Id).ToList();
            for(int i = 0; i < courses.Count; i++)
            {
                int course = courses[i];
                if(db.Course.Find(course).CurrentNumberOfStudents == db.Course.Find(course).MaxNumberOfStudents)
                {
                    Filled = true;
                    db.Course.Find(course).Filled = Filled;
                    db.SaveChanges();
                }
                Filled = false;
            }
        }

    }
}
