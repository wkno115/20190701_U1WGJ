using System;
using UnityEngine;

namespace Pyke
{
    public interface IUnityView
    {
        bool IsAccessable { get; }
        Vector3 Position { get; }

        void Warp(Vector3 position);
        void SetActive(bool isActive);
        void Dead();

        IDisposable SubscribeLeftClick(Action<IUnityView> action);
        IDisposable SubscribeRightClick(Action<IUnityView> action);
        IDisposable SubscribeMiddleClick(Action<IUnityView> action);
        IDisposable SubscribeCursorEnter(Action<IUnityView> action);
        IDisposable SubscribeCursorExit(Action<IUnityView> action);
    }

}