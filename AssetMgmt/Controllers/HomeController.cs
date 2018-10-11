using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetMgmt.DAL;
using AssetMgmt.ViewModels;

namespace AssetMgmt.Controllers
{
    public class HomeController : Controller
    {
        private AssetMgmtContext db = new AssetMgmtContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<AssetNameGroup> data = from assetmanagements in db.AssetManagements
                                              group assetmanagements by assetmanagements.Employee.EmployeeName into assetGroup
                                              select new AssetNameGroup()
                                              {
                                                  EmployeeName = assetGroup.Key,
                                                  AssetCount = assetGroup.Count()
                                              };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}