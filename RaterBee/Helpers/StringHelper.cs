using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaterBee.Helpers
{
    public static class StringHelper
    {
        public static string EmptyIfNull(this string str)
        {
            if (str == null) return string.Empty;
            return str;
        }
    }
}