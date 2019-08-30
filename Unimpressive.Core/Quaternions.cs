using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Unimpressive.Core
{
    /// <summary>
    /// Quaternion utilities
    /// </summary>
    public static class Quaternions
    {
        /// <summary>
        /// The difference rotation between 2 quaternions
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Quaternion Diff(this Quaternion a, Quaternion b) => Quaternion.Multiply(Quaternion.Inverse(a), b);

        /// <summary>
        /// The angle between 2 quaternions in radians
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float AngleDiff(this Quaternion a, Quaternion b)
        {
            var diffq = Diff(a, b);
            return 2.0f * (float)Atan2(diffq.AsVector4().xyz().Length(), diffq.W);
        }
    }
}
