using System.Collections;
using System.Collections.Generic;

namespace Pyke
{

    public interface IController
    {
        IEnumerator Run();

        void Pause(bool isOn);
        void End();
    }

    public interface IController<TResult>
    {
        IEnumerable<TResult> Run();

        void Pause(bool isOn);
        void End();
    }

}