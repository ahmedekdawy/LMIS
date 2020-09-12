using LMIS.Infrastructure.Data.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LMIS.Bll.Helpers
{
    public static class Extensions
    {
        public static bool HasNoValue(this GlobalString gstr)
        {
            return gstr.IsNullOrWhiteSpace();
        }

        public static bool HasNoValue(this string str, string ignoredValues = "")
        {
            return string.IsNullOrWhiteSpace(str) || ignoredValues.Split(',').Any(i => i == str);
        }

        public static bool IsNotASubCode(this string str, string ignoredValues = "")
        {
            return str.HasNoValue(ignoredValues) || str.Length != 8;
        }

        public static bool HasNoValue(this List<string> lst, string ignoredValues = "")
        {
            return lst == null || lst.Count < 1 || lst.All(i => i.HasNoValue(ignoredValues));
        }
        public static bool IsNotASubCode(this List<string> lst, string ignoredValues = "")
        {
            return lst == null || lst.Count < 1 || lst.Any(i => i.IsNotASubCode(ignoredValues));
        }

        public static bool IsNotUnique(this List<string> lst)
        {
            return lst.Count != lst.Unique().Count;
        }

        public static List<string> Unique(this List<string> lst)
        {
            return lst.Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();
        }

        public static List<string> Trim(this List<string> lst)
        {
            return lst.Select(s => s.Trim()).ToList();
        }

        public static List<string> DropNoValue(this List<string> lst, string ignoredValues = "")
        {
            return lst.Where(s => !s.HasNoValue(ignoredValues)).ToList();
        }
    }
}