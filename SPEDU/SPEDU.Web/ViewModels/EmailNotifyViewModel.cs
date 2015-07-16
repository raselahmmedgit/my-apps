using SPEDU.DomainViewModel.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPEDU.Web.ViewModels
{
    public class EmailNotifyViewModel
    {
        public UserViewModel UserViewModel { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        public DateTime EmailDate { get; set; }
    }
}