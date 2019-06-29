using Pyke;
using System;
using Tower.Monster;
using UnityEngine;

namespace Tower.Lane
{
    [RequireComponent(typeof(CollisionHandleComponent))]
    [RequireComponent(typeof(BoxCollider))]
    public class DeadLineView : AbstractView
    {
        CollisionHandleComponent _collisionHandleComponent;

        protected override void Awake()
        {
            base.Awake();

            _collisionHandleComponent = GetComponent<CollisionHandleComponent>();

            GetComponent<BoxCollider>().isTrigger = true;
        }

        public IDisposable SubscribeMonsterViewEnter(Action<MonsterView> action)
        {
            return _collisionHandleComponent.SubscribeComponentTriggerEnter(action);
        }
    }
}
