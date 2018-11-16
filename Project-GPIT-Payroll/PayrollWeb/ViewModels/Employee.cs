﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollWeb.ViewModels
{
    public class Employee
    {

        public Employee()
        {
            prl_employee_details = new List<EmployeeDetails>();
        }

        [HiddenInput(DisplayValue = true)]
        public int id { get; set; }

        [Required(ErrorMessage = "No. can not be empty.")]
        [DisplayName("Employee No.")]
        public string emp_no { get; set; }

        [Required(ErrorMessage = "Name can not be empty.")]
        [DisplayName("Employee Name")]
        public string name { get; set; }

        [DisplayName("Present Address")]
        public string present_address { get; set; }

        [DisplayName("Permanent Address")]
        public string permanent_address { get; set; }

        [DisplayName("Phone")]
        public string phone { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter a valid email address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Religion can not be empty.")]
        [DisplayName("Religion")]
        public int religion_id { get; set; }

        [DisplayName("Gender")]
        public string gender { get; set; }

        [DisplayName("Marital Status")]
        public string marital_status { get; set; }

        [Required(ErrorMessage = "Company can not be empty.")]
        [DisplayName("Company")]
        public Nullable<int> company_id { get; set; }

        [DisplayName("Father Name")]
        public string father_name { get; set; }

        [DisplayName("Mother Name")]
        public string mother_name { get; set; }

        [Required(ErrorMessage = "Date of birth can not be empty.")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime dob { get; set; }

        [Required(ErrorMessage = "Join date can not be empty.")]
        [DisplayName("Join Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime joining_date { get; set; }

        [DisplayName("TIN")]
        public string tin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime confirmation_date { get; set; }

        public bool is_confirmed { get; set; }
        public bool is_pf_member { get; set; }
        public bool is_gf_member { get; set; }
        public bool is_active { get; set; }

        public Nullable<int> bank_id { get; set; }
        public Nullable<int> bank_branch_id { get; set; }
        public string account_no { get; set; }

        public List<EmployeeDetails> prl_employee_details { get; set; }
        public virtual Bank prl_bank { get; set; }
        public virtual BankBranch prl_bank_branch { get; set; }
        public virtual Company prl_company { get; set; }
        public virtual Religion prl_religion { get; set; }
    }
}