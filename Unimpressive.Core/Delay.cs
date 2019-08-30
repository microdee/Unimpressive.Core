using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Unimpressive.Core
{
    /// <summary>
    /// Global delay type metadata base 
    /// </summary>
    public abstract class DelayTypeMeta
    {
        /// <summary>
        /// Type of the delayable value
        /// </summary>
        public Type ValueType { get; protected set; }
    }

    /// <summary>
    /// Global delay type metadata, providing default interpolation function and default value for a type
    /// </summary>
    public class DelayTypeMeta<T> : DelayTypeMeta
    {
        /// <summary>
        /// (old, new, lerp): result; Intraframe interpolation function for your value.
        /// </summary>
        public Func<T, T, float, T> Interpolator { get; set; }
        /// <summary>
        /// (original): clone; Copy function if your type is reference type. If null simple assignment is used.
        /// </summary>
        public Func<T, T> Copier { get; set; }
        /// <summary>
        /// Default of your value. Used if there's no data available yet.
        /// </summary>
        public T Default { get; set; }

        /// <summary></summary>
        public DelayTypeMeta()
        {
            ValueType = typeof(T);
        }
    }

    public class TypeNotFoundInStaticTableException : Exception
    {
        public override string Message { get; } =
            "Type is not found in the static DelayUtils.TypeMeta dictionary. Please use constructor with interpolation function and default specifiers.";
    }

    /// <summary>
    /// Simple to use class for delaying values by a certain TimeSpan and interpolating between value submissions
    /// </summary>
    /// <typeparam name="T">Type of </typeparam>
    public class Delay<T>
    {
        /// <summary>
        /// Reference stopwatch driver
        /// </summary>
        public Stopwatch Timer { get; } = new Stopwatch();

        /// <summary>
        /// (old, new, lerp): result; Intraframe interpolation function for your value. This might be automatically filled if using a common value type and the type of T is present in DelayUtils.TypeMeta
        /// </summary>
        public Func<T, T, float, T> Interpolator { get; set; }

        /// <summary>
        /// (original): clone; Copy function if your type is reference type. If null simple assignment is used.
        /// </summary>
        public Func<T, T> Copier { get; set; }

        /// <summary>
        /// Default of your value. Used if there's no data available yet. This might be automatically filled if using a common value type and the type of T is present in DelayUtils.TypeMeta
        /// </summary>
        public T Default { get; set; }

        /// <summary>
        /// List of all the samples and when they were added (in TotalMilliSeconds)
        /// </summary>
        public List<(double Key, T Frame)> Samples { get; private set; } = new List<(double Key, T Frame)>();

        /// <summary>
        /// Delete the oldest elements which are older than this amount of time
        /// </summary>
        public TimeSpan Capacity { get; set; }

        public int Iterations { get; private set; }

        /// <summary>
        /// If your type is present in DelayUtils.TypeMeta already use this constructor. (most common value types and System.Numerics vectors/matrix4x4 are present by default)
        /// </summary>
        /// <param name="capacity">Initial time capacity of this delay</param>
        public Delay(TimeSpan capacity)
        {
            var t = typeof(T);
            if (DelayUtils.TypeMeta.ContainsKey(t))
            {
                if (DelayUtils.TypeMeta[t] is DelayTypeMeta<T> tf)
                {
                    Interpolator = tf.Interpolator;
                    Default = tf.Default;
                    Copier = tf.Copier;
                    Capacity = capacity;
                    Timer.Start();
                }
                else
                {
                    throw new TypeNotFoundInStaticTableException();
                }
            }
            else
            {
                throw new TypeNotFoundInStaticTableException();
            }
        }

        /// <summary>
        /// Manually set required functions for your type.
        /// </summary>
        /// <param name="capacity">Initial time capacity of this delay</param>
        /// <param name="interp">(old, new, lerp): result; Intraframe interpolation function for your value</param>
        /// <param name="def">Default of your value. Will be used if there's no data available yet.</param>
        /// <param name="copier">(original): clone; Copy function if your type is reference type. If null simple assignment will be used.</param>
        public Delay(TimeSpan capacity, Func<T, T, float, T> interp, T def, Func<T, T> copier = null)
        {
            Capacity = capacity;
            Interpolator = interp;
            Copier = copier;
            Default = def;
            Timer.Start();
        }

        /// <summary>
        /// Call this with your value and also get back a delayed value immediately. Check if real data is available.
        /// </summary>
        /// <param name="val">The new value</param>
        /// <param name="delaytime">Get a delayed value at this age</param>
        /// <param name="unavailable">True if there's no data available yet at the specified age</param>
        /// <returns>The delayed value</returns>
        public T Update(T val, TimeSpan delaytime, out bool unavailable)
        {
            Submit(val);
            return GetAt(delaytime, out unavailable);
        }
        /// <summary>
        /// Call this with your value and also get back a delayed value immediately.
        /// </summary>
        /// <param name="val">The new value</param>
        /// <param name="delaytime">Get a delayed value at this age</param>
        /// <returns>The delayed value</returns>
        public T Update(T val, TimeSpan delaytime)
        {
            Submit(val);
            return GetAt(delaytime, out var dummy);
        }

        /// <summary>
        /// Get a delayed value at a specified age. Check if real data is available.
        /// </summary>
        /// <param name="delaytime">Get a delayed value at this age</param>
        /// <param name="unavailable">True if there's no data available yet at the specified age</param>
        /// <returns>The delayed value</returns>
        public T GetAt(TimeSpan delaytime, out bool unavailable)
        {
            var absdeltime = Timer.Elapsed.TotalMilliseconds - delaytime.TotalMilliseconds;
            T Interpolate((double Key, T Frame) older, (double Key, T Frame) newer)
            {
                var wd = newer.Key - older.Key;
                var nd = newer.Key - absdeltime;
                var p = (float)(nd / wd);
                return Interpolator(older.Frame, newer.Frame, 1 - p);
            };
            unavailable = true;
            if (Samples.Count == 0) return Default;
            unavailable = delaytime > Timer.Elapsed || absdeltime < Samples[0].Key;
            if (unavailable) return Samples[0].Frame;
            if (delaytime.TotalMilliseconds <= 0.0) return Samples.Last().Frame;
            if (Samples.Count == 1) return Samples[0].Frame;
            if (Samples.Count == 2) return Interpolate(Samples[0], Samples[1]);

            int i = (int)Math.Floor(Samples.Count / 2.0);
            int lo = 0;
            int hi = Samples.Count;
            Iterations = 0;
            while (true)
            {
                Iterations++;
                var cs = Samples[i];
                var ps = Samples[i - 1];
                if (absdeltime > ps.Key && absdeltime < cs.Key)
                {
                    return Interpolate(ps, cs);
                }
                if (absdeltime > cs.Key)
                {
                    lo = i;
                    i += (int)Math.Ceiling((hi - i) / 2.0);
                }

                if (absdeltime < ps.Key)
                {
                    hi = i;
                    i -= (int)Math.Ceiling((i - lo) / 2.0);
                }
            }

            //Iterations = 0;
            //for (int i = Samples.Count - 1; i > 0; i--)
            //{
            //    Iterations++;
            //    var cs = Samples[i];
            //    var ps = Samples[i - 1];
            //    if (absdeltime > ps.Key && absdeltime < cs.Key)
            //    {
            //        return Interpolate(ps, cs);
            //    }
            //}
            //return Samples.Last().Frame;
        }
        /// <summary>
        /// Get a delayed value at a specified age.
        /// </summary>
        /// <param name="delaytime">Get a delayed value at this age</param>
        /// <returns>The delayed value</returns>
        public T GetAt(TimeSpan delaytime)
        {
            return GetAt(delaytime, out var dummy);
        }

        /// <summary>
        /// Submit a new value and progress the samples.
        /// </summary>
        /// <param name="val">The new value</param>
        public void Submit(T val)
        {
            if (Samples.Count > 0)
            {
                while (true)
                {
                    var ct = Samples[0].Key;
                    if (Timer.Elapsed.TotalMilliseconds - ct < Capacity.TotalMilliseconds) break;
                    Samples.RemoveAt(0);
                    if (Samples.Count == 0) break;
                }
            }
            var actual = Copier == null ? val : Copier(val);
            Samples.Add((Timer.Elapsed.TotalMilliseconds, actual));
        }
    }

    /// <summary>
    /// Global utils for Delays
    /// </summary>
    public static class DelayUtils
    {
        /// <summary>
        /// A table of common types easing the construction of Delays
        /// </summary>
        public static Dictionary<Type, DelayTypeMeta> TypeMeta = new Dictionary<Type, DelayTypeMeta>();

        /// <summary>
        /// Add your own type to the common Delay type table
        /// </summary>
        /// <typeparam name="T">Type of your value</typeparam>
        /// <param name="interp">(old, new, lerp): result; Intraframe interpolation function for your value.</param>
        /// <param name="def">Default of your value. Used if there's no data available yet.</param>
        /// <param name="copier">(original): clone; Copy function if your type is reference type. If null simple assignment is used.</param>
        /// <returns>The newly created type metadata</returns>
        public static DelayTypeMeta AddMeta<T>(Func<T, T, float, T> interp, T def, Func<T, T> copier = null)
        {
            var t = typeof(T);
            if (!TypeMeta.ContainsKey(t))
            {
                var res = new DelayTypeMeta<T>
                {
                    Interpolator = interp,
                    Copier = copier,
                    Default = def
                };
                TypeMeta.Add(t, res);
                return res;
            }
            return null;
        }

        static DelayUtils()
        {
            AddMeta<float>((a, b, p) => a * (1 - p) + b * p, 0.0f);
            AddMeta<double>((a, b, p) => a * (1 - p) + b * p, 0.0);
            AddMeta<decimal>((a, b, p) => a * (1 - (decimal)p) + b * (decimal)p, decimal.Zero);
            AddMeta<bool>((a, b, p) => p > 0.5f ? b : a, false);
            AddMeta<int>((a, b, p) => p > 0.5f ? b : a, 0);
            AddMeta<uint>((a, b, p) => p > 0.5f ? b : a, 0);
            AddMeta<long>((a, b, p) => p > 0.5f ? b : a, 0);
            AddMeta<ulong>((a, b, p) => p > 0.5f ? b : a, 0);

            AddMeta<Vector2>(Vector2.Lerp, Vector2.Zero);
            AddMeta<Vector3>(Vector3.Lerp, Vector3.Zero);
            AddMeta<Vector4>(Vector4.Lerp, Vector4.Zero);
            AddMeta<Quaternion>(Quaternion.Slerp, Quaternion.Identity);
            AddMeta<Matrix4x4>(Matrix4x4.Lerp, Matrix4x4.Identity);
        }
    }
}

