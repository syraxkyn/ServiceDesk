using ServiceDesk.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult GetAllRegistrations()
        {
            RegistrationRepository RegistrationRepo = new RegistrationRepository();
            return View(RegistrationRepo.GetAllRegistrations());
        }
        public ActionResult AddRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRegistration(Registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegistrationRepository RegistrationRepo = new RegistrationRepository();

                    if (RegistrationRepo.AddRegistration(registration))
                    {
                        ViewBag.Message = "Registration details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteRegistration(int customerid,int productcode)
        {
            try
            {
                RegistrationRepository RegistrationRepo = new RegistrationRepository();
                if (RegistrationRepo.DeleteRegistration(customerid, productcode))
                {
                    ViewBag.AlertMsg = "Registration details deleted successfully";
                }
                return RedirectToAction("GetAllRegistrations");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditRegistration(int customerId, int productId)
        {
            RegistrationRepository RegistrationRepo = new RegistrationRepository();

            return View(RegistrationRepo.GetAllRegistrations().Find(Registration => Registration.ProductCode == productId && Registration.CustomerID==customerId));

        }

        [HttpPost]
        public ActionResult EditRegistration(Registration obj)
        {
            try
            {
                RegistrationRepository RegistrationRepo = new RegistrationRepository();

                RegistrationRepo.UpdateRegistration(obj);
                return RedirectToAction("GetAllRegistrations");
            }
            catch
            {
                return View();
            }
        }
    }
}