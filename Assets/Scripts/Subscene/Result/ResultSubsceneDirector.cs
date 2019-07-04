using Play;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Result
{
    public class ResultSubsceneDirector : MonoBehaviour
    {


        public IEnumerable Run(PlayResult? result)
        {
            if (result.HasValue)
            {

            }
            else
            {

            }
            yield return null;
        }
    }
}
