using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDesk.Models
{
    public class IncidentListViewModel
    {
        public IEnumerable<Incident> Incidents { get;set; }
        public SelectList Statuses { get;set; }
    }
}