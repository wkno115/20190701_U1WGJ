using Pyke;
using System;
using UnityEngine;

namespace Puzzle.View
{
    /// <summary>
    /// 何も考えずに作った入力検知用コンポーネント
    /// </summary>
    public class InputHandlerComponent : MonoBehaviour
    {
        readonly EventPublisher<KeyCode> _inputKeyEventPublisher = new EventPublisher<KeyCode>();

        public bool IsActive = true;

        public IDisposable SubscribeInputKey(Action<KeyCode> action) => _inputKeyEventPublisher.Subscribe(action);

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) && IsActive)
            {
                _inputKeyEventPublisher.Publish(KeyCode.W);
            }
        }
    }
}
