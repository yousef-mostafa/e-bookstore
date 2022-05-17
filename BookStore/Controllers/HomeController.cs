using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        public ActionResult Index()
        {
            return RedirectToAction("../Books/Index");
        }


        //Logout for all persons (admin & user)
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("../Home/index");
        }
    }
}