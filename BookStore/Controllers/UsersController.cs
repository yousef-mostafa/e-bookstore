using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class UsersController : Controller
    {
        private BookStoreDB bookStoreDataBase = new BookStoreDB();

        // GET: Users/UserSignUp
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Users/USerSignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                Session["ID"] = user.userID;
                Session["username"] = user.userFirstName.ToString();
                bookStoreDataBase.Users.Add(user);
                bookStoreDataBase.SaveChanges();
                return RedirectToAction("../Home/Index");
            }

            return View(user);
        }

        // GET: Users/EditUserInfo
        public ActionResult EditUserInfo(int id)
        {
            return View(FindUser(id));
        }

        // POST: Users/EditUserInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo(User user, string userImage)
        {
            if (ModelState.IsValid)
            {
                user.userImage = CheckPhoto(user, userImage);
                bookStoreDataBase.Entry(user).State = EntityState.Modified;
                bookStoreDataBase.SaveChanges();
                return RedirectToAction("../Home/Index");
            }
            return View(user);
        }

        //Get: Users/FindUser
        public User FindUser(int id)
        {
            User user = bookStoreDataBase.Users.Find(id);
            return user;
        }
    }
}