
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _1262228_Arosh.Models;
using System.Data.Entity;
using System.IO;

namespace _1262228_Arosh.Controllers
{

    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();




        public ActionResult StudentProfile(Student userid)
        {
            return View(db.Students.ToList().Where(a => a.Email == userid.Email));
        }


        [ChildActionOnly]
        public ActionResult StudentResult(Student userid)
        {
            var results = db.Results.Include(r => r.Student);
            return View(results.ToList().Where(a => a.Student.Email == userid.Email));
        }

        public ActionResult StudentSeatPlan(Student userid)
        {
            var seats = db.SeatPlans.Include(r => r.Student);
            return View(seats.ToList().Where(a => a.Student.Email == userid.Email));
        }
        public ActionResult PaymentStatus(Student userid)
        {
            return View(db.Payments.ToList().Where(a=>a.Student.Email == userid.Email));
        }







        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Student student) 
        {
            //var c = db.Students.Find(pwd);
            // var v = db.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
            var name= db.Students.Where(a => a.Email == student.Email).FirstOrDefault();
            //var Password = db.Students.Where(a => a.SSCRoll == pwd).FirstOrDefault();
            var Password = db.Students.Where(a=>a.SSCRoll==student.SSCRoll.Trim()).FirstOrDefault();

            //var username = userid;

            //var username = userid;
            //var password = pwd;
          //var a=  db.Students.Find(pwd);

            //if (username == "Arosh" && password == "123456")


            if (name != null )
            {
                //return RedirectToAction("StudentProfile", new { userid });


                if (Password != null)
                {
                    return RedirectToAction("StudentProfile", new { student.Email });

                }
            }
            
            //else if(name !=null&& Password!=null)
            //{

            //    return RedirectToAction("StudentProfile", new { userid });

            //}
            
            
                   Response.Write(" <h2> Failed </h2> Invalid User...");
               return View();

           
            //if (name!=null && password == "123456")
            //   {


            //    //Response.Write("<h2> Success </h2> Valid User...");
            //    return RedirectToAction("StudentProfile",new { userid });

            //     }
            //else
            //    Response.Write(" <h2> Failed </h2> Invalid User...");
            //return View();
        }







        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            db.Configuration.ValidateOnSaveEnabled = false;

            Student student = db.Students.Find(id);
            Session["Image"] = student.Picture;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {



                if (ModelState.IsValid)
                {
                    if (student.UploadImage != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(student.UploadImage.FileName);
                        string extension = Path.GetExtension(student.UploadImage.FileName);
                        HttpPostedFileBase postedFile = student.UploadImage;



                        fileName = fileName + extension;
                        student.Picture = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        student.UploadImage.SaveAs(fileName);

                    }
                    student.Picture = Session["Image"].ToString();



                    db.Entry(student).State = EntityState.Modified;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    return RedirectToAction("login");
                }
            
            return View(student);
        }

    }
}