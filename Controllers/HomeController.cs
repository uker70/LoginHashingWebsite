using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoginHashing;

namespace LoginHashingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Fail()
        {
            return "Out of attempts";
        }

        [HttpPost]
        public ActionResult UserLogin()
        {
            if (Login.attempts != 2)
            {
                string username = Request["username"];
                string password = Request["password"];
                bool test = Login.VerifyLogin(username, password);
                if (test)
                {
                    
                    Login.attempts = 0;
                    Login.loggedIn = test;
                    return Redirect("https://localhost:44363/Welcome");
                }
                else
                {
                    Login.loggedIn = test;
                    Login.attempts++;
                }
            }
            else
            {
                return RedirectToAction("Fail");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UserCreate()
        {
            string username = Request["username"];
            string password = Request["password"];
            Login.CreateUser(username, password);

            return RedirectToAction("Done");
        }

        public ActionResult Done()
        {
            Thread.Sleep(5000);
            return RedirectToAction("Index");
        }
    }
}