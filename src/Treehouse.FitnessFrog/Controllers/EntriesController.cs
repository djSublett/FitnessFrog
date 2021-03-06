﻿using System;
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
            var entry = new Entry()
            {
                Date = DateTime.Today,
                
            };
            ViewBag.ActivitiesSelectListItems = new SelectList(
                Data.Data.Activities, "Id", "Name");

            return View(entry);
        }


        [HttpPost]      //used an attribute named "ActionName" within brackets. then set the parameter as "Add"....also used the attribute of "HttpPost" 
        public ActionResult Add(Entry entry) //Model Binder will recognize this parameter is an instance of a class or reference type
                                                //and then bind the incoming form field values to its properties
        {
            ModelState.AddModelError("", "This is a global message.");

            //if there arent any "duration" field validation errors
            //then make sure the duration entered is greater than 0
            if(ModelState.IsValidField("Duration") && entry.Duration <= 0)
            {
                ModelState.AddModelError("Duration",
                    "The Duration field value must be greater than '0'.");
            }
            if (ModelState.IsValid)
            {
                _entriesRepository.AddEntry(entry);

                //redirected to the Add Entry list page
                return RedirectToAction("Index");

            }

            ViewBag.ActivitiesSelectListItems = new SelectList(
                Data.Data.Activities, "Id", "Name");

            return View(entry);
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