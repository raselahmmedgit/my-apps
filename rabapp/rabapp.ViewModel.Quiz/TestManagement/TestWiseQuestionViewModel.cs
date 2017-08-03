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
        public int TestWiseQuestionId { get; set; }

        public int TestId { get; set; }
        public string TestName { get; set; }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
}
