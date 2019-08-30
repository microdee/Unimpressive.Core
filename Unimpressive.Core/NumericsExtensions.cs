using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static System.Math;

#pragma warning disable CS1591
namespace Unimpressive.Core
{
    /// <summary>
    /// Extension methods for System.Numerics
    /// </summary>
    public static class NumericsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static bool Eq(this float a, float b = 0.0f, float e = float.Epsilon) => Abs(a - b) < e;

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static bool Eq(this double a, double b = 0.0, double e = double.Epsilon) => Abs(a - b) < e;

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static bool Eq(this Vector2 a, Vector2 b, float e = float.Epsilon) => Vector2.Distance(a, b) < e;

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static bool Eq(this Vector3 a, Vector3 b, float e = float.Epsilon) => Vector3.Distance(a, b) < e;

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static bool Eq(this Vector4 a, Vector4 b, float e = float.Epsilon) => Vector4.Distance(a, b) < e;


        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Quaternion AsQuaternion(this Vector4 a) => new Quaternion(a.X, a.Y, a.Z, a.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector4 AsVector4(this Quaternion a) => new Vector4(a.X, a.Y, a.Z, a.W);
    }
}
#pragma warning restore CS1591
