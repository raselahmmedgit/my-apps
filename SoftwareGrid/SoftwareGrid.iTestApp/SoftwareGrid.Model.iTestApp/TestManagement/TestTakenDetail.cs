using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.iTestApp.TestManagement
{
    [Table("TestTakenDetail", Schema = "dbo")]
    public class TestTakenDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sl { get; set; }
        public int TakenId { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerOptionId { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}


