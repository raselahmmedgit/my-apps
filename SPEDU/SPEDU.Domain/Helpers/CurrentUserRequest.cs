using System;

namespace SPEDU.Domain.Helpers
{
    public class CurrentUserRequest
    {
        public Int32 UserId { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String AreaName { get; set; }
        public String ControllerName { get; set; }
        public String ActionName { get; set; }
    }
}
