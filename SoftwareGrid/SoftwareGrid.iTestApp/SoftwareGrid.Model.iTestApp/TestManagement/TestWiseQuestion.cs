using System;
using System.ComponentModel.DataAnnotations;
using SoftwareGrid.Model.iTestApp.QuestionManagement;

namespace SoftwareGrid.Model.iTestApp.TestManagement
{
    public class TestWiseQuestion
    {
        [Key]
        public int Sl { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Test Test { get; set; }
    }
}
