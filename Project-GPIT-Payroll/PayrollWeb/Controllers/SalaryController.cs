﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using com.gpit.DataContext;
using PayrollWeb.CustomSecurity;
using PayrollWeb.Service;
using PayrollWeb.Utility;
using PayrollWeb.ViewModels;


namespace PayrollWeb.Controllers
{
    public class SalaryController : Controller
    {
        private payroll_systemContext dataContext;
        
        public SalaryController(payroll_systemContext context)
        {
            this.dataContext = context;
        }

        [PayrollAuthorize]
        public ActionResult Index()
        {

            SalaryProcessModel spm=new SalaryProcessModel();
            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();
            ViewBag.Grades = Mapper.Map<List<Grade>>(dataContext.prl_grade.ToList());
            ViewBag.Divisions = Mapper.Map<List<Division>>(dataContext.prl_division.ToList());
            ViewBag.Departments = Mapper.Map<List<Department>>(dataContext.prl_department.ToList());
            return View(spm);
        }
        [HttpPost]
        public ActionResult GetEmployees(FormCollection collection)
        {
            try
            {
                int pageIndex = 0;
                var iDisplayStrart = collection.Get("iDisplayStart");
                if (!string.IsNullOrWhiteSpace(iDisplayStrart))
                {
                    pageIndex = Convert.ToInt32(iDisplayStrart) > 0 ? Convert.ToInt32(iDisplayStrart)-1 : Convert.ToInt32(iDisplayStrart);
                }
                int pageSize = 30;
                if (!string.IsNullOrWhiteSpace(collection.Get("iDisplayLength")))
                {
                    pageSize = Convert.ToInt32(collection.Get("iDisplayLength"));
                }

                int totalRecords = dataContext.prl_employee.AsEnumerable().Count(x => x.is_active == Convert.ToSByte(true));
                var employees = dataContext.prl_employee.AsEnumerable().Where(x => x.is_active == Convert.ToSByte(true)).Skip(pageIndex).Take(pageSize);
                
                //int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
                //var returnEmployees = employees.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                // this is test

                var aaData = employees.Select(x => new string[] {  x.emp_no, x.name, "department", x.phone});
                
                return Json(new { sEcho = collection.Get("sEcho"),aaData = aaData, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                var k = ex.Message;
                throw;
            }
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult SProcess(SalaryProcessModel sal)
        {

            var salService = new SalaryService(dataContext);
            var processAllEmployee = false;
            var empNumber = new List<string>();
            var empIds = new List<int>();
            if (string.IsNullOrWhiteSpace(sal.SelectedEmployeesOnly))
            {
                processAllEmployee = true;
            }
            if (!string.IsNullOrWhiteSpace(sal.SelectedEmployeesOnly))
            {
                empNumber = sal.SelectedEmployeesOnly.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                empIds =dataContext.prl_employee.AsEnumerable().Where(x => empNumber.Contains(x.emp_no))
                        .Select(x => x.id).ToList();
            }
            //var res = salService.ProcessSalary(processAllEmployee, empIds, sal.Grade, sal.Division, sal.Department, sal.SalaryProcessDate, sal.SalaryPaymentDate);
            var res = salService.ProcessSalary(processAllEmployee, empIds, sal.Grade, sal.Division, sal.Department, DateTime.Parse("25/10/2018"), DateTime.Parse("25/10/2018"));
            return Json(new { success = !res.ErrorOccured, errList = res.GetErrors, msg = "Salary could not be processed for some employees." });
        }

        [HttpPost]
        public ActionResult GetDateByMonthYear(int? year, int? month)
        {
            string nd = DateTime.Now.ToString("MM/dd/yyyy");
            if(year != null && month !=null)
            if (year > 0 && month > 0)
            {
                var dt = new DateTime((int)year, (int)month, 25);
                nd = dt.ToString("MM/dd/yyyy");
            }

            return Json(new {nd = nd}, JsonRequestBehavior.DenyGet);
        }

        [PayrollAuthorize]
        public ActionResult UndoSalaryProcess()
        {
            SalaryProcessModel spm = new SalaryProcessModel();
            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();
            ViewBag.Grades = Mapper.Map<List<Grade>>(dataContext.prl_grade.ToList());
            ViewBag.Divisions = Mapper.Map<List<Division>>(dataContext.prl_division.ToList());
            ViewBag.Departments = Mapper.Map<List<Department>>(dataContext.prl_department.ToList());

            return View(spm);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult UndoSalaryProcess(SalaryProcessModel salProcess)
        {
            var res = new OperationResult();
            int _result = 0;
            try
            {
                SalaryService _service = new SalaryService(dataContext);
                _result = _service.salaryRollbacked(salProcess, "", 0, salProcess.Month, salProcess.Year);
                if (_result > 0)
                {
                    res.IsSuccessful = true;
                    res.Message = "Salary has been rollbacked.";
                    TempData.Add("msg", res);
                    return RedirectToAction("UndoSalaryProcess");
                }
                else if (_result == -909)
                {
                    res.IsSuccessful = false;
                    res.Message = "Salary already rollbacked.";
                    TempData.Add("msg", res);
                }
                else
                {
                    res.IsSuccessful = false;
                    res.Message = "Salary has not been rollbacked.";
                    TempData.Add("msg", res);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            SalaryProcessModel spm = new SalaryProcessModel();
            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();
            ViewBag.Grades = Mapper.Map<List<Grade>>(dataContext.prl_grade.ToList());
            ViewBag.Divisions = Mapper.Map<List<Division>>(dataContext.prl_division.ToList());
            ViewBag.Departments = Mapper.Map<List<Department>>(dataContext.prl_department.ToList());

            return View();
        }
        [PayrollAuthorize]
        public ActionResult DisburseSalary()
        {
            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            return View();
        }

        public ActionResult GetUndisbursedBatch(int y, int m)
        {
            if (y == 0 || m == 0)
            {
                return   Json(new { isError= true, msg="You must select a valid year and month." },JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dt = new DateTime(y, m, 1);
                var procList = dataContext.prl_salary_process.AsEnumerable().
                    Where(x =>x.payment_date.Value.ToString("yyyy-MM") == dt.ToString("yyyy-MM") && x.is_disbursed.ToLower() == "n").
                    Select(x =>
                            new
                            {
                                id = x.id,
                                is_disbursed = x.is_disbursed,
                                batch_no = x.batch_no,
                                payment_date = x.payment_date,
                                process_date = x.process_date
                            }).ToList();
                return Json(new {isError = false, msg = "Data found",procList=procList},JsonRequestBehavior.AllowGet);
            }
            
        }
        [PayrollAuthorize]
        public ActionResult Disburse(int d)
        {
            var res = new OperationResult();
            try
            {
                var updateCmd = @"update prl_salary_process set is_disbursed='Y' where id="+d +";";
                int r = dataContext.Database.ExecuteSqlCommand(updateCmd);
                res.IsSuccessful = true;
                res.Message = "Successfully disbursed";
            }
            catch (Exception)
            {
                res.IsSuccessful = false;
                res.Message = "Could not disburse salary";
            }
            TempData.Add("msg", res);
           return RedirectToAction("DisburseSalary");
        }
    }
}
