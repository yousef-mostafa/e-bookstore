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
    public class UserOrderedBooksController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: UserOrderedBooks
        public ActionResult Index(int id)
        {
            var userOrderedBooks = db.UserOrderedBooks.Where(c => c.userId == id);
            return View(userOrderedBooks);
        }


        // GET: UserOrderedBooks/Add to cart
        public ActionResult AddToCart(int? id)
        {
            if (id != null)
                ViewBag.bookId = new SelectList(db.Books, "bookId", "bookTitle", id);
            else
                ViewBag.bookId = new SelectList(db.Books, "bookId", "bookTitle");
            return View();
        }

        // POST: UserOrderedBooks/Add to cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(UserOrderedBook userOrderedBook)
        {
            if (ModelState.IsValid)
            {
                userOrderedBook.userId = Convert.ToInt32(Session["ID"]);
                db.UserOrderedBooks.Add(userOrderedBook);
                db.SaveChanges();
                return RedirectToAction("Index/" + Session["ID"]);
            }

            ViewBag.bookId = new SelectList(db.Books, "bookId", "bookTitle", userOrderedBook.bookId);
            return View(userOrderedBook);
        }


        // GET: UserOrderedBooks/norder
        public ActionResult UnOrder(int id)
        {
            return View(FindOrder(id));
        }

        // POST: UserOrderedBooks/Delete/5
        [HttpPost, ActionName("UnOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserOrderedBook userOrderedBook = db.UserOrderedBooks.Find(id);
            db.UserOrderedBooks.Remove(userOrderedBook);
            db.SaveChanges();
            return RedirectToAction("../Books/Index");
        }

        //GET: FindOrder
        public UserOrderedBook FindOrder(int id)
        {
            UserOrderedBook userOrderedBook = db.UserOrderedBooks.Find(id);
            return userOrderedBook;
        }
    }
}
