using ServiceDesk.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult GetAllCustomers()
        {
            CustomerRepository CustomerRepo = new CustomerRepository();
            return View(CustomerRepo.GetAllCustomers());
        }
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerRepository CustomerRepo = new CustomerRepository();

                    if (CustomerRepo.AddCustomer(customer))
                    {
                        ViewBag.Message = "Customer details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                CustomerRepository CustomerRepo = new CustomerRepository();
                if (CustomerRepo.DeleteCustomer(id))
                {
                    ViewBag.AlertMsg = "Customer details deleted successfully";
                }
                return RedirectToAction("GetAllCustomers");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditCustomer(int id)
        {
            CustomerRepository CustomerRepo = new CustomerRepository();

            return View(CustomerRepo.GetAllCustomers().Find(Customer => Customer.CustomerID == id));

        }

        [HttpPost]
        public ActionResult EditCustomer(Customer obj)
        {
            try
            {
                CustomerRepository CustomerRepo = new CustomerRepository();

                CustomerRepo.UpdateCustomer(obj);
                return RedirectToAction("GetAllCustomers");
            }
            catch
            {
                return View();
            }
        }
    }
}