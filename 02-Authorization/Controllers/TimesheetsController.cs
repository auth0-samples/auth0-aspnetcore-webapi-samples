using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIApplication.Controllers
{
    public class Timesheet
    {
        public DateTime Date { get; set; }
        public string Employee { get; set; }
        public float Hours { get; set; }
    }

    [Route("api/timesheets")]
    public class TimesheetsController : Controller
    {
        [Authorize("read:timesheets")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new Timesheet[] 
            {
                new Timesheet
                {
                    Date = DateTime.Now,
                    Employee = "Peter Parker",
                    Hours = 8.5F
                },
                new Timesheet
                {
                    Date = DateTime.Now.AddDays(-1),
                    Employee = "Peter Parker",
                    Hours = 7.5F
                }
            });
        }

        [Authorize("create:timesheets")]
        [HttpPost]
        public IActionResult Create(Timesheet timeheet)
        {
            return Created("http://localhost:5000/api/timeheets/1", timeheet);
        }
    }
}
