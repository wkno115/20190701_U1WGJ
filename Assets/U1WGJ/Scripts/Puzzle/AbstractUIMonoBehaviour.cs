using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class AbstractUIMonoBehaviour : MonoBehaviour
    {
        protected RectTransform _rectTransform;

        public Transform Transform => _rectTransform;
        public float GetWidth() => _rectTransform.rect.width;
        public float GetHeight() => _rectTransform.rect.height;

        void Awake()
        {
            _rectTransform = transform as RectTransform;
        }
    }
}
