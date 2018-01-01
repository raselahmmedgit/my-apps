using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SoftwareGrid.Model.iTestApp.QuestionManagement
{
    [Table("QuestionAnswerOption", Schema = "dbo")]
    public class QuestionAnswerOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerOptionId { get; set; }

        public int QuestionId { get; set; }

        public int GlobalId { get; set; }

        [Required]
        [AllowHtml]
        public string AnswerOptionText { get; set; }

        [StringLength(500)]
        public string AnswerOptionImageName { get; set; }

        public bool IsCorrectAnswer { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }
        public virtual Question Question { get; set; }
    }
}
