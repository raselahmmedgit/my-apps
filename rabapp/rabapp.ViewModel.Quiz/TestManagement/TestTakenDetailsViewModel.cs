using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace rabapp.ViewModel.Quiz.TestManagement
{
    [NotMapped]
    public class TestTakenDetailsViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestTakenDetailsId { get; set; }

        public int TestTakenId { get; set; }

        public int TestId { get; set; }
        public string TestName { get; set; }

        public int QuestionId { get; set; }
        public String QuestionText { get; set; }

        public int QuestionAnswerOptionId { get; set; }
        public string QuestionAnswerOptionText { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}
