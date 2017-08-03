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
    public class TestWiseQuestionViewModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 TestWiseQuestionId { get; set; }

        public Int32 TestId { get; set; }
        public String TestName { get; set; }

        public Int32 QuestionId { get; set; }
        public String QuestionText { get; set; }
    }
}
