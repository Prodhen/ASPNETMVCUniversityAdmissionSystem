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
    public class UniversitieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View(db.Universities.ToList());
        }

       
    }
}
