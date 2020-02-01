using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Unimpressive.Core
{
    /// <summary>
    /// Indicates this type can be interpolated between 2 of each other
    /// </summary>
    /// <typeparam name="T">Actual type inheriting this interface</typeparam>
    public interface IBlendable<T>
    {
        /// <summary>
        /// Intraframe interpolation function for your value.
        /// </summary>
        /// <param name="a">Earlier</param>
        /// <param name="b">Later</param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        T Interpolate(T a, T b, float alpha);

        /// <summary>
        /// Copy function if your type is reference type. If null simple assignment is used.
        /// </summary>
        Func<T, T> Copier { get; }

        /// <summary>
        /// Default of your value. Used when invalid time is requested.
        /// </summary>
        T Default { get; }
    }

    /// <summary>
    /// Global tape type metadata base 
    /// </summary>
    public abstract class TapeTypeMeta
    {
        /// <summary>
        /// Type of the blendable value
        /// </summary>
        public Type ValueType { get; protected set; }
    }

    /// <summary>
    /// Global delay type metadata, providing default interpolation function and default value for a type
    /// </summary>
    public class TapeTypeMeta<T> : TapeTypeMeta
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
        public TapeTypeMeta()
        {
            ValueType = typeof(T);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TypeNotFoundInStaticTableException : Exception
    {
        public override string Message { get; } =
            "Type is not found in the static TapeUtils.TypeMeta dictionary and it's not IBlendable<T>. Please use constructor with interpolation function and default specifiers.";
    }

    /// <summary>
    /// Global utils for Tapes
    /// </summary>
    public static class TapeUtils
    {
        /// <summary>
        /// A table of common types easing the construction of Tapes
        /// </summary>
        public static Dictionary<Type, TapeTypeMeta> TypeMeta = new Dictionary<Type, TapeTypeMeta>();

        /// <summary>
        /// Add your own type to the common Tape type table
        /// </summary>
        /// <typeparam name="T">Type of your value</typeparam>
        /// <param name="interp">(old, new, lerp): result; Intraframe interpolation function for your value.</param>
        /// <param name="def">Default of your value. Used if there's no data available yet.</param>
        /// <param name="copier">(original): clone; Copy function if your type is reference type. If null simple assignment is used.</param>
        /// <returns>The newly created type metadata</returns>
        public static TapeTypeMeta AddMeta<T>(Func<T, T, float, T> interp, T def, Func<T, T> copier = null)
        {
            var t = typeof(T);
            if (!TypeMeta.ContainsKey(t))
            {
                var res = new TapeTypeMeta<T>
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

        static TapeUtils()
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


    public class Tape<T>
    {
        private (bool valid, T val)[] _samples;
        private bool _isBlendable;

        /// <summary>
        /// Maximum time this tape can hold
        /// </summary>
        public double MaxTime { get; }

        /// <summary>
        /// Amount of samples allocated for 1.0 unit of time
        /// </summary>
        public int Resolution { get; }

        /// <summary>
        /// Has data ever written to this tape?
        /// </summary>
        public bool Empty { get; private set; }

        /// <summary>
        /// True if only one entry is present on the Tape and the index of it in the samples backbuffer
        /// </summary>
        public (bool onlyOne, int index) OnlyOneEntry { get; private set; }

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
        /// If your type is present in TapeUtils.TypeMeta already use this constructor. (most common value types and System.Numerics vectors/matrix4x4 are present by default)
        /// </summary>
        /// <param name="maxTime"></param>
        /// <param name="resolution"></param>
        public Tape(double maxTime, int resolution)
        {
            var t = typeof(T);
            Empty = true;
            MaxTime = maxTime;
            Resolution = resolution;
            _samples = new (bool, T)[(int)(maxTime * resolution) + 1];

            if (TapeUtils.TypeMeta.ContainsKey(t))
            {
                if (TapeUtils.TypeMeta[t] is TapeTypeMeta<T> tf)
                {
                    Interpolator = tf.Interpolator;
                    Default = tf.Default;
                    Copier = tf.Copier;
                }
                else
                {
                    throw new TypeNotFoundInStaticTableException();
                }
            }
            else if (t.Is(typeof(IBlendable<T>))) _isBlendable = true;
        }

        /// <summary>
        /// Manually set required functions for your type.
        /// </summary>
        /// <param name="maxTime"></param>
        /// <param name="resolution"></param>
        /// <param name="interp">(old, new, lerp): result; Intraframe interpolation function for your value</param>
        /// <param name="def">Default of your value. Will be used if there's no data available yet.</param>
        /// <param name="copier">(original): clone; Copy function if your type is reference type. If null simple assignment will be used.</param>
        public Tape(double maxTime, int resolution, Func<T, T, float, T> interp, T def, Func<T, T> copier = null)
        {
            Empty = true;
            MaxTime = maxTime;
            Resolution = resolution;
            _samples = new (bool, T)[(int)(maxTime * resolution) + 1];
            Interpolator = interp;
            Copier = copier;
            Default = def;
        }

        /// <summary>
        /// <para>Get or set a value at specified time.</para>
        /// </summary>
        /// <param name="time"></param>
        /// <returns>
        /// <para>Default value when empty, the only value when the tape has only
        /// one valid value and in normal case the supposed value at the given time</para>
        /// </returns>
        /// <remarks>
        /// <para>Setting a value will happen on the closest sample index.
        /// This might imply that (tape[X] = val) != tape[X] if X is not exactly
        /// pointing to a sample index. The margin of error depends on the resolution.</para>
        /// <para>Returned value might be an interpolation between the closest earlier
        /// and the closest later value using the provided Interpolator function.
        /// The Getter also has a side effect on the private samples backbuffer, because
        /// it caches calculated values if the desired time has invalid neighboring
        /// samples. This is done so because in that case a search loop is required
        /// to find the closest valid samples and it would be wasteful to do that
        /// search every time when the same time is requested</para>
        /// </remarks>
        public T this[double time]
        {
            get
            {
                if (Empty) return Default;
                if (OnlyOneEntry.onlyOne) return _samples[OnlyOneEntry.index].val;

                var abstime = UnMath.Clamp(time * Resolution, 0, _samples.Length - 1);
                int roundt = (int)Math.Round(abstime);
                int floort = (int)Math.Floor(abstime);
                int ceilt = (int)Math.Ceiling(abstime);

                var (flvalid, flval) = _samples[floort];

                if (floort == ceilt && flvalid)
                {
                    return flval;
                }

                var (clvalid, clval) = _samples[ceilt];
                var writeval = false;
                var flinvalid = false;
                var clinvalid = false;

                while (!flvalid || !clvalid)
                {
                    writeval = true;
                    if (!flvalid)
                    {
                        floort--;
                        (flvalid, flval) = _samples[floort];
                        if (!flvalid && floort == 0)
                        {
                            flvalid = true;
                            flinvalid = true;
                        }
                    }
                    if (!clvalid)
                    {
                        ceilt++;
                        (clvalid, clval) = _samples[ceilt];
                        if (!clvalid && ceilt == _samples.Length - 1)
                        {
                            clvalid = true;
                            clinvalid = true;
                        }
                    }
                }

                if (clinvalid && flinvalid)
                {
                    Empty = true;
                    return Default;
                }

                if (flinvalid)
                {
                    _samples[roundt] = (true, clval);
                    return clval;
                }
                if (clinvalid)
                {
                    _samples[roundt] = (true, flval);
                    return flval;
                }

                var blend = UnMath.Map(abstime, floort, ceilt, 0, 1, UnMath.MapMode.Clamp);
                var res = Interpolator == null ?
                    (blend > 0.5 ? clval : flval) :
                    Interpolator(flval, clval, (float) blend);

                if(writeval)
                    _samples[roundt] = (true, res);

                return res;
            }
            set
            {
                if (_isBlendable && Interpolator == null)
                {
                    if (value is IBlendable<T> blendval)
                    {
                        Interpolator = blendval.Interpolate;
                        Copier = blendval.Copier;
                        Default = blendval.Default;
                    }
                }
                int roundt = (int)Math.Round(time * Resolution);
                if (roundt == _samples.Length) roundt--;
                if(roundt < 0 || roundt >= _samples.Length) return;

                _samples[roundt] = (true, Copier == null ? value : Copier(value));


                if (OnlyOneEntry.onlyOne && OnlyOneEntry.index != roundt)
                {
                    OnlyOneEntry = (false, OnlyOneEntry.index);
                }
                if (Empty) OnlyOneEntry = (true, roundt);
                Empty = false;
            }
        }

        /// <summary>
        /// Clears the tape to its default state again
        /// </summary>
        /// <param name="noGc">If true it will clear samples with a for loop
        /// instead of allocating a new array (and in turn GC the old one)</param>
        public void Clear(bool noGc = false)
        {
            if (noGc)
            {
                for (int i = 0; i < _samples.Length; i++)
                {
                    _samples[i] = (false, default);
                }
            }
            else
            {
                _samples = new (bool, T)[(int)(MaxTime * Resolution) + 1];
            }

            Empty = true;
            OnlyOneEntry = (false, 0);
        }
    }
}
