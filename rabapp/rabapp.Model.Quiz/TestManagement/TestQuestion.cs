﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace rabapp.Model.Quiz.TestManagement
{
    [Table("TestQuestion", Schema = "dbo")]
    public class TestQuestion : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 TestQuestionId { get; set; }

        public Int32 TestId { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        public Int32 QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

    }
}