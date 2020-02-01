using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimpressive.Core
{
    /// <summary>
    /// Record and playback values with a loop over short periods of time
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Sequencer<T>
    {
        private Tape<T> _backTape;

        private double _recordStartedAt;
        private double _playbackStartedAt;

        /// <summary>
        /// Duration of the currently recorded sequence
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Sequencer doesn't have any data yet
        /// </summary>
        public bool Empty => _backTape.Empty;

        /// <summary></summary>
        /// <param name="maxTime">The total time the backing tape can hold
        /// for this sequencer. Any recorded clips should be shorter than that</param>
        /// <param name="resolution">Underlying temporal resolution</param>
        public Sequencer(double maxTime, int resolution)
        {
            _backTape = new Tape<T>(maxTime, resolution);
        }

        /// <summary>
        /// Clears the sequencer to an empty state.
        /// </summary>
        public virtual void Reset()
        {
            Duration = 0.0;
            _backTape.Clear();
        }

        /// <summary>
        /// Record a value for any given time
        /// </summary>
        /// <param name="input">Value to be recorded</param>
        /// <param name="time">Absolute time</param>
        /// <param name="start">Indicate that this is a start of a clip</param>
        /// <param name="roundDuration">Round duration to the closest integer time</param>
        public void Record(T input, double time, bool start, bool roundDuration)
        {
            if (start)
            {
                _recordStartedAt = time;
                _playbackStartedAt = time;
                _backTape.Clear();
            }
            var actualTime = time - _recordStartedAt;
            if(actualTime > _backTape.MaxTime) return;
            Duration = roundDuration ? Math.Round(actualTime) : actualTime;

            _backTape[actualTime] = input;
        }

        /// <summary>
        /// Get a value for any given time with a phase
        /// </summary>
        /// <param name="time"></param>
        /// <param name="phase">Seek within sequence, normalized 0..1 relative to duration</param>
        /// <param name="start"></param>
        /// <param name="overlap">Overlaps the end and the beginning of a sequence.
        /// Sequence playback becomes slower while doing that</param>
        /// <param name="pingpong">Instead of wrapping loop, use ping-pong or mirrored loop </param>
        /// <returns></returns>
        public T Play(double time, double phase, bool start, double overlap = 0, bool pingpong = false)
        {
            if (start)
            {
                _playbackStartedAt = time;
            }

            var actualTime = time - _recordStartedAt + Duration * phase;
            var modTime = actualTime.Fmod(Duration);
            if (pingpong)
                modTime = UnMath.Map(
                    actualTime,
                    0, Duration,
                    0, Duration,
                    UnMath.MapMode.Mirror
                );

            if (overlap > 0 && !pingpong)
            {
                var absoverlap = overlap * Duration;
                var trimmedTime= UnMath.Map(
                    modTime,
                    0, Duration,
                    absoverlap,
                    Duration,
                    UnMath.MapMode.Float
                );
                var fadeInTime = UnMath.Map(
                    trimmedTime,
                    Duration - absoverlap,
                    Duration,
                    0,
                    absoverlap,
                    UnMath.MapMode.Clamp
                );

                var baseVal = _backTape[trimmedTime];
                var fadeVal = _backTape[fadeInTime];

                return _backTape.Interpolator(baseVal, fadeVal, (float)(fadeInTime / absoverlap));
            }
            return _backTape[modTime];
        }
    }

    /// <summary>
    /// Instead of using functions to record and play, this class holds the state also
    /// to determine recording and playing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StatefulSequencer<T> : Sequencer<T>
    {
        private bool _prevRecord;
        private bool _prevPlay;

        /// <inheritdoc cref="Sequencer{T}"/>
        public StatefulSequencer(double maxTime, int resolution) : base(maxTime, resolution)
        {
        }

        /// <inheritdoc cref="Sequencer{T}"/>
        public override void Reset()
        {
            base.Reset();
            Recording = _prevRecord = Playing = _prevPlay = false;
        }

        /// <summary></summary>
        public bool Recording;

        /// <summary></summary>
        public bool Playing;

        /// <summary>
        /// Instead of wrapping loop, use ping-pong or mirrored loop
        /// </summary>
        public bool PingPong;

        /// <summary>
        /// Round duration to the closest integer time
        /// </summary>
        public bool RoundDuration;

        /// <summary>
        /// Automatically start playing once recording finished
        /// </summary>
        public bool AutoStart;

        /// <summary>
        /// Seek within sequence, normalized 0..1 relative to duration
        /// </summary>
        public double Phase;

        /// <summary>
        /// Overlaps the end and the beginning of a sequence.
        /// Sequence playback becomes slower while doing that
        /// </summary>
        public double Overlap;

        /// <summary>
        /// Call this function every frame with the target value and the current absolute time
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns>The sequence animation while playing, the input value otherwise</returns>
        public T Update(T input, double time)
        {
            if (Recording)
            {
                Playing = false;
                Record(input, time, !_prevRecord, RoundDuration);
            }
            else if (_prevRecord && AutoStart)
                Playing = true;

            _prevRecord = Recording;

            var res = input;

            if (Playing)
            {
                res = Play(time, Phase, !_prevPlay, Overlap, PingPong);
            }

            _prevPlay = Playing;


            return res;
        }
    }
}
