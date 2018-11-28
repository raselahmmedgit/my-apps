﻿using System.Collections.Generic;

namespace PayrollWeb.Models
{
    public class SalaryProcessResult : IProcessResult
    {

        private readonly ProcessType processType;
        private readonly List<string> errors;
        private object _lstOfSuccessfulObj;

        public SalaryProcessResult(ProcessType processType)
        {
            this.processType = processType;
            errors = new List<string>();
        }
        public ProcessType GetProcessTypeType { get { return processType; } }

        public List<string> GetErrors { get { return errors; }  }

        public bool ErrorOccured { get; set; }

        public void AddToErrorList(string errorMsg)
        {
            errors.Add(errorMsg);
        }

        public void AddCompletedResultObjects(object lstSuccessfullObjects)
        {
            this._lstOfSuccessfulObj = lstSuccessfullObjects;
        }

        public object GetCompletedResultObjects()
        {
            return _lstOfSuccessfulObj;
        }
    }
}