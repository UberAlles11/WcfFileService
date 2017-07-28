using System;
using System.Collections.Generic;
using System.Linq;

namespace WcfFileService.BL.Common
{
    public static class Extensions
    {
        #region string IsNullOrEmpty
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        #endregion

        #region IsNullOrEmpty
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> me) => !me?.Any() ?? true;
        #endregion

        #region ForEach
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list.IsNullOrEmpty() || action == null) return;

            foreach (T element in list)
            {
                action(element);
            }
        }
        #endregion

        #region ForEach
        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            if (list.IsNullOrEmpty() || action == null) return;

            int index = 0;
            foreach (T element in list)
            {
                action(element, index);
                index++;
            }
        }
        #endregion
    }
}
