using Pyke;
using System.Collections;
using System.Collections.Generic;
using Title.View;
using UnityEngine;

namespace Title
{
    public class TitleSubsceneDirector : MonoBehaviour
    {
        [SerializeField]
        TitleUI _ui;

        public IEnumerable Run()
        {
            var shouldContinue = true;
            _ui.gameObject.SetActive(true);

            using (_ui.SubscribeTap(() => shouldContinue = false))
            {
                while (shouldContinue)
                {
                    yield return null;
                }
            }
            _ui.gameObject.SetActive(true);
        }
    }
}
