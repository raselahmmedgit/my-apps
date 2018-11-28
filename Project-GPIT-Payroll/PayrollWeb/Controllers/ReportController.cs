﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using com.gpit.DataContext;
using PayrollWeb.CustomSecurity;
using PayrollWeb.Utility;
using PayrollWeb.ViewModels;
using Microsoft.Reporting.WebForms;

namespace PayrollWeb.Controllers
{
    public class ReportController : Controller
    {
        private readonly payroll_systemContext dataContext;

        public ReportController(payroll_systemContext cont)
        {
            this.dataContext = cont;
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult PaySlip()
        {

            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();
            ViewBag.Allowance = new List<AllowanceDeduction>();
            ViewBag.Deduction = new List<AllowanceDeduction>();
            return View();
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult PaySlip(int? empid, FormCollection collection, string sButton, ReportPayslip rp)
        {

            bool errorFound = false;
            var res = new OperationResult();
            var payslipInfo = new ReportPayslip();
            

            try
            {
                if (sButton == "Search")
                {
                    if (empid == null && string.IsNullOrEmpty(collection["Emp_No"]))
                    {
                        errorFound = true;
                        ModelState.AddModelError("", "Please select an employee or put employee no.");
                    }
                    else
                    {
                        var Emp = new Employee();
                        if (empid != null)
                        {
                            var _empD = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == empid);
                            Emp = Mapper.Map<Employee>(_empD);
                        }
                        else
                        {
                            var _empD = dataContext.prl_employee.AsEnumerable().SingleOrDefault(x => x.emp_no == collection["Emp_No"]);
                            if (_empD == null)
                            {
                                errorFound = true;
                                ModelState.AddModelError("", "Threre is no information for the given employee no.");
                            }
                            else
                            {
                                Emp = Mapper.Map<Employee>(_empD);
                            }
                        }
                        var salaryPD = dataContext.prl_salary_process_detail.SingleOrDefault(x => x.emp_id == Emp.id && x.salary_month.Year == rp.Year && x.salary_month.Month == rp.Month);
                        if (salaryPD == null)
                        {
                            errorFound = true;
                            ModelState.AddModelError("", "Salary has not been processed for the given data.");
                        }

                        if (!errorFound)
                        {
                            var empD = Mapper.Map<EmployeeDetails>(Emp.prl_employee_details.OrderByDescending(x => x.id).First());
                            
                            var salaryProcess = dataContext.prl_salary_process_detail.SingleOrDefault(x => x.emp_id == Emp.id && x.salary_month.Year == rp.Year && x.salary_month.Month == rp.Month);

                            var allowances = dataContext.prl_salary_allowances.Where(x => x.salary_process_id == salaryProcess.salary_process_id
                                && x.emp_id==salaryProcess.emp_id && x.salary_month == salaryProcess.salary_month).Select(p => new AllowanceDeduction
                                {
                                    head = p.prl_allowance_name.allowance_name,
                                    value = p.amount
                                }).ToList();
                            //Allowance = allowances;
                            var deductions = dataContext.prl_salary_deductions.Where(x => x.salary_process_id == salaryProcess.salary_process_id
                                && x.emp_id == salaryProcess.emp_id && x.salary_month == salaryProcess.salary_month).Select(p => new AllowanceDeduction
                                {
                                    head = p.prl_deduction_name.deduction_name,
                                    value = p.amount
                                }).ToList();
                            //deduction = deductions;

                            payslipInfo.empName = Emp.name;
                            payslipInfo.empNo = Emp.emp_no;
                            if (Emp.bank_id == null)
                            {
                                payslipInfo.paymentMode = "Cash";
                            }
                            else
                            {
                                payslipInfo.paymentMode = "Bank Transfer";
                                payslipInfo.bank = Emp.prl_bank.bank_name;
                                payslipInfo.accNo = Emp.account_no;
                            }
                            payslipInfo.designation = empD.prl_designation.name;
                            payslipInfo.department = empD.prl_Department.name;
                            payslipInfo.grade = empD.prl_grade.grade;
                            payslipInfo.basicSalary = Convert.ToDecimal(salaryProcess.this_month_basic);
                            payslipInfo.totalEarnings = salaryProcess.total_allowance;
                            payslipInfo.totalDeduction = salaryProcess.total_deduction;
                            payslipInfo.netPay = salaryProcess.total_allowance - salaryProcess.total_deduction;
                            

                            /****************/
                            string reportType = "PDF";

                            LocalReport lr = new LocalReport();
                            string path = Path.Combine(Server.MapPath("~/Views/Report/RDLC"), "PaySlip.rdlc");
                            if (System.IO.File.Exists(path))
                            {
                                lr.ReportPath = path;
                            }
                            else
                            {
                                return View("Index");
                            }
                            
                            var reportData = new  ReportPayslip();
                            var empDlist = new List<ReportPayslip>();
                            reportData.eId = Emp.id;
                            reportData.empNo = Emp.emp_no;
                            reportData.empName = Emp.name;
                            reportData.designation = empD.prl_designation.name;
                            reportData.department = empD.prl_Department.name;
                            reportData.division = empD.prl_division.name;
                            reportData.grade = empD.prl_grade.grade;
                            reportData.processId = salaryPD.salary_process_id;
                            reportData.basicSalary = Convert.ToDecimal(salaryPD.this_month_basic);
                            reportData.totalEarnings = salaryPD.total_allowance + reportData.basicSalary;
                            reportData.totalDeduction = salaryPD.total_deduction;
                            reportData.netPay = salaryPD.net_pay;
                            reportData.monthYear = salaryPD.salary_month;

                            reportData.tax = salaryPD.total_monthly_tax;
                            if (reportData.tax > 0)
                            {
                                reportData.totalDeduction = Convert.ToDecimal(reportData.totalDeduction + reportData.tax);
                            }

                            if (salaryPD.pf_arrear > 0)
                            {
                                reportData.pf = salaryPD.pf_amount + salaryPD.pf_arrear;
                            }
                            else
                            {
                                reportData.pf = salaryPD.pf_amount;
                            }
                            if (reportData.pf > 0)
                            {
                                reportData.totalEarnings = Convert.ToDecimal(reportData.totalEarnings + reportData.pf);
                                reportData.totalDeduction = Convert.ToDecimal(reportData.totalDeduction + reportData.pf*2);
                            }

                            if (Emp.bank_id == null)
                            {
                                reportData.paymentMode = "Cash";
                                reportData.bank = "";
                                reportData.accNo = "";
                            }
                            else
                            {
                                reportData.paymentMode = "Bank Transfer";
                                reportData.bank = Emp.prl_bank.bank_name;
                                reportData.accNo = Emp.account_no;
                            }

                            //Bonus
                            string mnth = Convert.ToString(reportData.monthYear.Month);
                            string yr = Convert.ToString(reportData.monthYear.Year);
                            var bonus = (from bp in dataContext.prl_bonus_process
                                         join bn in dataContext.prl_bonus_name on bp.bonus_name_id equals bn.id
                                         join bpd in dataContext.prl_bonus_process_detail on bp.id equals bpd.bonus_process_id
                                         where bpd.emp_id == reportData.eId && bp.month == mnth && bp.year == yr && bp.is_pay_with_salary == "Y"
                                         select new AllowanceDeduction
                                         {
                                             value = bpd.amount
                                         }).ToList();
                            if (bonus.Count > 0)
                            {
                                decimal totlbonus = 0;
                                foreach (var item in bonus)
                                {
                                    totlbonus += Convert.ToDecimal(item.value);
                                }
                                reportData.totalEarnings = Convert.ToDecimal(reportData.totalEarnings + totlbonus);
                            }

                            empDlist.Add(reportData);
                                       
                            ReportDataSource rd = new ReportDataSource("DataSet1", empDlist);
                            lr.DataSources.Add(rd);
                            lr.SubreportProcessing += new SubreportProcessingEventHandler(lr_SubreportProcessing);
                            
                            string mimeType;
                            string encoding;
                            string fileNameExtension;

                            string deviceInfo =
                            "<DeviceInfo>" +
                            "<OutputFormat>PDF</OutputFormat>" +
                            "</DeviceInfo>";

                            Warning[] warnings;
                            string[] streams;
                            byte[] renderedBytes;

                            renderedBytes = lr.Render(
                                reportType,
                                deviceInfo,
                                out mimeType,
                                out encoding,
                                out fileNameExtension,
                                out streams,
                                out warnings);

                            return File(renderedBytes, mimeType);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();
            payslipInfo.Month = rp.Month;
            payslipInfo.Year = rp.Year;
            payslipInfo.MonthName = DateUtility.MonthName(rp.Month);

            return View(payslipInfo);
        }

        void lr_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            int eId = Convert.ToInt32(e.Parameters["eId"].Values[0]);
            int pId = Convert.ToInt32(e.Parameters["procesId"].Values[0]);
            
            string pth = e.ReportPath;

            if (pth == "PaySlipChildAllowance")
            {
                var allowances = dataContext.prl_salary_allowances.Where(x => x.salary_process_id == pId
                                && x.emp_id == eId).Select(p => new AllowanceDeduction
                                {
                                    head = p.prl_allowance_name.allowance_name,
                                    value = p.amount
                                }).ToList();

                AllowanceDeduction AD = new AllowanceDeduction();
                AD.head = "Basic Salary";
                AD.value = Convert.ToDecimal(e.Parameters["basicSlr"].Values[0]);
                allowances.Insert(0,AD);

                AllowanceDeduction AD2 = new AllowanceDeduction();
                decimal pf = Convert.ToDecimal(e.Parameters["pf"].Values[0]);
                if (pf > 0)
                {
                    AD2.head = "Provident Fund Company Contribution";
                    AD2.value = pf;
                    allowances.Insert(1, AD2);
                }

                DateTime dt = Convert.ToDateTime(e.Parameters["pDate"].Values[0]);
                string mnth = Convert.ToString(dt.Month);
                string yr = Convert.ToString(dt.Year);
                var bonus = (from bp in dataContext.prl_bonus_process
                             join bn in dataContext.prl_bonus_name on bp.bonus_name_id equals bn.id
                             join bpd in dataContext.prl_bonus_process_detail on bp.id equals bpd.bonus_process_id
                             where bpd.emp_id == eId && bp.month == mnth && bp.year == yr && bp.is_pay_with_salary == "Y"
                             select new AllowanceDeduction
                             {
                                 head = bn.name,
                                 value=bpd.amount
                             }).ToList();

                if (bonus.Count > 0)
                {
                    int i = 2;
                    foreach (var item in bonus)
                    {
                        AllowanceDeduction AD3 = new AllowanceDeduction();

                        AD3.head = item.head;
                        AD3.value = item.value;

                        allowances.Insert(i, AD3);
                    }
                }

                e.DataSources.Add(new ReportDataSource("DataSet1", allowances));
            }
            else
            {
                var deductions = dataContext.prl_salary_deductions.Where(x => x.salary_process_id == pId
                            && x.emp_id == eId).Select(p => new AllowanceDeduction
                            {
                                head = p.prl_deduction_name.deduction_name,
                                value = p.amount
                            }).ToList();
                decimal tax = Convert.ToDecimal(e.Parameters["tax"].Values[0]);
                if (tax > 0)
                {
                    AllowanceDeduction AD = new AllowanceDeduction();
                    AD.head = "Income Tax";
                    AD.value = tax;
                    deductions.Insert(0, AD);
                }

                decimal pf = Convert.ToDecimal(e.Parameters["pf"].Values[0]);
                if (pf > 0)
                {
                    AllowanceDeduction AD = new AllowanceDeduction();
                    AD.head = "Provident Fund (Company+ Own)";
                    AD.value = pf*2;
                    deductions.Insert(0, AD);
                }

                e.DataSources.Add(new ReportDataSource("DataSet1", deductions));
            }
            
        }


        [PayrollAuthorize]
        public ActionResult BankAdvice()
        {
            ReportBankAdvice ba = new ReportBankAdvice();

            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            var _bankInfo = dataContext.prl_bank.ToList();
            ViewBag.Banks = _bankInfo;
            return View(ba);
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
                    pageIndex = Convert.ToInt32(iDisplayStrart) > 0 ? Convert.ToInt32(iDisplayStrart) - 1 : Convert.ToInt32(iDisplayStrart);
                }
                int pageSize = 30;
                if (!string.IsNullOrWhiteSpace(collection.Get("iDisplayLength")))
                {
                    pageSize = Convert.ToInt32(collection.Get("iDisplayLength"));
                }
                         
                int bnkId = Convert.ToInt32(collection["bnkId"]);
                int mnth = Convert.ToInt32(collection["mnth"]);
                int yr = Convert.ToInt32(collection["yr"]);
                var EmpList = (from emp in dataContext.prl_employee
                               join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                               join bank in dataContext.prl_bank on emp.bank_id equals bank.id
                               where bank.id == bnkId && spd.salary_month.Month == mnth && spd.salary_month.Year == yr
                               select new
                               {
                                   emp.emp_no,
                                   emp.name,
                                   emp.phone,
                               }).ToList();


                int totalRecords = EmpList.Count();
                var employees = EmpList.Skip(pageIndex).Take(pageSize);

                var aaData = employees.Select(x => new string[] { x.emp_no, x.name, "department", x.phone });

                return Json(new { sEcho = collection.Get("sEcho"), aaData = aaData, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                var k = ex.Message;
                throw;
            }
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult BankAdvice(ReportBankAdvice ba, FormCollection collection)
        {

            var empNumber = new List<string>();
            var empIds = new List<int>();
            var EmpList = new List<ReportModel>();
            if (collection.Get("empGroup") == "all")
            {
                EmpList = (from emp in dataContext.prl_employee
                           join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                           where spd.salary_month.Year == ba.Year && spd.salary_month.Month == ba.Month
                           select new ReportModel
                           {
                               empNo = emp.emp_no,
                               name = emp.name,
                               accNo = emp.account_no,
                               netPay = spd.net_pay
                           }).ToList();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ba.SelectedEmployees))
                {
                    empNumber = ba.SelectedEmployees.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    empIds = dataContext.prl_employee.AsEnumerable().Where(x => empNumber.Contains(x.emp_no))
                            .Select(x => x.id).ToList();

                    EmpList = (from emp in dataContext.prl_employee
                               join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                               where empNumber.Contains(emp.emp_no) && spd.salary_month.Year == ba.Year && spd.salary_month.Month == ba.Month
                               select new ReportModel
                               {
                                   empNo = emp.emp_no,
                                   name = emp.name,
                                   accNo = emp.account_no,
                                   netPay = spd.net_pay
                               }).ToList();
                }
                else
                {
                    ModelState.AddModelError("","No employee selected.");
                }
            }

            if (EmpList.Count > 0)
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Views/Report/RDLC"), "BankAdvice.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Index");
                }

                ReportDataSource rd = new ReportDataSource("DataSet1", EmpList);
                lr.DataSources.Add(rd);
                lr.SetParameters(new ReportParameter("monthYr", DateTime.Today.ToString("MM,yyy")));
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                            "<DeviceInfo>" +

                "  <OutputFormat>reportType</OutputFormat>" +

                "  <PageWidth>8.5in</PageWidth>" +

                "  <PageHeight>11in</PageHeight>" +

                "  <MarginTop>0.5in</MarginTop>" +

                "  <MarginLeft>1in</MarginLeft>" +

                "  <MarginRight>1in</MarginRight>" +

                "  <MarginBottom>0.5in</MarginBottom>" +

                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);


                return File(renderedBytes, mimeType);
            }
            else
            {
                ModelState.AddModelError("","No information found");
            }

            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            var _bankInfo = dataContext.prl_bank.ToList();
            ViewBag.Banks = _bankInfo;
            return View();
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult SalarySheet()
        {

            Grade g = new Grade();
            g.id = 0;
            g.grade = "All";
            var Grd = (from gr in dataContext.prl_grade
                         select new Grade
                         {
                             id = gr.id,
                             grade = gr.grade
                         }).ToList();
            Grd.Insert(0,g);
            ViewBag.Grades = Grd;

            Division d = new Division();
            d.id = 0;
            d.name = "All";
            var dvsion = (from dv in dataContext.prl_division
                          select new Division
                           {
                               id = dv.id,
                               name = dv.name
                           }).ToList();
            dvsion.Insert(0, d);
            ViewBag.Divisions = dvsion;

            Department dpt = new Department();
            dpt.id = 0;
            dpt.name = "All";
            var departM = (from gr in dataContext.prl_department
                           select new Department
                       {
                           id = gr.id,
                           name = gr.name
                       }).ToList();
            departM.Insert(0, dpt);
            ViewBag.Departments = departM;

            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            return View(new ReportSalarySheet
            {
                //RType. = true
            });
        }

        [HttpPost]
        public ActionResult GetEmps(FormCollection collection)
        {
            try
            {
                int pageIndex = 0;
                var iDisplayStrart = collection.Get("iDisplayStart");
                if (!string.IsNullOrWhiteSpace(iDisplayStrart))
                {
                    pageIndex = Convert.ToInt32(iDisplayStrart) > 0 ? Convert.ToInt32(iDisplayStrart) - 1 : Convert.ToInt32(iDisplayStrart);
                }
                int pageSize = 30;
                if (!string.IsNullOrWhiteSpace(collection.Get("iDisplayLength")))
                {
                    pageSize = Convert.ToInt32(collection.Get("iDisplayLength"));
                }

                int gradeId = Convert.ToInt32(collection["grdId"]);
                int divisionId = Convert.ToInt32(collection["diviId"]);
                int departId = Convert.ToInt32(collection["dptId"]);
                int mnth = Convert.ToInt32(collection["mnth"]);
                int yr = Convert.ToInt32(collection["yr"]);

                var EmpList = (from emp in dataContext.prl_employee
                           join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                           where spd.salary_month.Month == mnth && spd.salary_month.Year == yr
                               select new ReportSalarySheet
                           {
                               empId = emp.id,
                               empNo = emp.emp_no,
                               empName = emp.name,
                               phone = emp.phone
                           }).ToList();

                var distEmp = new List<ReportSalarySheet>();
                int flag = 0; //All
                if (EmpList.Count > 0)
                {
                    if (gradeId == 0 && divisionId == 0 && departId == 0)
                    {}
                    else
                    {
                        flag = 1;
                        foreach (ReportSalarySheet emp in EmpList)
                        {
                            var _empD = dataContext.prl_employee_details.Where(x => x.emp_id == emp.empId).OrderByDescending(p => p.id).First();
                            if (_empD.grade_id == gradeId)
                            {
                                distEmp.Add(emp);
                            }
                            else if (_empD.division_id == divisionId)
                            {
                                distEmp.Add(emp);
                            }
                            else if (_empD.department_id == departId)
                            {
                                distEmp.Add(emp);
                            }
                        }
                        
                    }
                }

                if (flag==1)
                {
                    EmpList.Clear();
                    EmpList = distEmp;
                }

                int totalRecords = EmpList.Count();
                var employees = EmpList.Skip(pageIndex).Take(pageSize);

                var aaData = employees.Select(x => new string[] { x.empNo, x.empName, "department", x.phone });

                return Json(new { sEcho = collection.Get("sEcho"), aaData = aaData, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                var k = ex.Message;
                throw;
            }
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult SalarySheet(ReportSalarySheet reportSalarySheet, FormCollection collection, string sButton)
        {
            var empNumber = new List<string>();
            var empIds = new List<int>();
            var EmpList = new List<ReportSalarySheet>();
            if (collection.Get("empGroup") == "all")
            {
                EmpList = (from emp in dataContext.prl_employee
                           join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                           where spd.salary_month.Year == reportSalarySheet.Year && spd.salary_month.Month == reportSalarySheet.Month
                           select new ReportSalarySheet
                           {
                               empNo = emp.emp_no,
                               empName = emp.name,
                               totalA=spd.total_allowance,
                               totalD=spd.total_deduction,
                               netPay = spd.net_pay
                           }).ToList();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(reportSalarySheet.SelectedEmployees))
                {
                    empNumber = reportSalarySheet.SelectedEmployees.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    empIds = dataContext.prl_employee.AsEnumerable().Where(x => empNumber.Contains(x.emp_no))
                            .Select(x => x.id).ToList();

                    EmpList = (from emp in dataContext.prl_employee
                               join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                               where empNumber.Contains(emp.emp_no) && spd.salary_month.Year == reportSalarySheet.Year && spd.salary_month.Month == reportSalarySheet.Month
                               select new ReportSalarySheet
                               {
                                   empNo = emp.emp_no,
                                   empName = emp.name,
                                   netPay = spd.net_pay
                               }).ToList();
                }
                else
                {
                    ModelState.AddModelError("", "No employee selected.");
                }
            }

            if (EmpList.Count > 0)
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Views/Report/RDLC"), "BankAdvice.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Index");
                }

                DateTime dt = new DateTime(reportSalarySheet.Year,reportSalarySheet.Month,1);

                ReportDataSource rd = new ReportDataSource("DataSet1", EmpList);
                lr.DataSources.Add(rd);
                lr.SetParameters(new ReportParameter("monthYr", dt.ToString("MM,yyy")));
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                            "<DeviceInfo>" +

                "  <OutputFormat>reportType</OutputFormat>" +

                "  <PageWidth>8.5in</PageWidth>" +

                "  <PageHeight>11in</PageHeight>" +

                "  <MarginTop>0.5in</MarginTop>" +

                "  <MarginLeft>1in</MarginLeft>" +

                "  <MarginRight>1in</MarginRight>" +

                "  <MarginBottom>0.5in</MarginBottom>" +

                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);


                return File(renderedBytes, mimeType);
            }
            else
            {
                ModelState.AddModelError("", "No information found");
            }


            Grade g = new Grade();
            g.id = 0;
            g.grade = "All";
            var Grd = (from gr in dataContext.prl_grade
                       select new Grade
                       {
                           id = gr.id,
                           grade = gr.grade
                       }).ToList();
            Grd.Insert(0, g);
            ViewBag.Grades = Grd;

            Division d = new Division();
            d.id = 0;
            d.name = "All";
            var dvsion = (from dv in dataContext.prl_division
                          select new Division
                          {
                              id = dv.id,
                              name = dv.name
                          }).ToList();
            dvsion.Insert(0, d);
            ViewBag.Divisions = dvsion;

            Department dpt = new Department();
            dpt.id = 0;
            dpt.name = "All";
            var departM = (from gr in dataContext.prl_department
                           select new Department
                           {
                               id = gr.id,
                               name = gr.name
                           }).ToList();
            departM.Insert(0, dpt);
            ViewBag.Departments = departM;

            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            return View();
        }

        [PayrollAuthorize]
        public ActionResult SalaryChange()
        {
            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SalaryChange(ReportModel reportModel, string sButton)
        {
            var slrChange = (from slr in dataContext.prl_salary_review
                             join emp in dataContext.prl_employee on slr.emp_id equals emp.id
                             where slr.created_date.Value.Month == reportModel.Month && slr.created_date.Value.Year == reportModel.Year
                             select new ReportSalaryChange
                             {
                                 eId= emp.id,
                                 empId=emp.emp_no,
                                 empName=emp.name,
                                 grade="",
                                 entrydate=slr.created_date.Value,
                                 effectiveFrom=slr.effective_from.Value,
                                 oldbasic=slr.current_basic,
                                 newBasic=slr.new_basic,
                                 reason=slr.increment_reason
                             }).ToList();

            if (slrChange.Count > 0)
            {
                var data = new List<ReportSalaryChange>();
                foreach (var item in slrChange)
                {
                    item.grade = dataContext.prl_employee_details.Where(p => p.emp_id == item.eId).OrderByDescending(x => x.id).First().prl_grade.grade;
                    data.Add(item);
                }

                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Views/Report/RDLC"), "SalaryChange.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Index");
                }

                DateTime dt = new DateTime(reportModel.Year, reportModel.Month, 1);

                ReportDataSource rd = new ReportDataSource("DataSet1", slrChange);
                lr.DataSources.Add(rd);
                lr.SetParameters(new ReportParameter("monthYr", dt.ToString("MM,yyy")));

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                if (sButton == "To Excel")
                {
                    reportType = "Excel";
                    //fileNameExtension = string.Format("{0}.{1}", "ExportToExcel", "xlsx");
                }


                string deviceInfo =

                            "<DeviceInfo>" +

                "  <OutputFormat>reportType</OutputFormat>" +

                "  <PageWidth>11in</PageWidth>" +

                "  <PageHeight>8.5in</PageHeight>" +

                "  <MarginTop>0.5in</MarginTop>" +

                "  <MarginLeft>1in</MarginLeft>" +

                "  <MarginRight>1in</MarginRight>" +

                "  <MarginBottom>0.5in</MarginBottom>" +

                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            else
            {
                ModelState.AddModelError("", "No information found");
            }

            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();

            return View(reportModel);
        }

        /*
        [PayrollAuthorize]
        public ActionResult Bonus(string MnthAndYr)
        {
            var Rb = new ReportBonus();
            
            var years = DateUtility.GetYears();
            string yrs = Convert.ToString(years[0]);
            ViewBag.Years = years;
            ViewBag.Months = DateUtility.GetMonths();

            var bonus = new List<ReportBonus>() ;
            if (MnthAndYr != null)
            {
                string[] MnYr = MnthAndYr.Split(',');

                string yr = MnYr[0];
                string mnth = MnYr[1];

                bonus = (from bp in dataContext.prl_bonus_process
                         join bn in dataContext.prl_bonus_name on bp.bonus_name_id equals bn.id
                         where bp.month == mnth && bp.year == yr
                         select new ReportBonus
                         {
                             BonusId = bp.bonus_name_id,
                             BonusName = bn.name
                         }).Distinct().ToList();
                Rb.Month = mnth;
                Rb.Year = yr;
            }
            else
            {
                bonus = (from bp in dataContext.prl_bonus_process
                         join bn in dataContext.prl_bonus_name on bp.bonus_name_id equals bn.id
                         where bp.month == "1" && bp.year == yrs
                         select new ReportBonus
                         {
                             BonusId = bp.bonus_name_id,
                             BonusName = bn.name
                         }).Distinct().ToList();
            }
            if (bonus.Count == 0)
            {
                ReportBonus rb = new ReportBonus();
                rb.BonusId = -1;
                rb.BonusName = "--- No Bonus ---";

                bonus.Add(rb);
            }
            else
            {
                ReportBonus rb = new ReportBonus();
                rb.BonusId = 0;
                rb.BonusName = "All";

                bonus.Add(rb);
            }
            ViewBag.BonusName = bonus;

            return View(Rb);
        }*/
        
        /*
        [PayrollAuthorize]
        [HttpPost]
        public ActionResult Bonus(ReportBonus RB)
        {
            var bonus = new List<ReportBonus>();

            if (RB.BonusId == -1)
            {
                ModelState.AddModelError("", "No information found");
            }
            else if (RB.BonusId == 0) // All
            {
                bonus = (from bp in dataContext.prl_bonus_process
                         join bn in dataContext.prl_bonus_name on bp.bonus_name_id equals bn.id
                         join bpd in dataContext.prl_bonus_process_detail on bp.id equals bpd.bonus_process_id
                         join emp in dataContext.prl_employee on bpd.emp_id equals emp.id
                         where bp.month == RB.Month && bp.year == RB.Year
                         select new ReportBonus
                         {
                             eId = emp.id,
                             empId = emp.emp_no,
                             empName = emp.name,
                             grade = "",
                             newBasic = 0,
                             BonusName = bn.name,
                             bonus = bpd.amount
                         }).ToList();
            }
            else
            {
                bonus = (from bp in dataContext.prl_bonus_process
                         join bn in dataContext.prl_bonus_name on bp.bonus_name_id equals bn.id
                         join bpd in dataContext.prl_bonus_process_detail on bp.id equals bpd.bonus_process_id
                         join emp in dataContext.prl_employee on bpd.emp_id equals emp.id
                         where bp.month == RB.Month && bp.year == RB.Year && bn.id == RB.BonusId
                         select new ReportBonus
                         {
                             eId=emp.id,
                             empId = emp.emp_no,
                             empName = emp.name,
                             grade = "",
                             newBasic = 0,
                             BonusName = bn.name,
                             bonus = bpd.amount
                         }).ToList();
            }

            if (bonus.Count > 0)
            {
                var data = new List<ReportBonus>();
                foreach (var item in bonus)
                {
                    item.grade = dataContext.prl_employee_details.Where(p => p.emp_id == item.eId).OrderByDescending(x => x.id).First().prl_grade.grade;
                    item.bonus = dataContext.prl_employee_details.Where(p => p.emp_id == item.eId).OrderByDescending(x => x.id).First().basic_salary;
                    data.Add(item);
                }

                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Views/Report/RDLC"), "Bonus.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Index");
                }
                ReportDataSource rd = new ReportDataSource("DataSet1", data);
                lr.DataSources.Add(rd);

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                //if (sButton == "To Excel")
                //{
                //    reportType = "Excel";
                //    //fileNameExtension = string.Format("{0}.{1}", "ExportToExcel", "xlsx");
                //}

                string deviceInfo =

                            "<DeviceInfo>" +

                "  <OutputFormat>reportType</OutputFormat>" +

                "  <PageWidth>11in</PageWidth>" +

                "  <PageHeight>8.5in</PageHeight>" +

                "  <MarginTop>0.5in</MarginTop>" +

                "  <MarginLeft>1in</MarginLeft>" +

                "  <MarginRight>1in</MarginRight>" +

                "  <MarginBottom>0.5in</MarginBottom>" +

                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            else
            {
                ModelState.AddModelError("", "No information found");
            }


            ViewBag.Years = DateUtility.GetYears();
            ViewBag.Months = DateUtility.GetMonths();
            if (bonus.Count == 0)
            {
                ReportBonus rb = new ReportBonus();
                rb.BonusId = -1;
                rb.BonusName = "--- No Bonus ---";

                bonus.Add(rb);
            }
            else
            {
                ReportBonus rb = new ReportBonus();
                rb.BonusId = -1;
                rb.BonusName = "All";

                bonus.Add(rb);
            }
            ViewBag.BonusName = bonus;
            return View(RB);
        }*/
    }
}
