using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pyke
{
    public class PointerEventPublisher : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        EventPublisher _leftClickEventPublisher = new EventPublisher();
        EventPublisher _middleClickEventPublisher = new EventPublisher();
        EventPublisher _rightClickEventPublisher = new EventPublisher();
        EventPublisher _enterEventPublisher = new EventPublisher();
        EventPublisher _exitClickEventPublisher = new EventPublisher();

        void OnDestroy()
        {
            _leftClickEventPublisher.Dispose();
            _middleClickEventPublisher.Dispose();
            _rightClickEventPublisher.Dispose();
            _enterEventPublisher.Dispose();
            _exitClickEventPublisher.Dispose();
        }

        public IDisposable SubscribeLeftClick(Action action)
        {
            return _leftClickEventPublisher.Subscribe(action);
        }

        public IDisposable SubscribeMiddleClick(Action action)
        {
            return _middleClickEventPublisher.Subscribe(action);
        }

        public IDisposable SubscribeRightClick(Action action)
        {
            return _rightClickEventPublisher.Subscribe(action);
        }

        public IDisposable SubscribeEnter(Action action)
        {
            return _enterEventPublisher.Subscribe(action);
        }

        public IDisposable SubscribeExit(Action action)
        {
            return _exitClickEventPublisher.Subscribe(action);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _leftClickEventPublisher.Publish();
            }
            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                _middleClickEventPublisher.Publish();
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _rightClickEventPublisher.Publish();
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _enterEventPublisher.Publish();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _exitClickEventPublisher.Publish();
        }
    }
}


