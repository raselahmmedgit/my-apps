using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.SecurityManagement;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Model.Quiz.QuestionManagement;

namespace rabapp.Model.Quiz.ViewModels
{
    [NotMapped]
    public class TestViewModel : Test
    {
        public string TestCategoryName { get; set; }

        public string TestIconPath { get; set; }

        public virtual List<TestQuestion> TestQuestions { get; set; }
        
        public virtual List<Question> Questions { get; set; }
    }
}
