using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class AdminsController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            try
            {
                var search_admin = db.Admins.Single(item => item.adminEmail == admin.adminEmail && item.adminPassword == admin.adminPassword);
                Session["username"] = "Admin";
                return RedirectToAction("../Home/index");
            }
            catch
            {
                ModelState.AddModelError("", "Username or Password is Wrong.");
            }

            return View(admin);
        }


        // GET: Admins/Create
        public ActionResult CreateBook()
        {
            return RedirectToAction("../Books/CreateBook");
        }

        // GET: Admins/Edit/5
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("../Books/EditBook/" + id);
        }

        // GET: Admins/Delete/5
        public ActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("../Books/DeleteBook/" + id);
        }
    }
}
