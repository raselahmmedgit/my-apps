using System;

namespace com.gpit.Model
{
    public partial class prl_bonus_hold
    {
        public int id { get; set; }
        public int emp_id { get; set; }
        public int bonus_name_id { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string is_holded { get; set; }
        public string hold_reason { get; set; }
        public Nullable<System.DateTime> hold_from { get; set; }
        public Nullable<System.DateTime> hold_to { get; set; }
        public Nullable<System.DateTime> unhold_date { get; set; }
        public string unhold_reason { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public virtual prl_bonus_name prl_bonus_name { get; set; }
        public virtual prl_employee prl_employee { get; set; }
    }
}