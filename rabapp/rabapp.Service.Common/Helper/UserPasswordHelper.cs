using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Service.Common.Helper
{
    public static class UserPasswordHelper
    {
        public static byte[] ConvertToByteArray(string password, Encoding encoding)
        {
            return encoding.GetBytes(password);
        }

        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static byte[] ConvertToByteArray(string password)
        {
            return Encoding.ASCII.GetBytes(password);
        }

        public static byte[] ConvertToByteArray(string userName, string password, Encoding encoding)
        {
            return encoding.GetBytes(userName + password);
        }

        public static String ToBinary(string userName, Byte[] data)
        {
            string userNamePassword = string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
            string password = string.Empty;
            int indexUserName = userNamePassword.IndexOf(userName, System.StringComparison.Ordinal);
            if (indexUserName != -1)
            {
                password = userNamePassword.Remove(indexUserName);
            }
            
            return password;
        }

        public static byte[] ConvertToByteArray(string userName, string password)
        {
            return Encoding.ASCII.GetBytes(userName + password);
        }
    }
}
