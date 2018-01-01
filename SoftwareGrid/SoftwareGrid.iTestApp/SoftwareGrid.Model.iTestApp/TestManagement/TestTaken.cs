using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.iTestApp.TestManagement
{
    [Table("TestTaken", Schema = "dbo")]
    public class TestTaken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TakenId { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        [StringLength(16)]
        public string AccessCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Score { get; set; }
        [StringLength(250)]
        public string IpAddress { get; set; }
        public virtual IEnumerable<TestTakenDetails> TestTakenDetails { get; set; }
        public virtual Test Test { get; set; }

    }
}
