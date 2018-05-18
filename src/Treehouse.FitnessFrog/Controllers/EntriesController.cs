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

        [ActionName("Add"), HttpPost]      //used an attribute named "ActionName" within brackets. then set the parameter as "Add"....also used the attribute of "HttpPost" 
        public ActionResult AddPost(DateTime? date, int? activityId, double? duration, Entry.IntensityLevel? intensity, bool? exclude, string notes)   //MVC allows us to add in the parameters to capture the input from the field  
                                                                                                                      //this process is called "Model Binding" and its just matching the parameter names with the form field names  
        {

            //to parse the string Date into an integer, we first create the variable to store te conversion into
            //DateTime dateValue;
            //DateTime.TryParse(date, out dateValue);
            //or you can change the parameter data types passed in inside of the above method


            //the AttemptedValue property will check the parameters to see if they're formatted correctly...ex: date/time has to be submitted as 1/1/2011 12:00
            ViewBag.Date = ModelState["Date"].Value.AttemptedValue;
            ViewBag.ActivityId = ModelState["ActivityId"].Value.AttemptedValue;
            ViewBag.Duration = ModelState["Duration"].Value.AttemptedValue;
            ViewBag.Intensity = ModelState["Intensity"].Value.AttemptedValue;
            ViewBag.Exclude = ModelState["Exclude"].Value.AttemptedValue;
            ViewBag.Notes = ModelState["Notes"].Value.AttemptedValue;
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