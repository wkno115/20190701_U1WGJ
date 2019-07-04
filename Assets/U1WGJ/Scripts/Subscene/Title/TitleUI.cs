using Pyke;
using System;
using UnityEngine;

namespace Title.View
{
    public class TitleUI : MonoBehaviour
    {
        [SerializeField]
        StandardButton _button;

        public IDisposable SubscribeTap(Action onTap) => _button.SubscribeLeftClick(_ => onTap());
    }
}
