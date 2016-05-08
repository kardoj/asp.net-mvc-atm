using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.Where(c=>c.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            // ViewBag'i külge saab suvalisi atribuute panna
            ViewBag.TheMessage = "Kui sul on probleeme, saada meile teade.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            ViewBag.TheMessage = "Aitäh, saime sinu teate kätte!";

            return View();
        }

        // GET: /Home/Test saadab ära /Home/About lehele
        // View ilma nimeta saadab meetodi nimega View, nimega otsib vastava View
        public ActionResult Test() {
            return View("About");
        }

        public ActionResult Serial(string letterCase) {
            var serial = "ASPNETMVC5ATM1";
            if (letterCase == "lower") {
                return Content(serial.ToLower());
            }
            return Content(serial);
        }
    }
}