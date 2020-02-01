using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Unimpressive.Core.Interfaces
{
    /// <summary>
    /// An interface generalizing per-frame/mainlooping behavior
    /// </summary>
    public interface IMainlooping
    {
        /// <summary>
        /// First thing should be invoked in the mainloop
        /// </summary>
        event EventHandler OnMainLoopBegin;

        /// <summary>
        /// Last thing should be invoked in the mainloop
        /// </summary>
        event EventHandler OnMainLoopEnd;

        /// <summary>
        /// Function to be called once every frame
        /// </summary>
        /// <param name="deltatime">Delta time between frames usually in seconds but can be ms (depending on the implementer)</param>
        void Mainloop(float deltatime);
    }

    /// <inheritdoc />
    /// <summary>
    /// Scheduler allowing to queue observable data on a mainloop
    /// </summary>
    /// <remarks>
    /// modified from https://github.com/vvvv/vvvv-sdk/blob/af11934c2e328865c24516f9dc3f98becd7536bc/vvvv45/src/nodes/plugins/System/FrameBasedScheduler.cs
    /// </remarks>
    public class MainloopScheduler : IScheduler
    {
        /// <summary>
        /// Stepping behavior of the scheduler
        /// </summary>
        public enum QueueMode
        {
            /// <summary>
            /// Step-by-step iteration
            /// </summary>
            Enqueue,
            /// <summary>
            /// Keep only the last event and discard the rest
            /// </summary>
            Discard
        }

        private readonly SchedulerQueue<uint> _queue = new SchedulerQueue<uint>(4);

        /// <inheritdoc cref="IScheduler"/>
        public DateTimeOffset Now
        {
            get;
            private set;
        }

        /// <inheritdoc cref="IScheduler"/>
        /// <summary>THIS IS NOT SUPPORTED</summary>
        public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc cref="IScheduler"/>
        /// <summary>THIS IS NOT SUPPORTED</summary>
        public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func<IScheduler, TState, IDisposable> action)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc cref="IScheduler"/>
        public IDisposable Schedule<TState>(TState state, Func<IScheduler, TState, IDisposable> action)
        {
            var scheduledItem = new ScheduledItem<uint, TState>(this, state, action, CurrentFrame);
            _queue.Enqueue(scheduledItem);
            return Disposable.Create(scheduledItem.Cancel);
        }

        /// <summary>
        /// Number of mainloop frames since creation
        /// </summary>
        public uint CurrentFrame { get; private set; }

        /// <summary>
        /// Number of items in the queue
        /// </summary>
        public int QueueSize => _queue.Count;

        /// <summary>
        /// Invoke this in your mainloop
        /// </summary>
        /// <param name="mode"><see cref="QueueMode"/></param>
        public void Mainloop(QueueMode mode = QueueMode.Enqueue)
        {
            var currentFrame = CurrentFrame;
            switch (mode)
            {
                case QueueMode.Enqueue:
                    CurrentFrame++;
                    Run(currentFrame);
                    break;
                case QueueMode.Discard:
                    Run(currentFrame);
                    CurrentFrame++;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void Run(uint frame)
        {
            while (_queue.Count > 0)
            {
                var nextWorkItem = _queue.Peek();
                if (nextWorkItem.DueTime <= frame)
                {
                    var workItem = _queue.Dequeue();
                    if (!workItem.IsCanceled)
                        workItem.Invoke();
                }
                else
                    break;
            }
        }
    }
}
