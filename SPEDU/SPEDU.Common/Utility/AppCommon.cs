using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEDU.Common.Utility
{
    public static class Password
    {
        public static byte[] GetBytes(string password)
        {
            byte[] bytPassword = Encoding.ASCII.GetBytes(password); ;
            return bytPassword;
        }

        public static string GetString(byte[] password)
        {
            string strPassword = Encoding.ASCII.GetString(password);
            return strPassword;
        }

    }
}
