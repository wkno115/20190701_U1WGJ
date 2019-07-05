using System.Collections;
using System.Collections.Generic;
using Title.View;
using UnityEngine;

namespace Play.View
{
    public class PlayUI : MonoBehaviour
    {
        [SerializeField]
        StandardButton _finishButton;

        public IEnumerable Run()
        {
            var shouldContinue = true;
            using (_finishButton.SubscribeLeftClick(_ => shouldContinue = false))
            {
                while (shouldContinue)
                {
                    yield return null;
                }
            }
        }
    }
}
