using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPEDU.Web.ViewModels
{
    public class KendoUiGridParamViewModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }

        public int skip { get; set; }
        public int take { get; set; }

        public Sort[] sort { get; set; }

        public Filter[] filter { get; set; }
    }

    public class Sort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class Filter
    {
        public string field { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }
}