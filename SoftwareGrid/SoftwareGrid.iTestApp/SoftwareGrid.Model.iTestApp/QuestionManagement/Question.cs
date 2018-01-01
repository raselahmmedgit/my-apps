using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SoftwareGrid.Model.iTestApp.QuestionManagement
{
    [Table("Question", Schema = "dbo")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionId { get; set; }

        public int GlobalId { get; set; }

        [ForeignKey("QuestionCategory")]
        public int QuestionCategoryId { get; set; }

        [Required]
        [AllowHtml]
        public string QuestionText { get; set; }

        [StringLength(500)]
        public string QuestionImageName { get; set; }

        [StringLength(500)]
       
        public string Tags { get; set; }

        public int NoOfAnswerOption { get; set; }

        public decimal Marks { get; set; }

        public bool IsMultipleAnswer { get; set; }

        public int DifficultyLevel { get; set; }
        
        [AllowHtml]
        public string AnswerExplanation { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual QuestionCategory QuestionCategory { get; set; }

        #region NotMapped

        [NotMapped]
        public string QuestionCategoryName { get; set; }

        [NotMapped]
        public string QuestionImagePath { get; set; }

        [NotMapped]
        public string QuestionImage { get; set; }

        [NotMapped]
        public string AnswerOptionText { get; set; }

        [NotMapped]
        public string AnswerOptionImagePath { get; set; }

        [NotMapped]
        public string AnswerOptionImage { get; set; }

        [NotMapped]
        public string AnswerOptionImageName { get; set; }

        [NotMapped]
        public bool IsCorrectAnswer { get; set; }

        [NotMapped]
        public string AnswerOptionList { get; set; }

        [NotMapped]
        public virtual IEnumerable<QuestionAnswerOption> QuestionAnswerOptionList { get; set; }

        [NotMapped]
        public int? TestId { get; set; }

        [NotMapped]
        public int TotalRecordCount { get; set; }

        #endregion

    }
}
