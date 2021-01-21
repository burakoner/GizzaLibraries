using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string ToQueryString(this NameValueCollection parameters, bool urlEncode = false)
        {
            List<string> items = new List<string>();
            foreach (string name in parameters)
            {
                if (urlEncode)
                {
                    items.Add(string.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name])));
                }
                else
                {
                    items.Add(string.Concat(name, "=", parameters[name]));
                }
            }

            return string.Join("&", items);
        }
    }
}