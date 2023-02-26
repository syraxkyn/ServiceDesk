using ServiceDesk.DAL;
using ServiceDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Controllers
{
    public class IncidentController : Controller
    {
        public ActionResult GetAllIncidents(string status)
        {
            IncidentRepository IncidentRepo = new IncidentRepository();
            IEnumerable<Incident> incidents = IncidentRepo.GetAllIncidents();
            if (status == "Все")
            {
                incidents = IncidentRepo.GetAllIncidents();
            }
            if (status == "Решенные")
            {
                incidents = IncidentRepo.GetAllIncidents().Where(x => x.TechID != null);
            }
            if (status == "Нерешенные")
            {
                incidents = IncidentRepo.GetAllIncidents().Where(x => x.TechID == null);
            }
            IncidentListViewModel ilvm = new IncidentListViewModel
            {
                Incidents = incidents,
                Statuses = new SelectList(new List<string>() { "Все", "Решенные", "Нерешенные" })
            };
            return View(ilvm);
        }
        public ActionResult AddIncident()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddIncident(Incident incident)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IncidentRepository IncidentRepo = new IncidentRepository();

                    if (IncidentRepo.AddIncident(incident))
                    {
                        ViewBag.Message = "Incident details added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteIncident(int id)
        {
            try
            {
                IncidentRepository IncidentRepo = new IncidentRepository();
                if (IncidentRepo.DeleteIncident(id))
                {
                    ViewBag.AlertMsg = "Incident details deleted successfully";
                }
                return RedirectToAction("GetAllIncidents");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult ResolveIncident(int id)
        {
            IncidentRepository IncidentRepo = new IncidentRepository();

            return View(IncidentRepo.GetAllIncidents().Find(Incident => Incident.IncidentID == id));

        }

        public ActionResult GetUnresolvedIncidents()
        {
            IncidentRepository IncidentRepo = new IncidentRepository();

            return View(IncidentRepo.GetAllIncidents().Find(Incident => Incident.TechID == null));

        }

        public ActionResult GetResolvedIncidents()
        {
            IncidentRepository IncidentRepo = new IncidentRepository();

            return View(IncidentRepo.GetAllIncidents().Find(Incident => Incident.TechID != null));

        }


        [HttpPost]
        public ActionResult ResolveIncident(Incident obj)
        {
            try
            {
                IncidentRepository IncidentRepo = new IncidentRepository();

                IncidentRepo.ResolveIncident(obj);
                return RedirectToAction("GetAllIncidents");
            }
            catch
            {
                return View();
            }
        }
    }
}