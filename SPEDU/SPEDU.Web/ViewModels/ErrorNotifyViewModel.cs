using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPEDU.Web.ViewModels
{
    public class ErrorNotifyViewModel
    {
        public string ErrorIcon { get; set; }

        public string ErrorType { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime ErrorDate { get; set; }
    }
}