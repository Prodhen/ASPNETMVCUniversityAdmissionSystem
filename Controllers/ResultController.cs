using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _1262228_Arosh.Models;

namespace _1262228_Arosh.Controllers
{
    public class ResultController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
      
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Student);
            return View(results.ToList());
        }


        public ActionResult Details(int? id)
        {

            Result result = db.Results.Find(id);
            return View(result);
        }


        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", result.StudentID);
            return View(result);
        }


        public ActionResult Edit(int? id)
        {

            Result result = db.Results.Find(id);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", result.StudentID);
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", result.StudentID);
            return View(result);
        }


        public ActionResult Delete(int? id)
        {

            Result result = db.Results.Find(id);
            return View(result);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
