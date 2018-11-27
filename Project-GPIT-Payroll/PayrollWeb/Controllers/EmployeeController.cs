using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using com.gpit.DataContext;
using com.gpit.Model;
using PagedList;
using PayrollWeb.Utility;
using PayrollWeb.ViewModels;
using PayrollWeb.CustomSecurity;
using PayrollWeb.ViewModels.Utility;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;

namespace PayrollWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly payroll_systemContext dataContext;

        public EmployeeController(payroll_systemContext cont)
        {
            this.dataContext = cont;
        }

        [PayrollAuthorize]
        public ActionResult Index(int? empid, FormCollection collection, string sButton)
        {

            if (Session["_EmpD"] != null)
                Session["_EmpD"] = null;
            if (Session["NewEmp"] != null)
                Session["NewEmp"] = null;
            if (Session["NewEmpForEdit"] != null)
                Session["NewEmpForEdit"] = null;
            if (Session["NewEmpDetailForEdit"] != null)
                Session["NewEmpDetailForEdit"] = null;

            var lists = new List<Employee>().ToPagedList(1, 1);

            if (sButton == null)
            {
                var lstEmp = dataContext.prl_employee.Include("prl_employee_details").OrderBy(x => x.emp_no);
                lists = Mapper.Map<List<Employee>>(lstEmp).ToPagedList(1, 25);
            }
            else
            {

                if (empid == null && string.IsNullOrEmpty(collection["Emp_No"]))
                {
                    //errorFound = true;
                    ModelState.AddModelError("", "Please select an employee or put employee ID");
                }
                else
                {
                    if (empid != null)
                    {
                        var _emp = dataContext.prl_employee.Include("prl_employee_details").Where(x => x.id == empid);
                        lists = Mapper.Map<List<Employee>>(_emp).ToPagedList(1, 1);
                    }
                    else
                    {
                        var _emp = dataContext.prl_employee.Include("prl_employee_details").AsEnumerable().Where(x => x.emp_no == collection["Emp_No"]);
                        if (_emp.Count() > 0)
                        {
                            lists = Mapper.Map<List<Employee>>(_emp).ToPagedList(1, 1);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Threre is no information for the given employee ID");
                        }
                    }
                }
            }
            return View(lists);
        }

        public ActionResult Paging(int? page)
        {
            int pageSize = 25;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            var _employees = dataContext.prl_employee.Include("prl_employee_details").OrderBy(x => x.emp_no);
            var empList = Mapper.Map<List<Employee>>(_employees);

            var pglst = empList.ToPagedList(pageIndex, pageSize);

            return View("Index", pglst);
        }

        [PayrollAuthorize]
        public ActionResult Details(int id)
        {
            var lstEmpD = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == id);
            return View(Mapper.Map<Employee>(lstEmpD));
        }

        [PayrollAuthorize]
        public ActionResult Create()
        {

            var _Emp = new Employee();
            if (Session["NewEmp"] != null)
            {
                _Emp = (Employee)Session["NewEmp"];
            }

            var lstReligion = dataContext.prl_religion.ToList();
            ViewBag.Religions = lstReligion;

            var lstCompany = dataContext.prl_company.ToList();
            ViewBag.Companies = lstCompany;

            _Emp.joining_date = DateTime.Now;
            return View(_Emp);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            
            if (ModelState.IsValid)
            {
                Session["NewEmp"] = emp;
                return RedirectToAction("CreateEmpDetails");
            }


            var lstReligion = dataContext.prl_religion.ToList();
            ViewBag.Religions = lstReligion;

            var lstCompany = dataContext.prl_company.ToList();
            ViewBag.Companies = lstCompany;

            return View();
        }

        [PayrollAuthorize]
        public ActionResult CreateEmpDetails()
        {

            var _Emp = (Employee)Session["NewEmp"];
            var EmpD = new EmployeeDetails();
            EmpD.name = _Emp.name;
            if (Session["_EmpD"] == null)
            {
                EmpD.contract_start_date = DateTime.Today;
                EmpD.contract_end_date = DateTime.Today.AddMonths(6);
            }
            else
            {
                var _empD = (EmployeeDetails)Session["_EmpD"];
                EmpD.designation_id = _empD.designation_id;
                EmpD.department_id = _empD.department_id;
                EmpD.division_id = _empD.division_id;
                EmpD.emp_status = _empD.emp_status;
                EmpD.contract_start_date = _empD.contract_start_date;
                EmpD.contract_end_date = _empD.contract_end_date;
                EmpD.grade_id = _empD.grade_id;
                EmpD.basic_salary = _empD.basic_salary;
                EmpD.posting_location_id = _empD.posting_location_id;
                EmpD.posting_date = _empD.posting_date;
            }

            var lstGrades = dataContext.prl_grade.ToList();
            ViewBag.Grades = lstGrades;

            var lstDesig = dataContext.prl_designation.ToList();
            ViewBag.Designations = lstDesig;

            var lstDept = dataContext.prl_department.ToList();
            ViewBag.Departments = lstDept;

            var lstDiv = dataContext.prl_division.ToList();
            ViewBag.Divitions = lstDiv;

            var lstPostings = dataContext.prl_location.ToList();
            ViewBag.PostingLocations = lstPostings;

            return View(EmpD);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult CreateEmpDetails(EmployeeDetails empD, string submitButton)
        {

            var res = new OperationResult();
            var _InsertingEmp = (Employee)Session["NewEmp"];
            if (submitButton == "Previous")
            {
                Session["_EmpD"] = empD;
                return RedirectToAction("Create");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (empD.emp_status == "Regular")
                        {
                            empD.contract_start_date = null;
                            empD.contract_end_date = null;
                        }
                        empD.created_by = User.Identity.Name;
                        empD.created_date = DateTime.Now;
                        _InsertingEmp.prl_employee_details.Add(empD);

                        var nwEmp = Mapper.Map<prl_employee>(_InsertingEmp);
                        nwEmp.created_by = User.Identity.Name;
                        nwEmp.created_date = DateTime.Now;
                        dataContext.prl_employee.Add(nwEmp);
                        dataContext.SaveChanges();

                        var _salaryReview = new SalaryReview();
                        _salaryReview.emp_id = nwEmp.id;
                        _salaryReview.current_basic = empD.basic_salary;
                        _salaryReview.new_basic = empD.basic_salary;
                        _salaryReview.effective_from = _InsertingEmp.joining_date;
                        _salaryReview.increment_reason = "Joining";

                        _salaryReview.description = "NA";
                        _salaryReview.created_by = User.Identity.Name;
                        _salaryReview.created_date = DateTime.Now;

                        var _review = Mapper.Map<prl_salary_review>(_salaryReview);
                        dataContext.prl_salary_review.Add(_review);
                        dataContext.SaveChanges();

                        res.IsSuccessful = true;
                        res.Message = "Employee information of " + nwEmp.name + " saved successfully.";
                        TempData.Add("msg", res);


                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                    }
                }


                var lstGrades = dataContext.prl_grade.ToList();
                ViewBag.Grades = lstGrades;

                var lstDesig = dataContext.prl_designation.ToList();
                ViewBag.Designations = lstDesig;

                var lstDept = dataContext.prl_department.ToList();
                ViewBag.Departments = lstDept;

                var lstDiv = dataContext.prl_division.ToList();
                ViewBag.Divitions = lstDiv;

                var lstPostings = dataContext.prl_location.ToList();
                ViewBag.PostingLocations = lstPostings;

                return View();
            }
        }

        [PayrollAuthorize]
        public ActionResult Edit(int? id)
        {

            var _empInfoById = new Employee();
            if (Session["NewEmpForEdit"] != null)
            {
                _empInfoById = (Employee)Session["NewEmpForEdit"];
            }
            else
            {
                var empD = dataContext.prl_employee.SingleOrDefault(x => x.id == id).prl_employee_details.OrderByDescending(x => x.id).First();
                var _empDetailsfoForEdit = Mapper.Map<EmployeeDetails>(empD);

                _empInfoById = dataContext.prl_employee.Where(x => x.id == id).Select(x => new Employee
                {
                    emp_no = x.emp_no,
                    name = x.name,
                    joining_date = x.joining_date,
                    company_id = x.company_id,
                    father_name = x.father_name,
                    mother_name = x.mother_name,
                    present_address = x.present_address,
                    permanent_address = x.permanent_address,
                    phone = x.phone,
                    email = x.email,
                    religion_id = x.religion_id,
                    gender = x.gender,
                    marital_status = x.marital_status,
                    dob = x.dob,
                    tin = x.tin,
                }).SingleOrDefault();

                _empInfoById.prl_employee_details.Add(_empDetailsfoForEdit);
            }

            var lstReligion = dataContext.prl_religion.ToList();
            ViewBag.Religions = lstReligion;

            var lstCompany = dataContext.prl_company.ToList();
            ViewBag.Companies = lstCompany;

            return View(_empInfoById);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult Edit(Employee UpdatingEmp)
        {
            var res = new OperationResult();
            if (ModelState.IsValid)
            {
                try
                {
                    Session["NewEmpForEdit"] = UpdatingEmp;
                    return RedirectToAction("EditEmpDetails");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }
            }


            var lstReligion = dataContext.prl_religion.ToList();
            ViewBag.Religions = lstReligion;

            var lstCompany = dataContext.prl_company.ToList();
            ViewBag.Companies = lstCompany;

            return View();
        }

        [PayrollAuthorize]
        public ActionResult EditEmpDetails()
        {

            var _empDetailsForEdit = new EmployeeDetails();
            var _empInfoForEdit = (Employee)Session["NewEmpForEdit"];

            if (Session["NewEmpDetailForEdit"] != null)
            {
                _empDetailsForEdit = (EmployeeDetails)Session["NewEmpDetailForEdit"];
            }
            else
            {
                _empDetailsForEdit = Mapper.Map<EmployeeDetails>(_empInfoForEdit.prl_employee_details[0]);
            }
            ViewData["EmpNum"] = _empInfoForEdit.emp_no;

            var lstGrades = dataContext.prl_grade.ToList();
            ViewBag.Grades = lstGrades;

            var lstDesig = dataContext.prl_designation.ToList();
            ViewBag.Designations = lstDesig;

            var lstDept = dataContext.prl_department.ToList();
            ViewBag.Departments = lstDept;

            var lstDiv = dataContext.prl_division.ToList();
            ViewBag.Divitions = lstDiv;

            var lstPostings = dataContext.prl_location.ToList();
            ViewBag.PostingLocations = lstPostings;

            return View(_empDetailsForEdit);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult EditEmpDetails(int id, EmployeeDetails empD, string submitButton)
        {
            var res = new OperationResult();
            if (submitButton == "Previous")
            {
                Session["NewEmpDetailForEdit"] = empD;
                return RedirectToAction("Edit");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var _empD = dataContext.prl_employee_details.SingleOrDefault(x => x.id == id);
                        _empD.designation_id = empD.designation_id;
                        _empD.department_id = empD.department_id;
                        _empD.division_id = empD.division_id;
                        _empD.emp_status = empD.emp_status;
                        if (empD.emp_status == "Regular")
                        {
                            _empD.contract_start_date = null;
                            _empD.contract_end_date = null;
                        }
                        else
                        {
                            _empD.contract_start_date = empD.contract_start_date;
                            _empD.contract_end_date = empD.contract_end_date;
                        }
                        _empD.grade_id = empD.grade_id;
                        _empD.basic_salary = empD.basic_salary;
                        _empD.posting_location_id = empD.posting_location_id;
                        _empD.posting_date = empD.posting_date;
                        _empD.updated_by = User.Identity.Name;
                        _empD.updated_date = DateTime.Now;

                        var emp = (Employee)Session["NewEmpForEdit"];
                        var _emp = dataContext.prl_employee.SingleOrDefault(x => x.id == emp.id);
                        _emp.name = emp.name;
                        _emp.joining_date = emp.joining_date;
                        _emp.company_id = emp.company_id;
                        _emp.father_name = emp.father_name;
                        _emp.mother_name = emp.mother_name;
                        _emp.present_address = emp.present_address;
                        _emp.permanent_address = emp.permanent_address;
                        _emp.phone = emp.phone;
                        _emp.email = emp.email;
                        _emp.religion_id = emp.religion_id;
                        _emp.gender = emp.gender;
                        _emp.marital_status = emp.marital_status;
                        _emp.dob = emp.dob;
                        _emp.tin = emp.tin;
                        _emp.prl_employee_details.Add(_empD);
                        _emp.updated_by = User.Identity.Name;
                        _emp.updated_date = DateTime.Now;

                        dataContext.SaveChanges();

                        res.IsSuccessful = true;
                        res.Message = "Information of " + emp.name + " updated successfully.";
                        TempData.Add("msg", res);

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                    }
                }


                var lstGrades = dataContext.prl_grade.ToList();
                ViewBag.Grades = lstGrades;

                var lstDesig = dataContext.prl_designation.ToList();
                ViewBag.Designations = lstDesig;

                var lstDept = dataContext.prl_department.ToList();
                ViewBag.Departments = lstDept;

                var lstDiv = dataContext.prl_division.ToList();
                ViewBag.Divitions = lstDiv;

                var lstPostings = dataContext.prl_location.ToList();
                ViewBag.PostingLocations = lstPostings;

                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5

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

        [PayrollAuthorize]
        public ActionResult SearchEmployee(string SearchFor)
        {

            var empS = new EmployeeSearch();
            empS.SearchFor = SearchFor;
            return View(empS);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult SearchEmployee(int? empid, FormCollection collection)
        {
            bool errorFound = false;
            var res = new OperationResult();

            int _empId = 0;

            if (empid == null && string.IsNullOrEmpty(collection["Emp_No"]))
            {
                errorFound = true;
                ModelState.AddModelError("", "Please select an employee or put employee no.");
            }
            else
            {
                if (empid != null)
                {
                    _empId = Convert.ToInt32(empid);
                }
                else
                {
                    string empNo = collection["Emp_No"];
                    var _emp = dataContext.prl_employee.SingleOrDefault(x => x.emp_no == empNo);
                    if (_emp == null)
                    {
                        ModelState.AddModelError("", "Threre is no information for the given employee no.");
                    }
                    else
                    {
                        _empId = _emp.id;
                    }
                }
            }
            if (_empId > 0)
            {
                return RedirectToAction("AddEmpDetails", new { empid = _empId });
            }


            return View();
        }

        public JsonResult GetEmployeeSeach(string query)
        {
            var lst = dataContext.prl_employee.AsEnumerable().Where(x => x.name.ToLower().Contains(query) || x.emp_no.Contains(query)).Select(x => new SearchEmployeeData() { id = x.id, name = x.name + " (" + x.emp_no + ")" }).ToList();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult AddEmpDetails(string msg, int? Id)
        {

            var empD = new EmployeeDetails();

            if (Id != null)
            {
                var emp = dataContext.prl_employee.SingleOrDefault(x => x.id == Id);
                empD.name = emp.name + " (" + emp.emp_no + ")";
                empD.emp_id = emp.id;
                var emP = dataContext.prl_employee_details.Where(p => p.emp_id == Id).OrderByDescending(x => x.id).First();

                empD.id = emP.id;
                empD.designation_id = emP.designation_id;
                empD.division_id = emP.division_id;
                empD.department_id = emP.department_id;
                empD.emp_status = emP.emp_status;
                empD.contract_start_date = emP.contract_start_date;
                empD.contract_end_date = emP.contract_end_date;
                empD.grade_id = emP.grade_id;
                empD.basic_salary = emP.basic_salary;
                empD.posting_location_id = emP.posting_location_id;
                empD.posting_date = emP.posting_date;

            }
            else
            {
                ModelState.AddModelError("", msg);
            }

            var lstGrades = dataContext.prl_grade.ToList();
            ViewBag.Grades = lstGrades;

            var lstDesig = dataContext.prl_designation.ToList();
            ViewBag.Designations = lstDesig;

            var lstDept = dataContext.prl_department.ToList();
            ViewBag.Departments = lstDept;

            var lstDiv = dataContext.prl_division.ToList();
            ViewBag.Divitions = lstDiv;

            var lstPostings = dataContext.prl_location.ToList();
            ViewBag.PostingLocations = lstPostings;

            return View(empD);
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult AddEmpDetails(int? empid, FormCollection collection, EmployeeDetails empD, string sButton)
        {
            var res = new OperationResult();
            try
            {
                if (sButton == "Search")
                {
                    if (empid == null && string.IsNullOrEmpty(collection["Emp_No"]))
                    {
                        return RedirectToAction("AddEmpDetails", new { msg = "Please select an employee or put employee Id" });
                    }
                    else
                    {
                        if (empid != null)
                        {
                            return RedirectToAction("AddEmpDetails", new { Id = empid });
                        }
                        else
                        {
                            string empNo = collection["Emp_No"];
                            var emp = dataContext.prl_employee.SingleOrDefault(x => x.emp_no == empNo);

                            if (emp == null)
                            {
                                return RedirectToAction("AddEmpDetails", new { msg = "Threre is no information for the selected employee" });
                            }
                            else
                            {
                                return RedirectToAction("AddEmpDetails", new { Id = emp.id });
                            }
                        }
                    }
                }
                else
                {

                    var empDfromDb = dataContext.prl_employee_details.Where(p => p.emp_id == empD.emp_id).OrderByDescending(x => x.id).First();
                    if (empDfromDb.designation_id == empD.designation_id && empDfromDb.department_id == empD.department_id
                        && empDfromDb.division_id == empD.division_id && empDfromDb.emp_status == empD.emp_status
                        && empDfromDb.contract_start_date == empD.contract_start_date && empDfromDb.contract_end_date == empD.contract_end_date
                        && empDfromDb.grade_id == empD.grade_id && empDfromDb.basic_salary == empD.basic_salary
                        && empDfromDb.posting_location_id == empD.posting_location_id && empDfromDb.posting_date == empD.posting_date)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var nwEmp = Mapper.Map<prl_employee_details>(empD);
                        if (nwEmp.emp_status == "Regular")
                        {
                            nwEmp.contract_start_date = null;
                            nwEmp.contract_end_date = null;
                        }
                        nwEmp.updated_by = User.Identity.Name;
                        nwEmp.updated_date = DateTime.Now;

                        dataContext.prl_employee_details.Add(nwEmp);
                        dataContext.SaveChanges();
                        res.IsSuccessful = true;
                        res.Message = "Employee information is successfully updated.";
                        TempData.Add("msg", res);

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }



            var lstGrades = dataContext.prl_grade.ToList();
            ViewBag.Grades = lstGrades;

            var lstDesig = dataContext.prl_designation.ToList();
            ViewBag.Designations = lstDesig;

            var lstDept = dataContext.prl_department.ToList();
            ViewBag.Departments = lstDept;

            var lstDiv = dataContext.prl_division.ToList();
            ViewBag.Divitions = lstDiv;

            var lstPostings = dataContext.prl_location.ToList();
            ViewBag.PostingLocations = lstPostings;

            return View();
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult EmpConfirmation(int? id)
        {

            if (id > 0)
            {
                var _Emp = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == id);
                return View(Mapper.Map<Employee>(_Emp));
            }
            else
            {
                return View();
            }

        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult EmpConfirmation(int? empid, FormCollection collection, Employee emp, string sButton)
        {

            bool errorFound = false;
            var res = new OperationResult();

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
                        if (empid != null)
                        {
                            var _empD = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == empid);
                            var empD = Mapper.Map<Employee>(_empD);
                            ViewBag.Employee = empD;
                            return View(empD);
                        }
                        else
                        {
                            var _empD = dataContext.prl_employee.Include("prl_employee_details").AsEnumerable().SingleOrDefault(x => x.emp_no == collection["Emp_No"]);
                            if (_empD == null)
                            {
                                ModelState.AddModelError("", "Threre is no information for the given employee no.");
                            }
                            else
                            {
                                var empD = Mapper.Map<Employee>(_empD);
                                ViewBag.Employee = empD;
                                return View(empD);
                            }
                        }
                    }
                }
                else if (sButton == "Save")
                {

                    if (emp.id == 0)
                    {
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }
                    if (!errorFound)
                    {
                        var _confirmingEmp = dataContext.prl_employee.SingleOrDefault(x => x.id == emp.id);
                        _confirmingEmp.is_confirmed = 1;
                        _confirmingEmp.confirmation_date = emp.confirmation_date;
                        _confirmingEmp.is_pf_member = Convert.ToSByte(emp.is_pf_member);
                        _confirmingEmp.updated_by = User.Identity.Name;
                        _confirmingEmp.updated_date = DateTime.Today;

                        dataContext.SaveChanges();

                        res.IsSuccessful = true;
                        res.Message = " Employee confirmed successfully.";
                        TempData.Add("msg", res);

                        return RedirectToAction("EmpConfirmation", new { id = _confirmingEmp.id });
                    }
                }
                else if (sButton == "Undo")
                {
                    if (emp.id > 0)
                    {
                        var salaryProcess = dataContext.prl_salary_process_detail.Where(p => p.emp_id == emp.id);
                        if (salaryProcess.Count() > 0)
                        {
                            errorFound = true;
                            ModelState.AddModelError("", "Confirmation of this employee can't be undo.");
                        }
                    }
                    else
                    {
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }
                    if (!errorFound)
                    {
                        var _confirmingEmp = dataContext.prl_employee.SingleOrDefault(x => x.id == emp.id);
                        _confirmingEmp.is_confirmed = 0;
                        _confirmingEmp.confirmation_date = null;
                        _confirmingEmp.is_pf_member = Convert.ToSByte(false);
                        _confirmingEmp.updated_by = User.Identity.Name;
                        _confirmingEmp.updated_date = DateTime.Today;

                        dataContext.SaveChanges();

                        res.IsSuccessful = true;
                        res.Message = " Confirmation cancelled.";
                        TempData.Add("msg", res);


                        return RedirectToAction("EmpConfirmation", new { id = _confirmingEmp.id });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            return View();
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult EmpBankInfo(string empIdbankId)
        {

            var _Emp = new Employee();

            if (empIdbankId != null)
            {
                string[] ids = empIdbankId.Split(',');

                int empId = Convert.ToInt32(ids[0]);
                int bankId = ids[1] == "" ? 0 : Convert.ToInt32(ids[1]);

                var emp = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == empId);
                _Emp = Mapper.Map<Employee>(emp);
                _Emp.bank_id = bankId;

                var _bankInfo = dataContext.prl_bank.ToList();
                ViewBag.Banks = _bankInfo;

                var _banches = dataContext.prl_bank_branch.Where(x => x.bank_id == bankId).ToList();
                ViewBag.Branches = _banches;

                return View(_Emp);
            }
            else
            {
                var _bankInfo = dataContext.prl_bank.Where(x => x.id == 0).ToList();
                ViewBag.Banks = _bankInfo;

                var _banches = dataContext.prl_bank_branch.Where(x => x.bank_id == 0).ToList();
                ViewBag.Branches = _banches;

                return View();
            }
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult EmpBankInfo(int? empid, FormCollection collection, Employee emp, string sButton)
        {

            var _empD = new Employee();

            bool errorFound = false;
            var res = new OperationResult();

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
                        if (empid != null)
                        {
                            var empD = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == empid);
                            _empD = Mapper.Map<Employee>(empD);
                        }
                        else
                        {
                            var empD = dataContext.prl_employee.Include("prl_employee_details").AsEnumerable().SingleOrDefault(x => x.emp_no == collection["Emp_No"]);
                            if (empD == null)
                            {
                                ModelState.AddModelError("", "Threre is no information for the given employee no.");
                            }
                            else
                            {
                                _empD = Mapper.Map<Employee>(empD);
                            }
                        }
                    }
                }
                else if (sButton == "Save")
                {
                    if (emp.id == 0)
                    {
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }
                    else
                    {
                        if (emp.bank_id == null)
                        {
                            errorFound = true;
                            ModelState.AddModelError("", "Please select bank.");
                        }
                        if (emp.bank_branch_id == null)
                        {
                            errorFound = true;
                            ModelState.AddModelError("", "Please select bank branch.");
                        }
                        if (string.IsNullOrEmpty(emp.account_no))
                        {
                            errorFound = true;
                            ModelState.AddModelError("", "Please provide account no.");
                        }

                        var _Emp = dataContext.prl_employee.SingleOrDefault(x => x.id == emp.id);
                        if (!errorFound)
                        {
                            _Emp.bank_id = emp.bank_id;
                            _Emp.bank_branch_id = emp.bank_branch_id;
                            _Emp.account_no = emp.account_no;
                            _Emp.updated_by = User.Identity.Name;
                            _Emp.updated_date = DateTime.Today;

                            dataContext.SaveChanges();

                            res.IsSuccessful = true;
                            res.Message = "Bank information updated successfully.";
                            TempData.Add("msg", res);
                        }
                        _empD = Mapper.Map<Employee>(_Emp);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            int bankId = emp.bank_id == null ? 0 : Convert.ToInt16(emp.bank_id);

            var _bInfo = dataContext.prl_bank.ToList();
            ViewBag.Banks = _bInfo;

            var _banch = dataContext.prl_bank_branch.Where(x => x.bank_id == bankId).ToList();
            ViewBag.Branches = _banch;

            if (_empD.id > 0)
            {
                ViewBag.Employee = _empD;
                return View(_empD);
            }
            return View();
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult AddSalaryReview()
        {

            var lstReview = dataContext.prl_salary_review.Where(x => x.id == 0).ToList();
            ViewBag.SalReview = Mapper.Map<List<SalaryReview>>(lstReview);

            return View();
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult AddSalaryReview(int? empid, FormCollection collection, string sButton)
        {
            var _empD = new Employee();
            var _salaryReview = new SalaryReview();

            bool errorFound = false;
            var res = new OperationResult();

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
                        if (empid != null)
                        {
                            var empD = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == empid);
                            _empD = Mapper.Map<Employee>(empD);
                            _salaryReview.emp_id = _empD.id;
                            _salaryReview.current_basic = _empD.prl_employee_details.OrderByDescending(x => x.id).First().basic_salary;
                        }
                        else
                        {
                            var empD = dataContext.prl_employee.Include("prl_employee_details").AsEnumerable().SingleOrDefault(x => x.emp_no == collection["Emp_No"]);
                            if (empD == null)
                            {
                                ModelState.AddModelError("", "Threre is no information for the given employee no.");
                            }
                            else
                            {
                                _empD = Mapper.Map<Employee>(empD);
                                _salaryReview.emp_id = _empD.id;
                                _salaryReview.current_basic = _empD.prl_employee_details.OrderByDescending(x => x.id).First().basic_salary;
                            }
                        }
                    }
                }
                else if (sButton == "Save")
                {
                    if (string.IsNullOrEmpty(collection["emp_id"]))
                    {
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }

                    if (!errorFound)
                    {
                        _salaryReview.emp_id = Convert.ToInt32(collection["emp_id"]);

                        var emp = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == _salaryReview.emp_id);
                        _empD = Mapper.Map<Employee>(emp);

                        _salaryReview.current_basic = Convert.ToDecimal(collection["current_basic"]);
                        _salaryReview.new_basic = Convert.ToDecimal(collection["new_basic"]);

                        string e_date = collection["effective_from"].ToString();
                        _salaryReview.effective_from = Convert.ToDateTime(e_date);
                        _salaryReview.increment_reason = collection["increment_reason"];

                        _salaryReview.description = "NA";
                        _salaryReview.created_by = User.Identity.Name;
                        _salaryReview.created_date = DateTime.Now;

                        var _review = Mapper.Map<prl_salary_review>(_salaryReview);

                        dataContext.prl_salary_review.Add(_review);

                        var empD = dataContext.prl_employee_details.Where(p => p.emp_id == _review.emp_id).OrderByDescending(x => x.id).First();
                        empD.basic_salary = _salaryReview.new_basic;
                        empD.updated_by = User.Identity.Name;
                        empD.updated_date = DateTime.Now;

                        dataContext.SaveChanges();
                        res.IsSuccessful = true;
                        res.Message = "Reviewed information saved successfully.";
                        TempData.Add("msg", res);
                    }
                    if (res.IsSuccessful)
                    {
                        _salaryReview.current_basic = Convert.ToDecimal(collection["new_basic"]);
                        _salaryReview.new_basic = 0;
                        _salaryReview.effective_from = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            if (_empD.id > 0)
            {
                var lstReviewByEmp = dataContext.prl_salary_review.Where(x => x.emp_id == _empD.id).OrderByDescending(p => p.id);
                ViewBag.SalReview = Mapper.Map<List<SalaryReview>>(lstReviewByEmp);
                ViewBag.Employee = _empD;

                return View(_salaryReview);
            }
            var lstReview = dataContext.prl_salary_review.Where(x => x.id == 0).OrderByDescending(p => p.id);
            ViewBag.SalReview = Mapper.Map<List<SalaryReview>>(lstReview);
            return View();
        }

        [PayrollAuthorize]
        [HttpGet]
        public ActionResult EditSalaryReview(string empIdbankId)
        {


            var lstReview = dataContext.prl_salary_review.Where(x => x.id == 0).ToList();
            ViewBag.SalReview = Mapper.Map<List<SalaryReview>>(lstReview);

            return View();
        }

        [PayrollAuthorize]
        [HttpPost]
        public ActionResult EditSalaryReview(int? empid, FormCollection collection, string sButton)
        {

            var _empD = new Employee();
            var _salaryReview = new SalaryReview();

            bool errorFound = false;
            var res = new OperationResult();

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
                        if (empid != null)
                        {
                            var empD = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == empid);
                            _empD = Mapper.Map<Employee>(empD);

                            var ReviewList = dataContext.prl_salary_review.Where(x => x.emp_id == _empD.id).ToList();

                            if (ReviewList.Any())
                            {
                                var Review = ReviewList.OrderByDescending(p => p.id).First();

                                _salaryReview.id = Review.id;
                                _salaryReview.emp_id = Review.emp_id;
                                _salaryReview.current_basic = Review.current_basic;
                                _salaryReview.new_basic = Review.new_basic;
                                _salaryReview.effective_from = Review.effective_from;
                                _salaryReview.increment_reason = Review.increment_reason;
                            }
                            else {

                                _salaryReview.emp_id = _empD.id;
                                _salaryReview.current_basic = _empD.prl_employee_details.OrderByDescending(x => x.id).First().basic_salary;

                            }
                            
                        }
                        else
                        {
                            var empD = dataContext.prl_employee.Include("prl_employee_details").AsEnumerable().SingleOrDefault(x => x.emp_no == collection["Emp_No"]);
                            if (empD == null)
                            {
                                ModelState.AddModelError("", "Threre is no information for the given employee no.");
                            }
                            else
                            {
                                _empD = Mapper.Map<Employee>(empD);

                                var ReviewList = dataContext.prl_salary_review.Where(x => x.emp_id == _empD.id).ToList();

                                if (ReviewList.Any())
                                {
                                    var Review = dataContext.prl_salary_review.Where(x => x.emp_id == _empD.id).OrderByDescending(p => p.id).First();

                                    _salaryReview.id = Review.id;
                                    _salaryReview.emp_id = Review.emp_id;
                                    _salaryReview.current_basic = Review.current_basic;
                                    _salaryReview.new_basic = Review.new_basic;
                                    _salaryReview.effective_from = Review.effective_from;
                                    _salaryReview.increment_reason = Review.increment_reason;
                                }
                                else
                                {
                                    _salaryReview.emp_id = _empD.id;
                                    _salaryReview.current_basic = _empD.prl_employee_details.OrderByDescending(x => x.id).First().basic_salary;

                                }
                                
                            }
                        }

                    }
                }
                else if (sButton == "Update")
                {
                    if (string.IsNullOrEmpty(collection["emp_id"]))
                    {
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }

                    if (!errorFound)
                    {
                        _salaryReview.id = Convert.ToInt16(collection["id"]);
                        _salaryReview.emp_id = Convert.ToInt16(collection["emp_id"]);

                        var emp = dataContext.prl_employee.Include("prl_employee_details").SingleOrDefault(x => x.id == _salaryReview.emp_id);
                        _empD = Mapper.Map<Employee>(emp);

                        _salaryReview.current_basic = Convert.ToDecimal(collection["current_basic"]);
                        _salaryReview.new_basic = Convert.ToDecimal(collection["new_basic"]);
                        _salaryReview.effective_from = Convert.ToDateTime(collection["effective_from"]);
                        _salaryReview.increment_reason = collection["increment_reason"];

                        var _review = dataContext.prl_salary_review.SingleOrDefault(x => x.id == _salaryReview.id);
                        _review.new_basic = _salaryReview.new_basic;
                        _review.effective_from = _salaryReview.effective_from;
                        _review.increment_reason = _salaryReview.increment_reason;

                        var empD = dataContext.prl_employee_details.Where(p => p.emp_id == _empD.id).OrderByDescending(x => x.id).First();
                        empD.basic_salary = _review.new_basic;
                        empD.updated_by = "BR";
                        empD.updated_date = DateTime.Now;

                        dataContext.SaveChanges();
                        res.IsSuccessful = true;
                        res.Message = "Reviewed information updated successfully.";
                        TempData.Add("msg", res);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            if (_empD.id > 0)
            {
                var lstReviewByEmp = dataContext.prl_salary_review.Where(x => x.emp_id == _empD.id).OrderByDescending(p => p.id);
                ViewBag.SalReview = Mapper.Map<List<SalaryReview>>(lstReviewByEmp);
                ViewBag.Employee = _empD;

                return View(_salaryReview);
            }
            var lstReview = dataContext.prl_salary_review.Where(x => x.id == 0).OrderByDescending(p => p.id);
            ViewBag.SalReview = Mapper.Map<List<SalaryReview>>(lstReview);
            return View();
        }


        public ActionResult EmployeeDiscontinue()
        {

            return View();
        }

        [HttpPost]
        public ActionResult EmployeeDiscontinue(int? empid, FormCollection collection, EmployeeDiscontinue empDisCon, string sButton)
        {


            bool errorFound = false;
            var res = new OperationResult();
            var _emp = new Employee();
            //byte disconFlag = 1;
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
                        if (empid != null)
                        {
                            var emp = dataContext.prl_employee.SingleOrDefault(x => x.id == empid);
                            _emp = Mapper.Map<Employee>(emp);
                        }
                        else
                        {
                            string enpNo = collection["Emp_No"];
                            var emp = dataContext.prl_employee.SingleOrDefault(x => x.emp_no == enpNo);
                            _emp = Mapper.Map<Employee>(emp);


                        }
                        empDisCon.emp_id = _emp.id;
                        empDisCon.empInfo = _emp.name + "(" + _emp.emp_no + ")";

                        var lst = dataContext.prl_employee_discontinue.AsEnumerable().Where(p => p.emp_id == empDisCon.emp_id);
                        if (lst.Count() > 0)
                        {
                            var conDisCon = lst.OrderByDescending(x => x.id).First();
                            if (conDisCon.is_active == "Y")
                            {
                                empDisCon.status = "Continuing";
                            }
                            else
                            {
                                empDisCon.status = "Discontinuing";
                                empDisCon.remarks = conDisCon.remarks;
                                empDisCon.discontination_type = conDisCon.discontination_type;
                                empDisCon.discontinueAfterCurrentMonth = conDisCon.with_salary == "Y" ? true : false;
                                empDisCon.continue_pf = conDisCon.continue_pf == "Y" ? true : false;
                                empDisCon.continue_gf = conDisCon.continue_gf == "Y" ? true : false;
                                empDisCon.consider_for_next_tax_year = conDisCon.consider_for_next_tax_year == "Y" ? true : false;

                            }
                        }
                        else
                        {
                            empDisCon.status = "Continuing";
                        }
                    }
                }
                else
                {
                    if (empDisCon.emp_id == 0)
                    {
                        //disconFlag = 0;
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }
                    if (empDisCon.discontinue_date == null)
                    {
                        //disconFlag = 0;
                        errorFound = true;
                        ModelState.AddModelError("", "Please select discontinuation date.");
                    }

                    var lst = dataContext.prl_employee_discontinue.AsEnumerable().Where(p => p.emp_id == empDisCon.emp_id);
                    if (lst.Count() > 0)
                    {
                        var stat = lst.OrderByDescending(x => x.id).First().is_active;
                        if (stat == "N")
                        {
                            errorFound = true;
                            ModelState.AddModelError("", "This employee is already being discontinued.");
                        }
                    }

                    if (!errorFound)
                    {
                        var empDisC = new prl_employee_discontinue();

                        empDisC.emp_id = empDisCon.emp_id;
                        empDisC.is_active = "N";
                        empDisC.discontinue_date = empDisCon.discontinue_date;
                        empDisC.discontination_type = empDisCon.discontination_type;
                        if (empDisCon.discontinueAfterCurrentMonth == true)
                            empDisC.with_salary = "Y";
                        else
                            empDisC.with_salary = "N";

                        if (empDisCon.continue_pf == true)
                            empDisC.continue_pf = "Y";
                        else
                            empDisC.continue_pf = "N";

                        if (empDisCon.continue_gf == true)
                            empDisC.continue_gf = "Y";
                        else
                            empDisC.continue_gf = "N";

                        if (empDisCon.consider_for_next_tax_year == true)
                            empDisC.consider_for_next_tax_year = "Y";
                        else
                            empDisC.consider_for_next_tax_year = "N";

                        empDisC.created_by = User.Identity.Name;
                        empDisC.created_date = DateTime.Now;

                        dataContext.prl_employee_discontinue.Add(empDisC);
                        dataContext.SaveChanges();

                        var _updatingEmp = dataContext.prl_employee.SingleOrDefault(x => x.id == empDisCon.emp_id);
                        _updatingEmp.is_active = 0;
                        dataContext.SaveChanges();

                        res.IsSuccessful = true;
                        res.Message = "Employee discontinued successfully.";
                        TempData.Add("msg", res);

                        empDisCon.status = "Discontinuing";
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            return View(empDisCon);
        }

        public ActionResult UndoEmployeeDiscontinue()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UndoEmployeeDiscontinue(int? empid, FormCollection collection, EmployeeDiscontinue empDisCon, string sButton)
        {

            bool errorFound = false;
            var res = new OperationResult();
            var _emp = new Employee();
            byte UndodisconFlag = 1;
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
                        if (empid != null)
                        {
                            var emp = dataContext.prl_employee.SingleOrDefault(x => x.id == empid);
                            _emp = Mapper.Map<Employee>(emp);
                        }
                        else
                        {
                            string enpNo = collection["Emp_No"];
                            var emp = dataContext.prl_employee.SingleOrDefault(x => x.emp_no == enpNo);
                            _emp = Mapper.Map<Employee>(emp);


                        }
                        empDisCon.emp_id = _emp.id;
                        empDisCon.empInfo = _emp.name + "(" + _emp.emp_no + ")";

                        var lst = dataContext.prl_employee_discontinue.AsEnumerable().Where(p => p.emp_id == empDisCon.emp_id);
                        if (lst.Count() > 0)
                        {
                            var conDisCon = lst.OrderByDescending(x => x.id).First();
                            if (conDisCon.is_active == "Y")
                            {
                                empDisCon.status = "Continuing";
                            }
                            else
                            {
                                empDisCon.status = "Discontinuing";
                            }
                        }
                        else
                        {
                            empDisCon.status = "Continuing";
                        }
                    }
                }
                else
                {
                    if (empDisCon.emp_id == 0)
                    {
                        UndodisconFlag = 0;
                        errorFound = true;
                        ModelState.AddModelError("", "No employee selected.");
                    }
                    if (empDisCon.continution_date == null)
                    {
                        UndodisconFlag = 0;
                        errorFound = true;
                        ModelState.AddModelError("", "Please select continuation date.");
                    }

                    if (!errorFound)
                    {
                        var lst = dataContext.prl_employee_discontinue.AsEnumerable().Where(p => p.emp_id == empDisCon.emp_id);
                        if (lst.Count() > 0)
                        {
                            var ActiveInactive = lst.OrderByDescending(x => x.id).First().is_active;
                            if (ActiveInactive == "Y")
                            {
                                UndodisconFlag = 0;
                                errorFound = true;
                                ModelState.AddModelError("", "This employee is already continuing.");
                            }
                        }
                        else
                        {
                            UndodisconFlag = 0;
                            errorFound = true;
                            ModelState.AddModelError("", "This employee is already continuing.");
                        }

                        if (UndodisconFlag == 1)
                        {
                            var UndoDisC = dataContext.prl_employee_discontinue.Where(x => x.emp_id == empDisCon.emp_id).OrderByDescending(x => x.id).First();

                            UndoDisC.is_active = "Y";
                            UndoDisC.continution_date = empDisCon.continution_date;
                            UndoDisC.updated_by = User.Identity.Name;
                            UndoDisC.updated_date = DateTime.Now;

                            dataContext.SaveChanges();

                            var _updatingEmp = dataContext.prl_employee.SingleOrDefault(x => x.id == empDisCon.emp_id);
                            _updatingEmp.is_active = 1;
                            dataContext.SaveChanges();

                            res.IsSuccessful = true;
                            res.Message = "Employee continued successfully.";
                            TempData.Add("msg", res);

                            empDisCon.status = "Continuing";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            return View(empDisCon);
        }

        public ActionResult EmployeeFreeCar()
        {
            return View();
        }

        #region Import

        [PayrollAuthorize]
        public ActionResult EmpImport(int? empid, FormCollection collection, string sButton)
        {

            if (Session["_EmpD"] != null)
                Session["_EmpD"] = null;
            if (Session["NewEmp"] != null)
                Session["NewEmp"] = null;
            if (Session["NewEmpForEdit"] != null)
                Session["NewEmpForEdit"] = null;
            if (Session["NewEmpDetailForEdit"] != null)
                Session["NewEmpDetailForEdit"] = null;

            var lists = new List<Employee>().ToPagedList(1, 1);

            if (sButton == null)
            {
                var lstEmp = dataContext.prl_employee.Include("prl_employee_details").OrderBy(x => x.emp_no);
                lists = Mapper.Map<List<Employee>>(lstEmp).ToPagedList(1, 25);
            }
            else
            {

                if (empid == null && string.IsNullOrEmpty(collection["Emp_No"]))
                {
                    //errorFound = true;
                    ModelState.AddModelError("", "Please select an employee or put employee ID");
                }
                else
                {
                    if (empid != null)
                    {
                        var _emp = dataContext.prl_employee.Include("prl_employee_details").Where(x => x.id == empid);
                        lists = Mapper.Map<List<Employee>>(_emp).ToPagedList(1, 1);
                    }
                    else
                    {
                        var _emp = dataContext.prl_employee.Include("prl_employee_details").AsEnumerable().Where(x => x.emp_no == collection["Emp_No"]);
                        if (_emp.Count() > 0)
                        {
                            lists = Mapper.Map<List<Employee>>(_emp).ToPagedList(1, 1);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Threre is no information for the given employee ID");
                        }
                    }
                }
            }
            return View(lists);
        }

        [HttpPost]
        public ActionResult EmpImport(HttpPostedFileBase EmpImportFile)
        //public ActionResult EmpImport(ImportFileViewModel model)
        {
            string filefullpath = string.Empty;
            var res = new OperationResult();
            try
            {
                if (EmpImportFile != null)
                {
                    var file = EmpImportFile;

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, file.ContentLength);
                        //do stuff with the bytes
                        string fileName = file.FileName;
                        string filePath = Path.Combine(Request.PhysicalApplicationPath, "Files\\", fileName);

                        System.IO.File.WriteAllBytes(filePath, fileBytes);

                        //File Uploaded
                        XSSFWorkbook xssfWorkbook;

                        filefullpath = filePath;

                        //StreamReader streamReader = new StreamReader(model.ImportFile.InputStream);

                        using (FileStream fileStream = new FileStream(filefullpath, FileMode.Open, FileAccess.Read))
                        {
                            xssfWorkbook = new XSSFWorkbook(fileStream);
                        }
                        var employeeList = new List<Employee>();
                        var employeeDetailsList = new List<EmployeeDetails>();

                        var employeeXlsViewModelList = new List<EmployeeXlsViewModel>();

                        //the columns
                        var properties = new string[] {
                            "EMPLOYEE_NUMBER",
                            "EMPLOYEE_NAME",
                            "FATHERS_NAME",
                            "DESIGNATION",

                            "DATE_OF_BIRTH",
                            "DATE_OF_JOINING",
                            "CONFIRMATION_DATE",
                            "CATEGORY",

                            "RELIGION",
                            "MARITAL_STATUS",
                            "GENDER",
                            "TIN",

                            "IS_FOREIGN_EXPATRIATE",
                            "GRADE",
                            "BASIC_SALARY",
                            "SPECIAL_ALLOWANCE",

                            "BASICSAL_GRADE_EFFECTIVE_DATE",
                            "SALARY_CHANGE_TYPE",
                            "DIVISION",
                            "DEPARTMENT",

                            "SECTION",
                            "SUB_SECTION",
                            "ZONE_CODE",
                            "ZONE",

                            "LOCATION",
                            "ASSIGNMENT_CHANGE_DATE",
                            "SALARY_BANK_CODE",
                            "SALARY_BANK",

                            "SALARY_BRANCH_CODE",
                            "SALARY_BRANCH",
                            "SALARY_ACCOUNT_NUMBER",
                            "SALARY_ACCOUNT_EFFECT_DATE",

                            "EMAIL_ADDRESS",
                            "MOBILE_NUMBER",
                            "COMPANY_NAME"
                        };


                        ISheet sheet = xssfWorkbook.GetSheetAt(0);

                        for (int row = 1; row <= sheet.LastRowNum; row++)
                        {
                            if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                            {

                                string EMPLOYEE_NUMBER = GetRowCellValue(sheet, row, properties, "EMPLOYEE_NUMBER", "NumericCellValue");
                                string EMPLOYEE_NAME = GetRowCellValue(sheet, row, properties, "EMPLOYEE_NAME", "StringCellValue");
                                string FATHERS_NAME = GetRowCellValue(sheet, row, properties, "FATHERS_NAME", "StringCellValue");
                                string DESIGNATION = GetRowCellValue(sheet, row, properties, "DESIGNATION", "StringCellValue");

                                string DATE_OF_BIRTH = GetRowCellValue(sheet, row, properties, "DATE_OF_BIRTH", "DateCellValue");
                                string DATE_OF_JOINING = GetRowCellValue(sheet, row, properties, "DATE_OF_JOINING", "DateCellValue");
                                string CONFIRMATION_DATE =GetRowCellValue(sheet, row, properties, "CONFIRMATION_DATE", "DateCellValue");
                                string CATEGORY = GetRowCellValue(sheet, row, properties, "CATEGORY", "StringCellValue");

                                string RELIGION = GetRowCellValue(sheet, row, properties, "RELIGION", "StringCellValue");
                                string MARITAL_STATUS = GetRowCellValue(sheet, row, properties, "MARITAL_STATUS", "StringCellValue");
                                string GENDER = GetRowCellValue(sheet, row, properties, "GENDER", "StringCellValue");
                                string TIN = GetRowCellValue(sheet, row, properties, "TIN", "StringCellValue");

                                string IS_FOREIGN_EXPATRIATE = GetRowCellValue(sheet, row, properties, "IS_FOREIGN_EXPATRIATE", "BooleanCellValue");
                                string GRADE = GetRowCellValue(sheet, row, properties, "GRADE", "StringCellValue");
                                string BASIC_SALARY = GetRowCellValue(sheet, row, properties, "BASIC_SALARY", "NumericCellValue");
                                string SPECIAL_ALLOWANCE = GetRowCellValue(sheet, row, properties, "SPECIAL_ALLOWANCE", "StringCellValue");

                                string BASICSAL_GRADE_EFFECTIVE_DATE = GetRowCellValue(sheet, row, properties, "BASICSAL_GRADE_EFFECTIVE_DATE", "DateCellValue");
                                string SALARY_CHANGE_TYPE = GetRowCellValue(sheet, row, properties, "SALARY_CHANGE_TYPE", "StringCellValue");
                                string DIVISION = GetRowCellValue(sheet, row, properties, "DIVISION", "StringCellValue");
                                string DEPARTMENT = GetRowCellValue(sheet, row, properties, "DEPARTMENT", "StringCellValue");

                                string SECTION = GetRowCellValue(sheet, row, properties, "SECTION", "StringCellValue");
                                string SUB_SECTION = GetRowCellValue(sheet, row, properties, "SUB_SECTION", "StringCellValue");
                                string ZONE_CODE = GetRowCellValue(sheet, row, properties, "ZONE_CODE", "StringCellValue");
                                string ZONE = GetRowCellValue(sheet, row, properties, "ZONE", "StringCellValue");

                                string LOCATION = GetRowCellValue(sheet, row, properties, "LOCATION", "StringCellValue");
                                string ASSIGNMENT_CHANGE_DATE = GetRowCellValue(sheet, row, properties, "ASSIGNMENT_CHANGE_DATE", "DateCellValue");
                                string SALARY_BANK_CODE = GetRowCellValue(sheet, row, properties, "SALARY_BANK_CODE", "StringCellValue");
                                string SALARY_BANK = GetRowCellValue(sheet, row, properties, "SALARY_BANK", "StringCellValue");

                                string SALARY_BRANCH_CODE = GetRowCellValue(sheet, row, properties, "SALARY_BRANCH_CODE", "StringCellValue");
                                string SALARY_BRANCH = GetRowCellValue(sheet, row, properties, "SALARY_BRANCH", "StringCellValue");
                                string SALARY_ACCOUNT_NUMBER = GetRowCellValue(sheet, row, properties, "SALARY_ACCOUNT_NUMBER", "StringCellValue");
                                string SALARY_ACCOUNT_EFFECT_DATE = GetRowCellValue(sheet, row, properties, "SALARY_ACCOUNT_EFFECT_DATE", "DateCellValue");

                                string EMAIL_ADDRESS = GetRowCellValue(sheet, row, properties, "EMAIL_ADDRESS", "StringCellValue");
                                string MOBILE_NUMBER = GetRowCellValue(sheet, row, properties, "MOBILE_NUMBER", "NumericCellValue");
                                string COMPANY_NAME = GetRowCellValue(sheet, row, properties, "COMPANY_NAME", "StringCellValue");

                                var employeeXlsViewModel = new EmployeeXlsViewModel
                                {
                                    EMPLOYEE_NUMBER = EMPLOYEE_NUMBER.ToString(),
                                    EMPLOYEE_NAME = EMPLOYEE_NAME,
                                    FATHERS_NAME = FATHERS_NAME,
                                    DESIGNATION = DESIGNATION,
                                    DATE_OF_BIRTH = DATE_OF_BIRTH.ToString(),
                                    DATE_OF_JOINING = DATE_OF_JOINING.ToString(),
                                    CONFIRMATION_DATE = CONFIRMATION_DATE.ToString(),
                                    CATEGORY = CATEGORY,
                                    RELIGION = RELIGION,
                                    MARITAL_STATUS = MARITAL_STATUS,
                                    GENDER = GENDER == "M" ? "Male" : "Female",
                                    TIN = TIN.ToString(),
                                    IS_FOREIGN_EXPATRIATE = IS_FOREIGN_EXPATRIATE,
                                    GRADE = GRADE,
                                    BASIC_SALARY = BASIC_SALARY,
                                    SPECIAL_ALLOWANCE = SPECIAL_ALLOWANCE,
                                    BASICSAL_GRADE_EFFECTIVE_DATE = BASICSAL_GRADE_EFFECTIVE_DATE.ToString(),
                                    SALARY_CHANGE_TYPE = SALARY_CHANGE_TYPE,
                                    DIVISION = DIVISION,
                                    DEPARTMENT = DEPARTMENT,
                                    SECTION = SECTION,
                                    SUB_SECTION = SUB_SECTION,
                                    ZONE_CODE = ZONE_CODE,
                                    ZONE = ZONE,
                                    LOCATION = LOCATION,
                                    ASSIGNMENT_CHANGE_DATE = ASSIGNMENT_CHANGE_DATE.ToString(),
                                    SALARY_BANK_CODE = SALARY_BANK_CODE,
                                    SALARY_BANK = SALARY_BANK,
                                    SALARY_BRANCH_CODE = SALARY_BRANCH_CODE,
                                    SALARY_BRANCH = SALARY_BRANCH,
                                    SALARY_ACCOUNT_NUMBER = SALARY_ACCOUNT_NUMBER.ToString(),
                                    SALARY_ACCOUNT_EFFECT_DATE = SALARY_ACCOUNT_EFFECT_DATE.ToString(),
                                    EMAIL_ADDRESS = EMAIL_ADDRESS,
                                    MOBILE_NUMBER = MOBILE_NUMBER.ToString(),
                                    COMPANY_NAME = COMPANY_NAME
                                };

                                employeeXlsViewModelList.Add(employeeXlsViewModel);

                            }
                        }

                        #region Insert To Database

                        int returnSaveChanges = 0;

                        foreach (var employeeXlsViewModel in employeeXlsViewModelList)
                        {

                            #region Employee, EmployeeDetails

                            var religion = dataContext.prl_religion.ToList().FirstOrDefault(item => item.name.ToLower() == employeeXlsViewModel.RELIGION.ToLower());
                            var company = dataContext.prl_company.ToList().FirstOrDefault(item => item.name.ToLower() == employeeXlsViewModel.COMPANY_NAME.ToLower());

                            Employee employee = new Employee()
                            {
                                emp_no = employeeXlsViewModel.EMPLOYEE_NUMBER.ToString(),
                                name = employeeXlsViewModel.EMPLOYEE_NAME,
                                religion_id = religion != null ? religion.id : 0,
                                company_id = company != null ? company.id : 0,
                                dob = Convert.ToDateTime(employeeXlsViewModel.DATE_OF_BIRTH),
                                joining_date = Convert.ToDateTime(employeeXlsViewModel.DATE_OF_JOINING),
                                gender = employeeXlsViewModel.GENDER,
                                is_active = true
                            };

                            var _prl_employee = Mapper.Map<prl_employee>(employee);
                            dataContext.prl_employee.Add(_prl_employee);
                            returnSaveChanges = dataContext.SaveChanges();

                            employeeList.Add(employee);

                            var grade = dataContext.prl_grade.ToList().FirstOrDefault(item => item.grade.ToLower() == employeeXlsViewModel.GRADE.ToLower());
                            var division = dataContext.prl_division.ToList().FirstOrDefault(item => item.name.ToLower() == employeeXlsViewModel.DIVISION.ToLower());
                            var department = dataContext.prl_department.ToList().FirstOrDefault(item => item.name.ToLower() == employeeXlsViewModel.DEPARTMENT.ToLower());
                            var designation = dataContext.prl_designation.ToList().FirstOrDefault(item => item.name.ToLower() == employeeXlsViewModel.DESIGNATION.ToLower());

                            EmployeeDetails employeeDetails = new EmployeeDetails()
                            {
                                emp_id = _prl_employee.id,
                                emp_status = employeeXlsViewModel.CATEGORY.ToString(),
                                grade_id = grade != null ? grade.id : 0,
                                division_id = division != null ? division.id : 0,
                                department_id = department != null ? department.id : 0,
                                basic_salary = Convert.ToDecimal(employeeXlsViewModel.BASIC_SALARY),
                                designation_id = designation != null ? designation.id : 0,
                                posting_location_id = 13 //dhaka
                            };

                            var _prl_employee_details = Mapper.Map<prl_employee_details>(employeeDetails);
                            dataContext.prl_employee_details.Add(_prl_employee_details);

                            returnSaveChanges = dataContext.SaveChanges();

                            employeeDetailsList.Add(employeeDetails);

                            #endregion
                        }

                        //foreach (var employee in employeeList)
                        //{
                        //    var _prl_employee = Mapper.Map<prl_employee>(employee);
                        //    dataContext.prl_employee.Add(_prl_employee);
                        //}

                        //foreach (var employeeDetails in employeeDetailsList)
                        //{
                        //    var _prl_employee_details = Mapper.Map<prl_employee_details>(employeeDetails);
                        //    dataContext.prl_employee_details.Add(_prl_employee_details);
                        //}

                        //returnSaveChanges = dataContext.SaveChanges();

                        #endregion

                        if (returnSaveChanges > 0)
                        {
                            res.IsSuccessful = true;
                            res.Message = "Employee data uploaded successfully.";
                            TempData.Add("msg", res);
                        }
                        else{
                            res.IsSuccessful = false;
                            res.Message = "Employee data do not uploaded successfully.";
                            TempData.Add("msg", res);
                        }

                        return RedirectToAction("EmpImport", "Employee");
                        
                    }
                    else
                    {
                        res.IsSuccessful = false;
                        res.Message = "Upload can not be empty.";
                        TempData.Add("msg", res);

                        return RedirectToAction("EmpImport", "Employee");
                    }

                }
                else
                {
                    //Upload file Null Message
                    res.IsSuccessful = false;
                    res.Message = "Upload can not be empty.";
                    TempData.Add("msg", res);

                    return RedirectToAction("EmpImport", "Employee");
                }

            }
            catch (Exception ex)
            {
                res.IsSuccessful = false;
                res.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                TempData.Add("msg", res);

                return RedirectToAction("EmpImport", "Employee");
            }
            finally {

                if (System.IO.File.Exists(filefullpath))
                {
                    System.IO.File.Delete(filefullpath);
                }
            }

        }

        private string GetRowCellValue(ISheet sheet, int row, string[] properties, string propertyName, string propertyType)
        {
            string cellValue = string.Empty;

            if (sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)) != null)
            {
                CellType cellType = sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)).CellType;
                if (propertyType == "NumericCellValue")
                {
                    cellValue = Convert.ToString(sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)).NumericCellValue);
                }
                else if (propertyType == "BooleanCellValue")
                {
                    cellValue = Convert.ToString(sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)).BooleanCellValue);
                }
                else if (propertyType == "DateCellValue")
                {
                    cellValue = Convert.ToString(sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)).DateCellValue);
                }
                else if (propertyType == "StringCellValue")
                {
                    cellValue = sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)).StringCellValue;
                }
                else if (propertyType == "ErrorCellValue")
                {
                    cellValue = Convert.ToString(sheet.GetRow(row).GetCell(GetColumnIndex(properties, propertyName)).ErrorCellValue);
                }
            }

            return cellValue;
        }

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            if (columnName == null)
                throw new ArgumentNullException("columnName");

            for (int i = 0; i < properties.Length; i++)
                if (properties[i].Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    //return i + 1; //excel indexes start from 1
                    return i; //excel indexes start from 0
            return 0;
        }

        #endregion
    }
}
