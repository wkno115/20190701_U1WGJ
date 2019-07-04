using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyke
{
    public class AbstractUnityController : AbstractController
    {
        float _gameSpeed = 1f;
        List<IEnumerator> _parallelProcess = new List<IEnumerator>();

        protected IEnumerator _nextRunProcess;

        public override IEnumerator Run()
        {
            _shouldContinue = true;
            _shouldPause = false;
            _parallelProcess.Clear();

            yield return _onControllerInitialize();

            while (_shouldContinue)
            {
                while (_shouldPause)
                {
                    yield return null;
                }

                if (_nextRunProcess != null)
                {
                    yield return _nextRunProcess;
                    _nextRunProcess = null;
                }

                for (int i = 0; i < _parallelProcess.Count; i++)
                {
                    if (!_parallelProcess[i].MoveNext())
                    {
                        _parallelProcess.RemoveAt(i);
                    }
                }

                yield return _onControllerUpdate(Time.deltaTime * _gameSpeed);
            }

            yield return _onControllerFinalize();

            foreach (var eventDispose in _eventDisposables)
            {
                eventDispose.Dispose();
            }
        }

        protected void AddParallelProcess(IEnumerator process)
        {
            _parallelProcess.Add(process);
        }

        protected override void _pause(bool isOn)
        {
            base._pause(isOn);
        }
    }

    public class AbstractUnityController<TResult> : AbstractController<TResult>
        where TResult : class
    {
        float _gameSpeed = 1f;
        List<IEnumerator> _parallelProcess = new List<IEnumerator>();

        protected IEnumerator<TResult> _nextRunProcess;

        public override IEnumerable<TResult> Run()
        {
            _shouldContinue = true;
            _shouldPause = false;
            _parallelProcess.Clear();

            foreach (var item in _onControllerInitialize())
            {
                yield return item;
            }

            while (_shouldContinue)
            {
                while (_shouldPause)
                {
                    yield return null;
                }

                if (_nextRunProcess != null)
                {
                    yield return (TResult)_nextRunProcess;
                    _nextRunProcess = null;
                }

                for (int i = 0; i < _parallelProcess.Count; i++)
                {
                    if (!_parallelProcess[i].MoveNext())
                    {
                        _parallelProcess.RemoveAt(i);
                    }
                }

                foreach (var item in _onControllerUpdate(Time.deltaTime * _gameSpeed))
                {
                    yield return item;
                }
            }

            foreach (var item in _onControllerFinalize())
            {
                yield return item;
            }

            foreach (var eventDispose in _eventDisposables)
            {
                eventDispose.Dispose();
            }
        }

        protected void AddParallelProcess(IEnumerator process)
        {
            _parallelProcess.Add(process);
        }

        protected override void _pause(bool isOn)
        {
            base._pause(isOn);
        }
    }
}

