using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AssetMgmt.DAL;
using AssetMgmt.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace AssetMgmt.Controllers
{
    public class EmployeesController : Controller
    {
        private AssetMgmtContext db = new AssetMgmtContext();

        // GET: Employees
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EmpNameSortParm = String.IsNullOrEmpty(sortOrder) ? "empname_desc" : "";
            ViewBag.DesigNameSortParm = String.IsNullOrEmpty(sortOrder) ? "designame_desc" : "designame_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var employees = from s in db.Employees
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.EmployeeName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "empname_desc":
                    employees = employees.OrderByDescending(s => s.EmployeeName);
                    break;
                case "designame_asc":
                    employees = employees.OrderBy(s => s.Designation.DesignationName);
                    break;
                case "designame_desc":
                    employees = employees.OrderByDescending(s => s.Designation.DesignationName);
                    break;
                default:  // Name ascending 
                    employees = employees.OrderBy(s => s.EmployeeName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DesignationID = new SelectList(db.Designations, "ID", "DesignationName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeName, DesignationID")]Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.Employees.Any(ac => ac.EmployeeName.Equals(employee.EmployeeName)))
                    {
                        ModelState.AddModelError(string.Empty,"Already exists");

                        return RedirectToAction("Create");

                    }
                    else
                    {
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationID = new SelectList(db.Designations, "ID", "DesignationName", employee.DesignationID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = db.Employees.Find(id);
            if (TryUpdateModel(employeeToUpdate, "",
               new string[] { "EmployeeName","DesignationID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(employeeToUpdate);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
