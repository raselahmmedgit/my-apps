using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using rabapp.Model.Quiz.SecurityManagement;

namespace rabapp.Model.Quiz.TestManagement
{
    [Table("TestTaken", Schema = "dbo")]
    public class TestTaken : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestTakenId { get; set; }

        public int TestId { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [StringLength(16)]
        public string AccessCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Score { get; set; }
        [StringLength(250)]
        public string IpAddress { get; set; }

        
        

    }
}
