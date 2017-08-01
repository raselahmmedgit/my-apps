﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.SecurityManagement;

namespace rabapp.Model.Quiz.ViewModels
{
    [NotMapped]
    public class UserViewModel : User
    {
        public IEnumerable<RoleViewModel> RoleViewModelList { get; set; }
    }
}