﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollWeb.ViewModels
{
    public class SalaryReview
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }
        public int emp_id { get; set; }

        [DisplayName("Current Basic Salary")]
        public decimal current_basic { get; set; }

        [Required(ErrorMessage = "New basic can't be empty.")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary should be greater than {1}")]
        [DisplayName("New Basic Salary")]
        public decimal new_basic { get; set; }

        [DisplayName("Reason")]
        public string increment_reason { get; set; }

        public string description { get; set; }

        [Required(ErrorMessage = "Please provide effective date.")]
        [DisplayName("Effective From")]
        // [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> effective_from { get; set; }

        public string is_arrear_calculated { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public virtual Employee prl_employee { get; set; }
    }
}