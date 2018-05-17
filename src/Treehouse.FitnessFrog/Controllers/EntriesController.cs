using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;


        //instantiated an instance of the entriesRepository class
        public EntriesController()
        {
            _entriesRepository = new EntriesRepository();
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries();

            // Calculate the total activity.
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add() //this is referencing the "Add" field on the page
        {
            return View();
        }

        [ActionName("Add"), HttpPost]      //used an attribute named "ActionName" within brackets. then set the parameter as "Add"
        public ActionResult AddPost(string date, string activityId, string duration, string intensity, string exclude, string notes)   //MVC allows us to add in the parameters to capture the input from the field  
                                                                                                                      //this process is called "Model Binding" and its just matching the parameter names with the form field names  
        {
            ViewBag.Date = date;
            ViewBag.ActivityId = activityId;
            ViewBag.Duration = duration;
            ViewBag.Intensity = intensity;
            ViewBag.Exclude = exclude;
            ViewBag.Notes = notes;
            //set the input element value attribute in the view/Add.cshtml


            return View();
        }
        public ActionResult Edit(int? id) //the ? means the parameter can have a value of null...this is referencing the "Edit" field on the page
        {
            if (id == null)   //if the id parameter is null(not defined) then return the httpstatus code error
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        public ActionResult Delete(int? id) //this is referencing the "Delete" field on the page
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}