using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

namespace Hotel.Admin
{
    [Authorize]
    public class AdminPhotoController : Controller
    {
        
        private HotelEntities db = new HotelEntities();

        // GET: AdminPhoto
        public ActionResult Index()
        {
            var photo = db.Photo.Include(p => p.Rooms);
            return View(photo.ToList());
        }

        // GET: AdminPhoto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: AdminPhoto/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName");
            return View();
        }

        // POST: AdminPhoto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoId,Type,Url,RoomId")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Photo.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName", photo.RoomId);
            return View(photo);
        }

        // GET: AdminPhoto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName", photo.RoomId);
            return View(photo);
        }

        // POST: AdminPhoto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoId,Type,Url,RoomId")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName", photo.RoomId);
            return View(photo);
        }

        // GET: AdminPhoto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: AdminPhoto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photo.Find(id);
            db.Photo.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
