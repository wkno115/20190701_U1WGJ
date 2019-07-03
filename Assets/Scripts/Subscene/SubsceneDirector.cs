using Play;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subscene
{
    public class SubsceneDirector : MonoBehaviour
    {
        [SerializeField]
        PlaySubsceneDirector _playSubsceneDirector;
        

        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
