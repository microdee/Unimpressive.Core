using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using md.stdl.Interfaces;

namespace Unimpressive.Core
{
    /// <inheritdoc cref="Stopwatch" />
    /// <inheritdoc cref="IMainlooping" />
    /// <summary>
    /// A Seekable version of Stopwatch with the capability to fire time based triggers synchronously
    /// </summary>
    public class StopwatchInteractive : Stopwatch, IMainlooping, IDisposable
    {
        private TimeSpan _timeOffset = TimeSpan.Zero;
        private Timer _timer;

        /// <inheritdoc cref="Stopwatch"/>
        public new TimeSpan Elapsed => base.Elapsed + _timeOffset;
        /// <inheritdoc cref="Stopwatch"/>
        public new long ElapsedMilliseconds => base.ElapsedMilliseconds + (long)_timeOffset.TotalMilliseconds;
        /// <inheritdoc cref="Stopwatch"/>
        public new long ElapsedTicks => base.ElapsedTicks + _timeOffset.Ticks;

        /// <summary>
        /// Set time immediately
        /// </summary>
        /// <param name="time"></param>
        public void SetTime(TimeSpan time)
        {
            _timeOffset = time;
            if (IsRunning)
            {
                Restart();
            }
            else
            {
                Reset();
            }
        }

        /// <summary>
        /// Set time immediately fluidly
        /// </summary>
        /// <param name="time"></param>
        public StopwatchInteractive WithTime(TimeSpan time)
        {
            SetTime(time);
            return this;
        }

        /// <summary>
        /// Initialize this interactive stopwach with a timer which fires the mainloop at given interval in ms
        /// </summary>
        /// <param name="interval">Timer period in ms</param>
        /// <returns></returns>
        public StopwatchInteractive WithInternalMainloop(double interval = 10.0)
        {
            _timer = new Timer(interval) { AutoReset = true };
            _timer.Elapsed += (sender, args) => Mainloop(0);
            _timer.Start();
            return this;
        }

        /// <inheritdoc cref="IMainlooping"/>
        public event EventHandler OnMainLoopBegin;
        /// <inheritdoc cref="IMainlooping"/>
        public event EventHandler OnMainLoopEnd;

        /// <summary>
        /// Fired when a trigger is passed
        /// </summary>
        public event EventHandler OnTriggerPassed;

        private readonly List<(TimeSpan time, bool passed)> _triggers = new List<(TimeSpan time, bool passed)>();

        /// <summary>
        /// Set a list of triggers when the OnTriggerPassed should be invoked
        /// </summary>
        /// <param name="triggers"></param>
        public void SetTrigger(params TimeSpan[] triggers)
        {
            _triggers.Clear();
            foreach (var time in triggers)
            {
                _triggers.Add((time, false));
            }
        }

        /// <summary>
        /// Fluidly set a list of triggers when the OnTriggerPassed should be invoked
        /// </summary>
        /// <param name="triggers"></param>
        public StopwatchInteractive WithTriggers(params TimeSpan[] triggers)
        {
            SetTrigger(triggers);
            return this;
        }

        /// <summary>
        /// Reset triggers to an untriggered state
        /// </summary>
        public void ResetTriggers()
        {
            for (int i = 0; i < _triggers.Count; i++)
            {
                var trig = _triggers[i];
                trig.passed = false;
                _triggers[i] = trig;
            }
        }

        /// <inheritdoc cref="IMainlooping"/>
        public void Mainloop(float deltatime)
        {
            OnMainLoopBegin?.Invoke(this, EventArgs.Empty);

            for (int i = 0; i < _triggers.Count; i++)
            {
                var trig = _triggers[i];

                if (Elapsed >= trig.time && !trig.passed)
                {
                    OnTriggerPassed?.Invoke(this, EventArgs.Empty);
                    trig.passed = true;
                }
                _triggers[i] = trig;
            }

            OnMainLoopEnd?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
