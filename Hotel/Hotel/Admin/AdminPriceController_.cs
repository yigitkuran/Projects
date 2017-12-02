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
    public class AdminPriceController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: AdminPrice
        public ActionResult Index()
        {
            var price = db.Price.Include(p => p.Rooms);
            return View(price.ToList());
        }

        // GET: AdminPrice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // GET: AdminPrice/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName");
            return View();
        }

        // POST: AdminPrice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceId,Charge,Term,Month,Day,RoomId,Currency")] Price price)
        {
            if (ModelState.IsValid)
            {
                db.Price.Add(price);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName", price.RoomId);
            return View(price);
        }

        // GET: AdminPrice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName", price.RoomId);
            return View(price);
        }

        // POST: AdminPrice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceId,Charge,Term,Month,Day,RoomId,Currency")] Price price)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomName", price.RoomId);
            return View(price);
        }

        // GET: AdminPrice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // POST: AdminPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Price price = db.Price.Find(id);
            db.Price.Remove(price);
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
