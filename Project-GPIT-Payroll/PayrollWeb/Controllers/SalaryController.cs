using System;
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
using Microsoft.Reporting.WebForms;
using System.IO;


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

            DateTime nowDateTime = DateTime.Now;
            DateTime salaryProcessDate = new DateTime(nowDateTime.Year, nowDateTime.Month, 1);
            DateTime salaryPaymentDate = salaryProcessDate.AddMonths(1).AddDays(-1);
            spm.SalaryProcessDate = salaryProcessDate;
            spm.SalaryPaymentDate = salaryPaymentDate;

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
            var res = salService.ProcessSalary(processAllEmployee, empIds, sal.Grade, sal.Division, sal.Department, sal.SalaryProcessDate, sal.SalaryPaymentDate);
            //var res = salService.ProcessSalary(processAllEmployee, empIds, sal.Grade, sal.Division, sal.Department, DateTime.Parse("25/10/2018"), DateTime.Parse("25/10/2018"));
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

            DateTime nowDateTime = DateTime.Now;
            DateTime salaryProcessDate = new DateTime(nowDateTime.Year, nowDateTime.Month, 1);
            DateTime salaryPaymentDate = salaryProcessDate.AddMonths(1).AddDays(-1);
            spm.SalaryProcessDate = salaryProcessDate;
            spm.SalaryPaymentDate = salaryPaymentDate;

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

            DateTime nowDateTime = DateTime.Now;
            DateTime salaryProcessDate = new DateTime(nowDateTime.Year, nowDateTime.Month, 1);
            DateTime salaryPaymentDate = salaryProcessDate.AddMonths(1).AddDays(-1);
            spm.SalaryProcessDate = salaryProcessDate;
            spm.SalaryPaymentDate = salaryPaymentDate;

            return View(spm);
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
                                && x.emp_id == salaryProcess.emp_id && x.salary_month == salaryProcess.salary_month).Select(p => new AllowanceDeduction
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

                            var reportData = new ReportPayslip();
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
                                reportData.totalDeduction = Convert.ToDecimal(reportData.totalDeduction + reportData.pf * 2);
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
            ViewBag.Allowance = new List<AllowanceDeduction>();
            ViewBag.Deduction = new List<AllowanceDeduction>();

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
                allowances.Insert(0, AD);

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
                                 value = bpd.amount
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
                    AD.value = pf * 2;
                    deductions.Insert(0, AD);
                }

                e.DataSources.Add(new ReportDataSource("DataSet1", deductions));
            }

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
                    { }
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

                if (flag == 1)
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
        public ActionResult SalarySheet(ReportSalarySheet SS, FormCollection collection, string sButton)
        {
            var empNumber = new List<string>();
            var empIds = new List<int>();
            var EmpList = new List<ReportSalarySheet>();
            if (collection.Get("empGroup") == "all")
            {
                EmpList = (from emp in dataContext.prl_employee
                           join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                           where spd.salary_month.Year == SS.Year && spd.salary_month.Month == SS.Month
                           select new ReportSalarySheet
                           {
                               empNo = emp.emp_no,
                               empName = emp.name,
                               totalA = spd.total_allowance,
                               totalD = spd.total_deduction,
                               netPay = spd.net_pay
                           }).ToList();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SS.SelectedEmployees))
                {
                    empNumber = SS.SelectedEmployees.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    empIds = dataContext.prl_employee.AsEnumerable().Where(x => empNumber.Contains(x.emp_no))
                            .Select(x => x.id).ToList();

                    EmpList = (from emp in dataContext.prl_employee
                               join spd in dataContext.prl_salary_process_detail on emp.id equals spd.emp_id
                               where empNumber.Contains(emp.emp_no) && spd.salary_month.Year == SS.Year && spd.salary_month.Month == SS.Month
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

                DateTime dt = new DateTime(SS.Year, SS.Month, 1);

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
    }
}
