using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Model.Common
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now;
            this.IsDelete = false;
        }

        //public int Id { get; set; }

        public Int32 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Int32 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Boolean IsDelete { get; set; }

        public Int32 DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        [NotMapped]
        public virtual String ActionLink { get; set; }
        [NotMapped]
        public virtual Boolean HasCreate { get; set; }
        [NotMapped]
        public virtual Boolean HasUpdate { get; set; }
        [NotMapped]
        public virtual Boolean HasDelete { get; set; }
        [NotMapped]
        public virtual String MessageType { get; set; }
        [NotMapped]
        public virtual String Message { get; set; }

        #endregion

    }

    public class BaseNotMapModel
    {
        public BaseNotMapModel()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now;
            this.IsDelete = false;
        }

        //public int Id { get; set; }
        [NotMapped]
        public Int32 CreatedBy { get; set; }
        [NotMapped]
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public Int32 UpdatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public Boolean IsDelete { get; set; }
        [NotMapped]
        public Int32 DeletedBy { get; set; }
        [NotMapped]
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        [NotMapped]
        public virtual String ActionLink { get; set; }
        [NotMapped]
        public virtual Boolean HasCreate { get; set; }
        [NotMapped]
        public virtual Boolean HasUpdate { get; set; }
        [NotMapped]
        public virtual Boolean HasDelete { get; set; }

        #endregion

    }
}
