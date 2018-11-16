using System;

namespace com.gpit.Model
{
    public partial class prl_upload_deduction
    {
        public int id { get; set; }
        public int deduction_name_id { get; set; }
        public int emp_id { get; set; }
        public Nullable<System.DateTime> salary_month_year { get; set; }
        public decimal amount { get; set; }
        public Nullable<decimal> percentage { get; set; }
        public Nullable<System.DateTime> effective_from { get; set; }
        public Nullable<System.DateTime> effective_to { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public virtual prl_deduction_name prl_deduction_name { get; set; }
        public virtual prl_employee prl_employee { get; set; }
    }
}
