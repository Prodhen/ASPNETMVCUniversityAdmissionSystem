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
    public class SeatPlanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var seatPlans = db.SeatPlans;
            return View(seatPlans.ToList());
        }


        public ActionResult Details(int? id)
        {
            SeatPlanViewModel SPVM = new SeatPlanViewModel();
            SeatPlan seat = db.SeatPlans.Find(id);
            SPVM.Institution = seat.Institution;
            SPVM.FloorNumber = seat.FloorNumber;
            SPVM.BuildingName = seat.BuildingName;
            SPVM.SeatPlanID = seat.SeatPlanID;
            SPVM.StudentID = seat.StudentID;

            
            return View(SPVM);
        }


        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SeatPlanViewModel SPVM)
        {


            SeatPlan seat = new SeatPlan();
             seat.Institution= SPVM.Institution;
             seat.FloorNumber= SPVM.FloorNumber;
             seat.BuildingName= SPVM.BuildingName;

             seat.StudentID= SPVM.StudentID;


            if (ModelState.IsValid)
            {
                db.SeatPlans.Add(seat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", seat.StudentID);
            return View(seat);
        }


        public ActionResult Edit(int? id)
        {
            SeatPlanViewModel SPVM = new SeatPlanViewModel();
            SeatPlan seat = db.SeatPlans.Find(id);
            SPVM.Institution = seat.Institution;
            SPVM.FloorNumber = seat.FloorNumber;
            SPVM.BuildingName = seat.BuildingName;
            SPVM.SeatPlanID = seat.SeatPlanID;
            SPVM.StudentID = seat.StudentID;


            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", seat.StudentID);
            return View(SPVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SeatPlanViewModel SPVM,int id)
        {

            SeatPlan seat = db.SeatPlans.Find(id);
            seat.Institution = SPVM.Institution;
            seat.FloorNumber = SPVM.FloorNumber;
            seat.BuildingName = SPVM.BuildingName;
            seat.StudentID = SPVM.StudentID;



            if (ModelState.IsValid)
            {
                db.Entry(seat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", seat.StudentID);
            return View(seat);
        }

        public ActionResult Delete(int? id)
        {
            SeatPlanViewModel SPVM = new SeatPlanViewModel();
            SeatPlan seat = db.SeatPlans.Find(id);
            SPVM.Institution = seat.Institution;
            SPVM.FloorNumber = seat.FloorNumber;
            SPVM.BuildingName = seat.BuildingName;
            SPVM.SeatPlanID = seat.SeatPlanID;
            SPVM.StudentID = seat.StudentID;

            return View(SPVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeatPlan seatPlan = db.SeatPlans.Find(id);
            db.SeatPlans.Remove(seatPlan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
