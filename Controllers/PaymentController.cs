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
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Student);
            return View(payments.ToList());
        }

        public ActionResult Details(int? id)
        {

            Payment payment = db.Payments.Find(id);
 
            return View(payment);
        }


        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Payment payment)
        {

            if (ModelState.IsValid)
            {

                db.Payments.Add(payment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

           

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", payment.StudentID);
            return View(payment);
        }

        public ActionResult Edit(int? id)
        {

            Payment payment = db.Payments.Find(id);

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", payment.StudentID);
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", payment.StudentID);
            return View(payment);
        }

        public ActionResult Delete(int? id)
        {

            Payment payment = db.Payments.Find(id);

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
           
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
