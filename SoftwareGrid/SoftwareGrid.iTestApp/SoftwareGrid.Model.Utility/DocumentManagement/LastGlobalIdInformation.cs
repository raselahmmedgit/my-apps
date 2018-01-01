using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.Utility.DocumentManagement
{
    [Table("LastGlobalIdInformation", Schema = "dbo")]
    public class LastGlobalIdInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SL { get; set; }
        public int LastGlobalId { get; set; }
        public bool IsRowLock { get; set; }
    }
}



