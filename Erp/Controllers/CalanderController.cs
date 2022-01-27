using Erp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ERP.Controllers
{
    public class CalanderController : Controller
    {
		[HttpGet]
		public ActionResult Index()
		{
			return View(new EventViewModel());
		}

		public JsonResult GetEvents(DateTime start, DateTime end)
		{
			var viewModel = new EventViewModel();
			var events = new List<EventViewModel>();
			start = DateTime.Today.AddDays(-14);
			end = DateTime.Today.AddDays(-11);

			for (var i = 1; i <= 5; i++)
			{
				events.Add(new EventViewModel()
				{
					id = i,
					title = "Event " + i,
					start = start.ToString(),
					end = end.ToString(),
					allDay = false
				});

				start = start.AddDays(7);
				end = end.AddDays(7);
			}


			return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
		}
	}
}
    

