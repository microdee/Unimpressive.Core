using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using static System.Math;

namespace Unimpressive.Core
{
    /// <summary>
    /// Collection of standard mathematic operations not present in System.Math.
    /// Some functions have been ported from https://github.com/vvvv/vvvv-sdk/blob/develop/common/src/core/Utils/Math/VMath.cs
    /// </summary>
    public static class UnMath
    {
        /// <summary>
        /// Modi for the Map and Clamp functions specifying out-of-bounds behavior
        /// </summary>
        public enum MapMode
        {
            /// <summary>
            /// Maps the value continously
            /// </summary>
            Float,
            /// <summary>
            /// Maps the value, but clamps it at the min/max borders of the output range
            /// </summary>
            Clamp,
            /// <summary>
            /// Maps the value, but repeats it into the min/max range, like a modulo function
            /// </summary>
            Wrap,
            /// <summary>
            /// Maps the value, but mirrors it into the min/max range, always against either start or end, whatever is closer
            /// </summary>
            Mirror
		}

        #region Constants

        /// <summary>
        /// Pi, as you know it
        /// </summary>
        public const float Pi = 3.1415926535897932384626433832795f;

        /// <summary>
        /// Pi * 2
        /// </summary>
        public const float TwoPi = 6.283185307179586476925286766559f;

        /// <summary>
        /// 1 / Pi, multiply by this if you have to divide by Pi
        /// </summary>
        public const float PiRez = 0.31830988618379067153776752674503f;

        /// <summary>
        /// 2 / Pi, multiply by this if you have to divide by 2*Pi
        /// </summary>
        public const float TwoPiRez = 0.15915494309189533576888376337251f;

        /// <summary>
        /// Conversion factor from cycles to radians, (2 * Pi)
        /// </summary>
        public const float CycToRad = 6.28318530717958647693f;
        /// <summary>
        /// Conversion factor from radians to cycles, 1/(2 * Pi)
        /// </summary>
        public const float RadToCyc = 0.159154943091895335769f;
        /// <summary>
        /// Conversion factor from degree to radians, (2 * Pi)/360
        /// </summary>
        public const float DegToRad = 0.0174532925199432957692f;
        /// <summary>
        /// Conversion factor from radians to degree, 360/(2 * Pi)
        /// </summary>
        public const float RadToDeg = 57.2957795130823208768f;
        /// <summary>
        /// Conversion factor from degree to radians, 1/360
        /// </summary>
        public const float DegToCyc = 0.00277777777777777777778f;
        /// <summary>
        /// Conversion factor from radians to degree, 360
        /// </summary>
        public const float CycToDeg = 360.0f;

        #endregion

        #region Random

        /// <summary>
        /// A random object for conveninece
        /// </summary>
        public static Random Rand = new Random();

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static float NextFloat(this Random rand) => (float)rand.NextDouble();

        /// <summary>
        /// Creates a random 2d vector.
        /// </summary>
        /// <returns>Random vector with its components in the range [-1..1].</returns>
        public static Vector2 RandomVector2() =>
            new Vector2(Rand.NextFloat() * 2 - 1, Rand.NextFloat() * 2 - 1);

        /// <summary>
        /// Creates a random 3d vector.
        /// </summary>
        /// <returns>Random vector with its components in the range [-1..1].</returns>
        public static Vector3 RandomVector3() =>
            new Vector3(Rand.NextFloat() * 2 - 1,
                Rand.NextFloat() * 2 - 1,
                Rand.NextFloat() * 2 - 1
            );

        /// <summary>
        /// Creates a random 4d vector.
        /// </summary>
        /// <returns>Random vector with its components in the range [-1..1].</returns>
        public static Vector4 RandomVector4() =>
            new Vector4(Rand.NextFloat() * 2 - 1,
                Rand.NextFloat() * 2 - 1,
                Rand.NextFloat() * 2 - 1,
                Rand.NextFloat() * 2 - 1
            );

        #endregion

        /// <summary>
        /// Factorial function, DON'T FEED ME WITH LARGE NUMBERS !!! (n>10 can be huge)
        /// </summary>
        /// <param name="n"></param>
        /// <returns>The product n * n-1 * n-2 * n-3 * .. * 3 * 2 * 1</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static long Factorial(long n) => n == 0 ? 1 : Abs(n) * Factorial(Abs(n));

        /// <summary>
        /// Binomial function
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns>The number of k-tuples of n items</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static long Binomial(long n, long k) => Factorial(Abs(n)) / (Factorial(k) * Factorial(Abs(n) - k));

        /// <summary>
        /// Solves a quadratic equation a*x^2 + b*x + c for x
        /// </summary>
        /// <param name="a">Coefficient of x^2</param>
        /// <param name="b">Coefficient of x</param>
        /// <param name="c">Constant</param>
        /// <param name="x1">First solution</param>
        /// <param name="x2">Second solution</param>
        /// <returns>Number of solution, 0, 1, 2 or int.MaxValue</returns>
        public static int SolveQuadratic(double a, double b, double c, out double x1, out double x2)
        {
            x1 = 0;
            x2 = 0;

            if (a == 0)
            {
                if ((b == 0) && (c == 0))
                {
                    return int.MaxValue;
                }
                else
                {
                    x1 = -c / b;
                    x2 = x1;
                    return 1;
                }
            }
            else
            {
                double D = b * b - 4 * a * c;

                if (D > 0)
                {

                    D = Sqrt(D);
                    x1 = (-b + D) / (2 * a);
                    x2 = (-b - D) / (2 * a);
                    return 2;
                }
                else
                {
                    if (D == 0)
                    {
                        x1 = -b / (2 * a);
                        x2 = x1;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        #region Modulo

        /// <summary>
        /// Modulo function with the property, that the remainder of a division z / d
        /// and z &lt; 0 is positive. For example: zmod(-2, 30) = 28.
        /// </summary>
        /// <param name="z"></param>
        /// <param name="d"></param>
        /// <returns>Remainder of division z / d.</returns>
        public static int Zmod(int z, int d)
        {
            if (z >= d)
                return z % d;
            else if (z < 0)
            {
                int remainder = z % d;
                return remainder == 0 ? 0 : remainder + d;
            }
            else
                return z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static float Frac(this float a) => a - (float)Floor(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static double Frac(this double a) => a - Floor(a);

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static float Fmod(this float a, float b) => (a / b).Frac() * b;

        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static double Fmod(this double a, double b) => (a / b).Frac() * b;

        #endregion

        #region Clamp

        /// <summary>
        /// Clamp function, clamps a value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static double Clamp(double x, double min, double max) => Min(Max(x, Min(min, max)), Max(min, max));

        /// <summary>
        /// Clamp function, clamps a value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static float Clamp(float x, float min, float max) => Min(Max(x, Min(min, max)), Max(min, max));

        /// <summary>
        /// Clamp function, clamps a value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static long Clamp(long x, long min, long max) => Min(Max(x, Min(min, max)), Max(min, max));

        /// <summary>
        /// Clamp function, clamps a value into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static int Clamp(int x, int min, int max) => Min(Max(x, Min(min, max)), Max(min, max));

        /// <summary>
        /// Clamp function, clamps a vector into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector2 Clamp(Vector2 x, Vector2 min, Vector2 max) =>
            new Vector2(Clamp(x.X, min.X, max.X), Clamp(x.Y, min.Y, max.Y));

        /// <summary>
        /// Clamp function, clamps a vector into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector2 Clamp(Vector2 x, float min, float max) =>
            new Vector2(Clamp(x.X, min, max), Clamp(x.Y, min, max));

        /// <summary>
        /// Clamp function, clamps a vector into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector3 Clamp(Vector3 x, Vector3 min, Vector3 max) =>
            new Vector3(Clamp(x.X, min.X, max.X), Clamp(x.Y, min.Y, max.Y), Clamp(x.Z, min.Z, max.Z));

        /// <summary>
        /// Clamp function, clamps a vector into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector3 Clamp(Vector3 x, float min, float max) =>
            new Vector3(Clamp(x.X, min, max), Clamp(x.Y, min, max), Clamp(x.Z, min, max));

        /// <summary>
        /// Clamp function, clamps a vector into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector4 Clamp(Vector4 x, Vector4 min, Vector4 max) =>
            new Vector4(Clamp(x.X, min.X, max.X), Clamp(x.Y, min.Y, max.Y), Clamp(x.Z, min.Z, max.Z), Clamp(x.W, min.W, max.W));

        /// <summary>
        /// Clamp function, clamps a vector into the range [min..max]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector4 Clamp(Vector4 x, float min, float max) =>
            new Vector4(Clamp(x.X, min, max), Clamp(x.Y, min, max), Clamp(x.Z, min, max), Clamp(x.W, min, max));

        #endregion

        #region Lerp

        /// <summary>
        /// Linear interpolation (blending) between two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[ or a if x = 0 or b if x = 1</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static double Lerp(double a, double b, double x) => a + x * (b - a);

        /// <summary>
        /// Linear interpolation (blending) between two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[ or a if x = 0 or b if x = 1</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static float Lerp(float a, float b, float x) => a + x * (b - a);

        /// <summary>
        /// Linear interpolation (blending) between two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[ or a if x = 0 or b if x = 1</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 x) => a + x * (b - a);

        /// <summary>
        /// Linear interpolation (blending) between two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[ or a if x = 0 or b if x = 1</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector3 Lerp(Vector3 a, Vector3 b, Vector3 x) => a + x * (b - a);

        /// <summary>
        /// Linear interpolation (blending) between two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns>Linear interpolation between a and b if x in the range ]0..1[ or a if x = 0 or b if x = 1</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector4 Lerp(Vector4 a, Vector4 b, Vector4 x) => a + x * (b - a);

        #endregion

        #region Map

        /// <summary>
        /// This Method can be seen as an inverse of Lerp (in Mode Float). Additionally it provides the infamous Mapping Modes, author: velcrome
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="start">Minimum of input value range</param>
        /// <param name="end">Maximum of input value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static double Ratio(double Input, double start, double end, MapMode mode)
        {
            if (end.CompareTo(start) == 0) return 0;

            double range = end - start;
            double ratio = (Input - start) / range;

            if (mode == MapMode.Float) { }
            else if (mode == MapMode.Clamp)
            {
                if (ratio < 0) ratio = 0;
                if (ratio > 1) ratio = 1;
            }
            else
            {
                if (mode == MapMode.Wrap)
                {
                    // includes fix for inconsistent behaviour of old delphi Map 
                    // node when handling integers
                    int rangeCount = (int)Floor(ratio);
                    ratio -= rangeCount;
                }
                else if (mode == MapMode.Mirror)
                {
                    // merke: if you mirror an input twice it is displaced twice the range. same as wrapping twice really
                    int rangeCount = (int)Floor(ratio);
                    rangeCount -= rangeCount & 1; // if uneven, make it even. bitmask of one is same as mod2
                    ratio -= rangeCount;

                    if (ratio > 1) ratio = 2 - ratio; // if on the max side of things now (due to rounding down rangeCount), mirror once against max
                }
            }
            return ratio;
        }

        /// <summary>
        /// This Method can be seen as an inverse of Lerp (in Mode Float). Additionally it provides Mapping Modes
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="start">Minimum of input value range</param>
        /// <param name="end">Maximum of input value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static float Ratio(float Input, float start, float end, MapMode mode)
        {
            if (end.CompareTo(start) == 0) return 0;

            float range = end - start;
            float ratio = (Input - start) / range;

            if (mode == MapMode.Float) { }
            else if (mode == MapMode.Clamp)
            {
                if (ratio< 0) ratio = 0;
                if (ratio > 1) ratio = 1;
            }
            else
            {
                if (mode == MapMode.Wrap)
                {
                    // includes fix for inconsistent behaviour of old delphi Map 
                    // node when handling integers
                    int rangeCount = (int)Floor(ratio);
                    ratio -= rangeCount;
                }
                else if (mode == MapMode.Mirror)
                {
                    // merke: if you mirror an input twice it is displaced twice the range. same as wrapping twice really
                    int rangeCount = (int)Floor(ratio);
                    rangeCount -= rangeCount & 1; // if uneven, make it even. bitmask of one is same as mod2
                    ratio -= rangeCount;

                    if (ratio > 1) ratio = 2 - ratio; // if on the max side of things now (due to rounding down rangeCount), mirror once against max
                }
            }
            return ratio;
        }

        /// <summary>
        /// This Method can be seen as an inverse of Lerp (in Mode Float). Additionally it provides Mapping Modes
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="start">Minimum of input value range</param>
        /// <param name="end">Maximum of input value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static Vector2 Ratio(Vector2 Input, Vector2 start, Vector2 end, MapMode mode) =>
            new Vector2(
                Ratio(Input.X, start.X, end.X, mode),
                Ratio(Input.Y, start.Y, end.Y, mode)
            );

        /// <summary>
        /// This Method can be seen as an inverse of Lerp (in Mode Float). Additionally it provides Mapping Modes
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="start">Minimum of input value range</param>
        /// <param name="end">Maximum of input value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static Vector3 Ratio(Vector3 Input, Vector3 start, Vector3 end, MapMode mode) =>
            new Vector3(
                Ratio(Input.X, start.X, end.X, mode),
                Ratio(Input.Y, start.Y, end.Y, mode),
                Ratio(Input.Z, start.Z, end.Z, mode)
            );

        /// <summary>
        /// This Method can be seen as an inverse of Lerp (in Mode Float). Additionally it provides Mapping Modes
        /// </summary>
        /// <param name="Input">Input value to convert</param>
        /// <param name="start">Minimum of input value range</param>
        /// <param name="end">Maximum of input value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        public static Vector4 Ratio(Vector4 Input, Vector4 start, Vector4 end, MapMode mode) =>
            new Vector4(
                Ratio(Input.X, start.X, end.X, mode),
                Ratio(Input.Y, start.Y, end.Y, mode),
                Ratio(Input.Z, start.Z, end.Z, mode),
                Ratio(Input.W, start.W, end.W, mode)
            );

        /// <summary>
        /// Linearly transform input between two ranges
        /// </summary>
        /// <param name="Input">Input value to transform</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static double Map(double Input, double InMin, double InMax, double OutMin, double OutMax, MapMode mode) =>
            Lerp(OutMin, OutMax, Ratio(Input, InMin, InMax, mode));

        /// <summary>
        /// Linearly transform input between two ranges
        /// </summary>
        /// <param name="Input">Input value to transform</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static float Map(float Input, float InMin, float InMax, float OutMin, float OutMax, MapMode mode) =>
            Lerp(OutMin, OutMax, Ratio(Input, InMin, InMax, mode));

        /// <summary>
        /// Linearly transform input between two ranges
        /// </summary>
        /// <param name="Input">Input value to transform</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector2 Map(Vector2 Input, Vector2 InMin, Vector2 InMax, Vector2 OutMin, Vector2 OutMax, MapMode mode) =>
            Lerp(OutMin, OutMax, Ratio(Input, InMin, InMax, mode));

        /// <summary>
        /// Linearly transform input between two ranges
        /// </summary>
        /// <param name="Input">Input value to transform</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector3 Map(Vector3 Input, Vector3 InMin, Vector3 InMax, Vector3 OutMin, Vector3 OutMax, MapMode mode) =>
            Lerp(OutMin, OutMax, Ratio(Input, InMin, InMax, mode));

        /// <summary>
        /// Linearly transform input between two ranges
        /// </summary>
        /// <param name="Input">Input value to transform</param>
        /// <param name="InMin">Minimum of input value range</param>
        /// <param name="InMax">Maximum of input value range</param>
        /// <param name="OutMin">Minimum of destination value range</param>
        /// <param name="OutMax">Maximum of destination value range</param>
        /// <param name="mode">Defines the behavior of the function if the input value exceeds the destination range 
        /// <see cref="MapMode">MapMode</see></param>
        /// <returns>Input value mapped from input range into destination range</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
        public static Vector4 Map(Vector4 Input, Vector4 InMin, Vector4 InMax, Vector4 OutMin, Vector4 OutMax, MapMode mode) =>
            Lerp(OutMin, OutMax, Ratio(Input, InMin, InMax, mode));

        #endregion

        #region Bilerp

        /// <summary>
        /// 2d linear interpolation in x and y direction for single values
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left value</param>
        /// <param name="P2">Upper right value</param>
        /// <param name="P3">Lower right value</param>
        /// <param name="P4">Lower left value</param>
        /// <returns>Interpolated value between the 4 values of the corners of a unit square</returns>
        public static float Bilerp(Vector2 Input, float P1, float P2, float P3, float P4) =>
            Lerp(Lerp(P1, P2, Input.X), Lerp(P3, P4, Input.X), Input.Y);

        /// <summary>
        /// 2d linear interpolation in x and y direction for single values
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left value</param>
        /// <param name="P2">Upper right value</param>
        /// <param name="P3">Lower right value</param>
        /// <param name="P4">Lower left value</param>
        /// <returns>Interpolated value between the 4 values of the corners of a unit square</returns>
        public static Vector2 Bilerp(Vector2 Input, Vector2 P1, Vector2 P2, Vector2 P3, Vector2 P4) =>
            Vector2.Lerp(Vector2.Lerp(P1, P2, Input.X), Vector2.Lerp(P3, P4, Input.X), Input.Y);

        /// <summary>
        /// 2d linear interpolation in x and y direction for single values
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left value</param>
        /// <param name="P2">Upper right value</param>
        /// <param name="P3">Lower right value</param>
        /// <param name="P4">Lower left value</param>
        /// <returns>Interpolated value between the 4 values of the corners of a unit square</returns>
        public static Vector3 Bilerp(Vector2 Input, Vector3 P1, Vector3 P2, Vector3 P3, Vector3 P4) =>
            Vector3.Lerp(Vector3.Lerp(P1, P2, Input.X), Vector3.Lerp(P3, P4, Input.X), Input.Y);

        /// <summary>
        /// 2d linear interpolation in x and y direction for single values
        /// </summary>
        /// <param name="Input">The position where to interpolate, 0..1</param>
        /// <param name="P1">Upper left value</param>
        /// <param name="P2">Upper right value</param>
        /// <param name="P3">Lower right value</param>
        /// <param name="P4">Lower left value</param>
        /// <returns>Interpolated value between the 4 values of the corners of a unit square</returns>
        public static Vector4 Bilerp(Vector2 Input, Vector4 P1, Vector4 P2, Vector4 P3, Vector4 P4) =>
            Vector4.Lerp(Vector4.Lerp(P1, P2, Input.X), Vector4.Lerp(P3, P4, Input.X), Input.Y);

        #endregion

        #region TriLerp

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for single values
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated value between the 8 values of the corners of a unit cube</returns>
        public static float Trilerp(Vector3 Input,
            float V010, float V110, float V100, float V000,
            float V011, float V111, float V101, float V001) =>
            Lerp(
                Bilerp(Input.xy(), V010, V110, V100, V000),
                Bilerp(Input.xy(), V011, V111, V101, V001),
                Input.Z
            );

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for single values
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated value between the 8 values of the corners of a unit cube</returns>
        public static Vector2 Trilerp(Vector3 Input,
            Vector2 V010, Vector2 V110, Vector2 V100, Vector2 V000,
            Vector2 V011, Vector2 V111, Vector2 V101, Vector2 V001) =>
            Vector2.Lerp(
                Bilerp(Input.xy(), V010, V110, V100, V000),
                Bilerp(Input.xy(), V011, V111, V101, V001),
                Input.Z
            );

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for single values
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated value between the 8 values of the corners of a unit cube</returns>
        public static Vector3 Trilerp(Vector3 Input,
            Vector3 V010, Vector3 V110, Vector3 V100, Vector3 V000,
            Vector3 V011, Vector3 V111, Vector3 V101, Vector3 V001) =>
            Vector3.Lerp(
                Bilerp(Input.xy(), V010, V110, V100, V000),
                Bilerp(Input.xy(), V011, V111, V101, V001),
                Input.Z
            );

        /// <summary>
        /// 3d linear interpolation in x, y and z direction for single values
        /// </summary>
        /// <param name="Input">The Interpolation factor, 3d-position inside the unit cube</param>
        /// <param name="V010">Front upper left</param>
        /// <param name="V110">Front upper right</param>
        /// <param name="V100">Front lower right</param>
        /// <param name="V000">Front lower left</param>
        /// <param name="V011">Back upper left</param>
        /// <param name="V111">Back upper right</param>
        /// <param name="V101">Back lower right</param>
        /// <param name="V001">Back lower left</param>
        /// <returns>Interpolated value between the 8 values of the corners of a unit cube</returns>
        public static Vector4 Trilerp(Vector3 Input,
            Vector4 V010, Vector4 V110, Vector4 V100, Vector4 V000,
            Vector4 V011, Vector4 V111, Vector4 V101, Vector4 V001) =>
            Vector4.Lerp(
                Bilerp(Input.xy(), V010, V110, V100, V000),
                Bilerp(Input.xy(), V011, V111, V101, V001),
                Input.Z
            );

        #endregion
    }
}
