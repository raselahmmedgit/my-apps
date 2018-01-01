using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGrid.Model.iTestApp.DocumentManagement
{
    [Table("DocumentInformation", Schema = "dbo")]
    public class DocumentInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GlobalId { get; set; }

        [StringLength(250)]
        public string DocumentName { get; set; }
        public byte[] DocumentByte { get; set; }
        public int? DocumentSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
