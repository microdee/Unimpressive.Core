using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Unimpressive.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Generic unsubscriber for Observables
    /// </summary>
    /// <typeparam name="TObservable">Type of the observable</typeparam>
    /// <typeparam name="TArg">Argument type of the Observable</typeparam>
    public class Unsubscriber<TObservable, TArg> : IDisposable where TObservable : IObservable<TArg>
    {
        private readonly Action<TObservable, IObserver<TArg>> _removal;
        private readonly TObservable _observable;
        private readonly IObserver<TArg> _observer;

        /// <summary>
        /// Construct the Unsubscriber
        /// </summary>
        /// <param name="observer">The unsubscribing observer</param>
        /// <param name="observable">The observable which has the observer</param>
        /// <param name="removal">The method of subscription removal from the observable</param>
        public Unsubscriber(TObservable observable,
            IObserver<TArg> observer,
            Action<TObservable, IObserver<TArg>> removal)
        {
            _removal = removal;
            _observable = observable;
            _observer = observer;
        }

        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            _removal?.Invoke(_observable, _observer);
        }
    }

    /// <summary>
    /// Convenience abstract class for observable objects. An observer can only subscribe once
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public abstract class ObservableBase<TArgs> : IObservable<TArgs>
    {
        /// <summary>
        /// HashSet of currently subscribed observers
        /// </summary>
        protected readonly ConcurrentDictionary<IObserver<TArgs>, int> Observers = new ConcurrentDictionary<IObserver<TArgs>, int>();

        /// <summary>
        /// Call this to invoke OnNext
        /// </summary>
        /// <param name="val"></param>
        protected virtual void Next(TArgs val) => Observers.ForeachConcurrent((o, v) => o.OnNext(val));

        /// <summary>
        /// Call this to invoke OnCompleted
        /// </summary>
        protected virtual void Completed() => Observers.ForeachConcurrent((o, v) => o.OnCompleted());

        /// <summary>
        /// Call this to invoke OnError
        /// </summary>
        /// <param name="e"></param>
        protected virtual void Error(Exception e) => Observers.ForeachConcurrent((o, v) => o.OnError(e));

        /// <summary>
        /// Subscribe to this Observable. Override this to do things before or after subscription.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public virtual IDisposable Subscribe(IObserver<TArgs> observer)
        {
            Observers.AddOrUpdate(observer, 0, (o, v) => 0);
            return new Unsubscriber<IObservable<TArgs>, TArgs>(
                this, observer, (obl, obr) => Observers.TryRemove(obr, out _));
        }
    }
}
