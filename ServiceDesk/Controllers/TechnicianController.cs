using ServiceDesk.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Controllers
{
    public class TechnicianController : Controller
    {
        public ActionResult GetAllTechnicians()
        {
            TechnicianRepository TechnicianRepo = new TechnicianRepository();
            return View(TechnicianRepo.GetAllTechnicians());
        }
        public ActionResult AddTechnician()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTechnician(Technician technician)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TechnicianRepository TechnicianRepo = new TechnicianRepository();

                    if (TechnicianRepo.AddTechnician(technician))
                    {
                        ViewBag.Message = "Technician details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteTechnician(int id)
        {
            try
            {
                TechnicianRepository TechnicianRepo = new TechnicianRepository();
                if (TechnicianRepo.DeleteTechnician(id))
                {
                    ViewBag.AlertMsg = "Technician details deleted successfully";
                }
                return RedirectToAction("GetAllTechnicians");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditTechnician(int id)
        {
            TechnicianRepository TechnicianRepo = new TechnicianRepository();

            return View(TechnicianRepo.GetAllTechnicians().Find(Technician => Technician.TechID == id));

        }

        [HttpPost]
        public ActionResult EditTechnician(Technician obj)
        {
            try
            {
                TechnicianRepository TechnicianRepo = new TechnicianRepository();

                TechnicianRepo.UpdateTechnician(obj);
                return RedirectToAction("GetAllTechnicians");
            }
            catch
            {
                return View();
            }
        }
    }
}