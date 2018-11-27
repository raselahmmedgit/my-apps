﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollWeb.ViewModels
{
    public class EmployeeDetails
    {
        public EmployeeDetails()
        {
        }
       
        public virtual Designation prl_designation { get; set; }
        public virtual Department prl_Department { get; set; }
        public virtual Division prl_division { get; set; }
        public virtual Grade prl_grade { get; set; }
        public virtual PostingLocation prl_location { get; set; }

        [HiddenInput(DisplayValue = true)]
        public int id { get; set; }

        public int emp_id { get; set; }

        [Required(ErrorMessage = "Status can't be empty.")]
        [DisplayName("Employee Status")]
        public string emp_status { get; set; }

        [Required(ErrorMessage = "Grade can't be empty.")]
        [DisplayName("Grade")]
        public int grade_id { get; set; }

        [Required(ErrorMessage = "Division can't be empty.")]
        [DisplayName("Division")]
        public Nullable<int> division_id { get; set; }

        [Required(ErrorMessage = "Department can't be empty.")]
        [DisplayName("Department")]
        public Nullable<int> department_id { get; set; }

        [Required(ErrorMessage = "Salary can't be empty.")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary should be greater than {1}")]
        [DisplayName("Basic Salary")]
        public decimal basic_salary { get; set; }

        [Required(ErrorMessage = "Designation can not be empty.")]
        [DisplayName("Designation")]
        public int designation_id { get; set; }

        [Required(ErrorMessage = "Location can't be empty.")]
        [DisplayName("Posting Location")]
        public Nullable<int> posting_location_id { get; set; }

        [DisplayName("Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> posting_date { get; set; }
        
        [DisplayName("Contract From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> contract_start_date { get; set; }
        
        [DisplayName("Contract To")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> contract_end_date { get; set; }
        
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }

        public string name { get; set; }
    }
}