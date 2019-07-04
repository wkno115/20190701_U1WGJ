using System;
using System.Collections.Generic;

namespace Pyke
{
    public class EventPublisher : IDisposable
    {
        event Action _event;

        List<Action> _subscribedEvent = new List<Action>();

        public void Publish()
        {
            _event?.Invoke();
        }

        public IDisposable Subscribe(Action action)
        {
            _event += action;
            _subscribedEvent.Add(action);
            return this;
        }

        public void Dispose()
        {
            foreach (var i in _subscribedEvent)
            {
                _event -= i;
            }
            _subscribedEvent.Clear();
        }
    }

    public class EventPublisher<T1> : IDisposable
    {
        event Action<T1> _event;

        List<Action<T1>> _subscribedEvent = new List<Action<T1>>();

        public void Publish(T1 t1)
        {
            _event?.Invoke(t1);
        }

        public IDisposable Subscribe(Action<T1> action)
        {
            _event += action;
            _subscribedEvent.Add(action);
            return this;
        }

        public void Dispose()
        {
            foreach (var i in _subscribedEvent)
            {
                _event -= i;
            }
            _subscribedEvent.Clear();
        }
    }

    public class EventPublisher<T1, T2> : IDisposable
    {
        event Action<T1, T2> _event;

        List<Action<T1, T2>> _subscribedEvent = new List<Action<T1, T2>>();

        public void Publish(T1 t1, T2 t2)
        {
            _event?.Invoke(t1, t2);
        }

        public IDisposable Subscribe(Action<T1, T2> action)
        {
            _event += action;
            _subscribedEvent.Add(action);
            return this;
        }

        public void Dispose()
        {
            foreach (var i in _subscribedEvent)
            {
                _event -= i;
            }
            _subscribedEvent.Clear();
        }
    }

    public class EventPublisher<T1, T2, T3> : IDisposable
    {
        event Action<T1, T2, T3> _event;

        List<Action<T1, T2, T3>> _subscribedEvent = new List<Action<T1, T2, T3>>();

        public void Publish(T1 t1, T2 t2, T3 t3)
        {
            _event?.Invoke(t1, t2, t3);
        }

        public IDisposable Subscribe(Action<T1, T2, T3> action)
        {
            _event += action;
            _subscribedEvent.Add(action);
            return this;
        }

        public void Dispose()
        {
            foreach (var i in _subscribedEvent)
            {
                _event -= i;
            }
            _subscribedEvent.Clear();
        }
    }
}
