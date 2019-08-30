using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimpressive.Core
{
    /// <summary>
    /// Static class for following object changes
    /// </summary>
    public static class ObjectChange
    {
        /// <summary>
        /// Object change counters based on hashcode of object
        /// </summary>
        private static Dictionary<int, int> ChangeCounters { get; } = new Dictionary<int, int>();

        /// <summary>
        /// Register that this object have been changed
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The current change counter value</returns>
        public static int NotifyChange(this object obj)
        {
            var hc = obj.GetHashCode();
            if (ChangeCounters.ContainsKey(hc))
                ChangeCounters[hc]++;
            else ChangeCounters.Add(hc, 1);
            return ChangeCounters[hc];
        }

        /// <summary>
        /// Check if this object have been changed since last checked from a provided scope
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="refchangecount">A reference to a counter provided by the caller. CheckChange will update this counter, caller should not change it themselves.</param>
        /// <returns>True if object has been notified to change since last check</returns>
        public static bool CheckChanged(this object obj, ref int refchangecount)
        {
            var hc = obj.GetHashCode();
            if (ChangeCounters.ContainsKey(hc))
            {
                var res = ChangeCounters[hc] != refchangecount;
                refchangecount = ChangeCounters[hc];
                return res;
            }
            else
            {
                refchangecount = -1;
                return false;
            }
        }
    }
}
