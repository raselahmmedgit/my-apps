﻿using rabapp.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Model.Quiz.TestManagement
{
    [Table("TestCategory", Schema = "dbo")]
    public class TestCategory : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150)]
        public string TestCategoryName { get; set; }

        [StringLength(50)]
        public string TestCategoryIcon { get; set; }

    }
}