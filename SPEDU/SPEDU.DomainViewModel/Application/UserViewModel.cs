﻿using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.Models.Application;

namespace SPEDU.DomainViewModel.Application
{
    [NotMapped]
    public class UserViewModel : User
    {
        public string UserPhotoPath { get; set; }

        public string FullName { get; set; }
    }
}
