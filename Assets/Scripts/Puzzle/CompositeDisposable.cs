using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class CompositeDisposable : IDisposable
    {
        readonly IList<IDisposable> _disposables;

        public CompositeDisposable()
        {
            _disposables = new List<IDisposable>();
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public IEnumerator<IDisposable> GetEnumerator()
        {
            var res = new List<IDisposable>();
            foreach (var d in _disposables)
            {
                if (d != null)
                {
                    res.Add(d);
                }
            }
            return res.GetEnumerator();
        }

        public void Dispose()
        {
            foreach (var i in _disposables)
            {
                i.Dispose();
            }
        }
    }
}
