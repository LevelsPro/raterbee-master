using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicaitonGeneration.Helpers
{
    public static class Extensions
    {
        public static string EmptyIfNull(this string str)
        {
            if (str == null) return string.Empty;
            return str;
        }
        public static string PopFirst(ref string[] list)
        {
            string result = "";
            List<string> stringList = new List<string>(list);
            if (list.Length > 0)
            {
                result = list[0];
                stringList.RemoveAt(0);
                list = stringList.ToArray();
            }
            return result;
        }

        public static int PopFirst(ref int[] list)
        {
            int result = 0;
            List<int> intlist = new List<int>(list);
            if (list.Length > 0)
            {
                result = list[0];
                intlist.RemoveAt(0);
                list = intlist.ToArray();
            }
            return result;
        }

        public static int? TryParseInt(this string source)
        {
            int value = 0;
            if (!int.TryParse(source, out value))
                value = 0;

            return value;
        }

        public static decimal? TryParseDecimal(this string source)
        {
            decimal value = 0;
            if (!decimal.TryParse(source, out value))
                value = 0;

            return value;
        }

        public static DateTime? TryParseDate(this string source)
        {
            DateTime value = DateTime.MinValue;
            if (!DateTime.TryParse(source, out value))
                value = DateTime.MinValue;

            return value;
        }
    }
}