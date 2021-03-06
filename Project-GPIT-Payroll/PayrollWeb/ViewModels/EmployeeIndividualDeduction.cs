﻿using PayrollWeb.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollWeb.ViewModels
{
    public class EmployeeIndividualDeduction
    {
        [HiddenInput(DisplayValue = true)]
        public int id { get; set; }

        [HiddenInput(DisplayValue = true)]
        public int emp_id { get; set; }

        [DisplayName("Deduction Name")]
        [Required(ErrorMessage = "Deduction Name cannot be empty.")]
        public int deduction_name_id { get; set; }

        [InputBoxBinaryChoice("percentage")]
        [DisplayName("Flat Amount")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Enter a non-negetive decimal number.")]
        public Nullable<decimal> flat_amount { get; set; }

        [DisplayName("Percentage Amount")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Enter a non-negetive decimal number.")]
        public Nullable<decimal> percentage { get; set; }

        [DisplayName("Effective From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date cannot be empty.")]
        public Nullable<System.DateTime> effective_from { get; set; }
        
        [DisplayName("Effective To")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> effective_to { get; set; } //
        
        public virtual DeductionName prl_deduction_name { get; set; }
        public virtual Employee prl_employee { get; set; } //
    }
}