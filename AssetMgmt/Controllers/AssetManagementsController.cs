using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssetMgmt.DAL;
using AssetMgmt.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace AssetMgmt.Controllers
{
    public class AssetManagementsController : Controller
    {
        private AssetMgmtContext db = new AssetMgmtContext();

        // GET: AssetManagements
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.AssetNameSortParm = String.IsNullOrEmpty(sortOrder) ? "assetname_desc" : "";
            ViewBag.EmployeeSortParm = string.IsNullOrEmpty(sortOrder) ? "empname_desc" : "empname_asc";
            

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var assetmanagements = from s in db.AssetManagements
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assetmanagements = assetmanagements.Where(s => s.Asset.AssetName.Contains(searchString)
                                       || s.Employee.EmployeeName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "assetname_desc":
                    assetmanagements = assetmanagements.OrderByDescending(s => s.Asset.AssetName);
                    break;
                case "empname_asc":
                    assetmanagements = assetmanagements.OrderBy(s => s.Employee.EmployeeName);
                    break;
                case "empname_desc":
                    assetmanagements = assetmanagements.OrderByDescending(s => s.Employee.EmployeeName);
                    break;
                default:  // Name ascending 
                    assetmanagements = assetmanagements.OrderBy(s => s.Asset.AssetName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(assetmanagements.ToPagedList(pageNumber, pageSize));
        }

        // GET: AssetManagements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetManagement assetManagement = db.AssetManagements.Find(id);
            if (assetManagement == null)
            {
                return HttpNotFound();
            }
            return View(assetManagement);
        }

        // GET: AssetManagements/Create
        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "EmployeeName");
            return View();
        }

        // POST: AssetManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmployeeID,AssetID,modelNumber")] AssetManagement assetManagement)
        {
            if (ModelState.IsValid)
            {
                db.AssetManagements.Add(assetManagement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetName", assetManagement.AssetID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "EmployeeName", assetManagement.EmployeeID);
            return View(assetManagement);
        }

        // GET: AssetManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetManagement assetManagement = db.AssetManagements.Find(id);
            if (assetManagement == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetName", assetManagement.AssetID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "EmployeeName", assetManagement.EmployeeID);
            return View(assetManagement);
        }

        // POST: AssetManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeID,AssetID,modelNumber")] AssetManagement assetManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetManagement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetName", assetManagement.AssetID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "EmployeeName", assetManagement.EmployeeID);
            return View(assetManagement);
        }

        // GET: AssetManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetManagement assetManagement = db.AssetManagements.Find(id);
            if (assetManagement == null)
            {
                return HttpNotFound();
            }
            return View(assetManagement);
        }

        // POST: AssetManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetManagement assetManagement = db.AssetManagements.Find(id);
            db.AssetManagements.Remove(assetManagement);
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
