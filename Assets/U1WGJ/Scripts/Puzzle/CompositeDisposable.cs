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

        public void Dispose()
        {
            foreach (var i in _disposables)
            {
                i.Dispose();
            }
        }
    }
}
