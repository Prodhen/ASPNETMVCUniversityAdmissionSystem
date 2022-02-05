using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using _1262228_Arosh.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PagedList;

namespace _1262228_Arosh.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult EmailisExists(string Email)
        {
            return Json(!db.Students.Any(s => s.Email == Email), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortingName = sortOrder == "Name" ? "Name_D" : "Name";
            ViewBag.SortingUnit = sortOrder == "Unit" ? "Unit_D" : "Unit";
            ViewBag.SortingGender = sortOrder == "Gender" ? "Gender_D" : "Gender";
            var studentlist = from stu in db.Students select stu;

           

            switch (sortOrder)
            {

                case "Name":
                    studentlist = studentlist.OrderBy(s => s.Name);
                    break;
                case "Name_D":
                    studentlist = studentlist.OrderByDescending(s => s.Name);
                    break;
                case "Unit":
                    studentlist = studentlist.OrderBy(s => s.Unit);
                    break;
                case "Unit_D":
                    studentlist = studentlist.OrderByDescending(s => s.Unit);
                    break;
                case "Gender":
                    db.Students.OrderBy(s => s.Gender);
                    break;
                case "Gender_D":
                    studentlist = studentlist.OrderByDescending(s => s.Gender);
                    break;
                default:

                    break;
            }



            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(studentlist.ToList().ToPagedList(pageNumber, pageSize));
        }



        public ActionResult Create()

        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {


            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(s.UploadImage.FileName);
                string extension = Path.GetExtension(s.UploadImage.FileName);
                HttpPostedFileBase postedFile = s.UploadImage;




                fileName = fileName + extension;
                s.Picture = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                s.UploadImage.SaveAs(fileName);



                db.Students.Add(s);
                db.SaveChanges();
                SendVerificationLinkEmail(s.Email);
                //TempData["msg"] = "<script>alert('succesfully!!!We send Email to Your account ');</script>";
                //TempData["msg"] = "succesfully!!!We send Email to Your account ";
                return RedirectToAction("Index");
            }


            return View(s);


        }


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
        public ActionResult Edit( Student student)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
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
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }


        public ActionResult Delete(int? id)
        {

            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        public void SendVerificationLinkEmail(string emailID)
        {



            var fromEmail = new MailAddress("aroshprodhanisdb@gmail.com", " Think Forward School");

            var toEmail = new MailAddress(emailID);
            var fromemailPassword = "aroshprodhanisdb1234@";
            string subject = "";
            string body = "";

            subject = "Your account is successfully created";
            body = "<br/><br/> we are excited to tell you that your Think Forward School is successfully create";





            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromemailPassword)

            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(message);




        }
        //private void ShowAlert(string msg)
        //{
        //    string str;
        //    str = "<script language='javascript' type='text/javascript'> alert(' " + msg + " ');</script>";
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "JS", str);

        //}






    }
}
