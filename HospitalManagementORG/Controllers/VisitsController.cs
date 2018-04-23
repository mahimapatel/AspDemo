using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagementORG.Models;

namespace HospitalManagementORG.Controllers
{
    public class VisitsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Visits
        public ActionResult Index()
        {
            var tblVisits = db.tblVisits.Include(t => t.tblDoctor).Include(t => t.tblPatient);
            return View(tblVisits.ToList());
        }

        // GET: Visits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVisit tblVisit = db.tblVisits.Find(id);
            if (tblVisit == null)
            {
                return HttpNotFound();
            }
            return View(tblVisit);
        }

        // GET: Visits/Create
        public ActionResult Create()
        {
            ViewBag.DoctId = new SelectList(db.tblDoctors, "Id", "Name");
            ViewBag.PatientId = new SelectList(db.tblPatients, "Id", "Name");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DoctId,PatientId,Complaint,DateOfVisit")] tblVisit tblVisit)
        {
            if (ModelState.IsValid)
            {
                db.tblVisits.Add(tblVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctId = new SelectList(db.tblDoctors, "Id", "Name", tblVisit.DoctId);
            ViewBag.PatientId = new SelectList(db.tblPatients, "Id", "Name", tblVisit.PatientId);
            return View(tblVisit);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVisit tblVisit = db.tblVisits.Find(id);
            if (tblVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctId = new SelectList(db.tblDoctors, "Id", "Name", tblVisit.DoctId);
            ViewBag.PatientId = new SelectList(db.tblPatients, "Id", "Name", tblVisit.PatientId);
            return View(tblVisit);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DoctId,PatientId,Complaint,DateOfVisit")] tblVisit tblVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctId = new SelectList(db.tblDoctors, "Id", "Name", tblVisit.DoctId);
            ViewBag.PatientId = new SelectList(db.tblPatients, "Id", "Name", tblVisit.PatientId);
            return View(tblVisit);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVisit tblVisit = db.tblVisits.Find(id);
            if (tblVisit == null)
            {
                return HttpNotFound();
            }
            return View(tblVisit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVisit tblVisit = db.tblVisits.Find(id);
            db.tblVisits.Remove(tblVisit);
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
