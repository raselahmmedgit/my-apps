using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Model.Common
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            DeletedDate = DateTime.Now;
            IsDelete = false;
        }

        //public int Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public bool IsDelete { get; set; }

        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        #region NotMapped

        public virtual string ActionLink { get; set; }

        public virtual bool HasCreate { get; set; }

        public virtual bool HasUpdate { get; set; }

        public virtual bool HasDelete { get; set; }

        #endregion
    }
}
