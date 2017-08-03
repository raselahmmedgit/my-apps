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
        public int TestQuestionId { get; set; }

        public int TestId { get; set; }

        [StringLength(150)]
        public string TestName { get; set; }

        public int QuestionId { get; set; }

        public string QuestionText { get; set; }
    }
}
