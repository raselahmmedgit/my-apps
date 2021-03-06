﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using PayrollWeb.Models;

namespace PayrollWeb.Controllers
{
    public class DataSyncController : Controller
    {
        //
        // GET: /DataSync/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DataSync/Details/5

        public ActionResult SyncData()
        {
            //var dataImport= new OracleDataImportManger();
            //var importedData = dataImport.GetEmployees();
            //var result = dataImport.SaveData(importedData);
            return Json(new { ProcessResult = 1}, JsonRequestBehavior.AllowGet);
        }

        // GET: /DataSync/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DataSync/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DataSync/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DataSync/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DataSync/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DataSync/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
