using System;

namespace com.gpit.Model
{
    public partial class prl_salary_deductions
    {
        public int id { get; set; }
        public int salary_process_id { get; set; }
        public System.DateTime salary_month { get; set; }
        public int calculation_for_days { get; set; }
        public int emp_id { get; set; }
        public int deduction_name_id { get; set; }
        public decimal amount { get; set; }
        public Nullable<decimal> arrear_amount { get; set; }
        public virtual prl_deduction_name prl_deduction_name { get; set; }
        public virtual prl_employee prl_employee { get; set; }
        public virtual prl_salary_process prl_salary_process { get; set; }
    }
}
