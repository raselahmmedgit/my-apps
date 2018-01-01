using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareGrid.Model.iTestApp.SecurityManagement;

namespace SoftwareGrid.Model.iTestApp.TestManagement
{
    [Table("FavoriteTest", Schema = "dbo")]
    public class FavoriteTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SL { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }  
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
    }

   
}
