using SPEDU.Domain.Models.Application;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Helpers
{
    [NotMapped]
    public class CurrentUser : User
    {
    }
}
