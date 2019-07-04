using System;
using UnityEngine;

namespace Pyke
{
    public class AbstractView : MonoBehaviour, IUnityView
    {
        protected PointerEventPublisher _pointerEventPublisher;

        public bool IsAccessable => gameObject != null;
        public Vector3 Position => transform.position;

        protected virtual void Awake()
        {
            var pointerEventPublisher = GetComponentInChildren<PointerEventPublisher>();
            _pointerEventPublisher = pointerEventPublisher ?? gameObject.AddComponent<PointerEventPublisher>();
        }
        protected virtual void OnDestroy()
        {

        }

        public virtual void Warp(Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        public virtual void Dead()
        {
            Destroy(gameObject);
        }

        public IDisposable SubscribeLeftClick(Action<IUnityView> action)
        {
            return _pointerEventPublisher.SubscribeLeftClick(() => action(this));
        }
        public IDisposable SubscribeRightClick(Action<IUnityView> action)
        {
            return _pointerEventPublisher.SubscribeRightClick(() => action(this));
        }
        public IDisposable SubscribeMiddleClick(Action<IUnityView> action)
        {
            return _pointerEventPublisher.SubscribeMiddleClick(() => action(this));
        }
        public IDisposable SubscribeCursorEnter(Action<IUnityView> action)
        {
            return _pointerEventPublisher.SubscribeEnter(() => action(this));
        }
        public IDisposable SubscribeCursorExit(Action<IUnityView> action)
        {
            return _pointerEventPublisher.SubscribeExit(() => action(this));
        }
    }

}
