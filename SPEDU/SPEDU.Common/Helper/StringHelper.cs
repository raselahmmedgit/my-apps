﻿using System.Text;

namespace SPEDU.Common.Helper
{
    public static class StringHelper
    {
        public static string ConnectStrings(string connector, params string[] items)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    output.Append(items[i]);

                    //if the current item is not the last item
                    if (i < items.Length - 1)
                        output.Append(connector);
                }
            }

            if (output.ToString().EndsWith(connector))
                output.Remove(output.ToString().Length - connector.Length, connector.Length);

            return output.ToString();
        }

    }
}
