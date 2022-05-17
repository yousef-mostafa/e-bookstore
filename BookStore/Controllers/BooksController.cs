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
    public class BooksController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("../Home/index");
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(Book book, string bookImage)
        {
            if (ModelState.IsValid)
            {
                book.bookImage = CheckPhoto(book, bookImage);
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult DeleteBook(int id)
        {
            return View(GetBook(id));
        }


        // POST: Books/Delete/5
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookConfirm(int id)
        {
            db.Books.Remove(GetBook(id));
            db.SaveChanges();
            return RedirectToAction("../Home/Index");
        }

        //to get the spicsific book 
        private Book GetBook(int? id)
        {
            return (db.Books.Find(id));
        }

        //GET: Users/CheckPhoto
        public string CheckPhoto(Book Book, string bookImage)
        {
            if (bookImage != null)
            {
                return bookImage;
            }
            else
            {
                return Book.bookImage;
            }
        }
    }
}
