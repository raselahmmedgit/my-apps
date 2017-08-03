using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.ViewModels
{
    [NotMapped]
    public class TestQuestionViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 TestQuestionId { get; set; }

        public Int32 TestId { get; set; }

        [StringLength(150)]
        public string TestName { get; set; }

        public Int32 QuestionId { get; set; }

        public String QuestionText { get; set; }
    }
}
