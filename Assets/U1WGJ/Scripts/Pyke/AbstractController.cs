using System;
using System.Collections;
using System.Collections.Generic;

namespace Pyke
{
    public abstract class AbstractController : IController
    {
        protected bool _shouldPause;
        protected bool _shouldContinue;
        protected List<IDisposable> _eventDisposables = new List<IDisposable>();

        public AbstractController()
        {
        }

        protected virtual IEnumerator _onControllerInitialize()
        {
            yield return null;
        }
        protected virtual IEnumerator _onControllerFinalize()
        {
            yield return null;
        }
        protected virtual IEnumerator _onControllerUpdate(float deltaTime)
        {
            yield return null;
        }

        protected void _subscribeEvent(IDisposable eventDisposable)
        {
            _eventDisposables.Add(eventDisposable);
        }

        public virtual IEnumerator Run()
        {
            _shouldContinue = true;
            _shouldPause = false;

            yield return _onControllerInitialize();

            while (_shouldContinue)
            {
                while (_shouldPause)
                {
                    yield return null;
                }
                _onControllerUpdate(0);
            }

            yield return _onControllerFinalize();

            foreach (var eventDispose in _eventDisposables)
            {
                eventDispose.Dispose();
            }
        }

        public void End()
        {
            Pause(false);
            _shouldContinue = false;
        }

        public void Pause(bool isOn)
        {
            _pause(isOn);
        }
        protected virtual void _pause(bool isOn)
        {
            _shouldPause = isOn;
        }
    }

    public abstract class AbstractController<TResult> : IController<TResult>
        where TResult : class
    {
        protected bool _shouldPause;
        protected bool _shouldContinue;
        protected List<IDisposable> _eventDisposables = new List<IDisposable>();

        public AbstractController()
        {
        }

        protected virtual IEnumerable<TResult> _onControllerInitialize()
        {
            yield return null;
        }
        protected virtual IEnumerable<TResult> _onControllerFinalize()
        {
            yield return null;
        }
        protected virtual IEnumerable<TResult> _onControllerUpdate(float deltaTime)
        {
            yield return null;
        }

        protected void _subscribeEvent(IDisposable eventDisposable)
        {
            _eventDisposables.Add(eventDisposable);
        }

        public virtual IEnumerable<TResult> Run()
        {
            _shouldContinue = true;
            _shouldPause = false;

            yield return (TResult)_onControllerInitialize();

            while (_shouldContinue)
            {
                while (_shouldPause)
                {
                    yield return null;
                }
                _onControllerUpdate(0);
            }

            yield return (TResult)_onControllerFinalize();

            foreach (var eventDispose in _eventDisposables)
            {
                eventDispose.Dispose();
            }
        }

        public void End()
        {
            Pause(false);
            _shouldContinue = false;
        }

        public void Pause(bool isOn)
        {
            _pause(isOn);
        }
        protected virtual void _pause(bool isOn)
        {
            _shouldPause = isOn;
        }
    }
}


