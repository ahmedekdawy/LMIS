using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Dal.Helpers
{
    public static class Extensions
    {
        public static DateTime AsUtc(this DateTime dt)
        {
            return dt.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(dt, DateTimeKind.Utc) : dt.ToUniversalTime();
        }

        public static string Limit(this string str, int maxLength)
        {
            return str.Length > maxLength ? str.Substring(0, maxLength) : str;
        }

        public static GlobalString Reduce(this GlobalString gs, int langId)
        {
            return gs.ToReducedCopy((Language)langId, true);
        }
    }
}