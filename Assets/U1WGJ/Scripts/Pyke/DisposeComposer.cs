using System;
using System.Collections.Generic;

namespace Pyke
{
    public class DisposeComposer : IDisposable
    {
        IList<IDisposable> _disposables;

        public DisposeComposer(IList<IDisposable> disposables)
        {
            _disposables = disposables;
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
