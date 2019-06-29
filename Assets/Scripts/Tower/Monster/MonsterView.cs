using Pyke;
using System;
using Tower.Lane;
using UnityEngine;

namespace Tower.Monster
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CollisionHandleComponent))]
    [RequireComponent(typeof(BoxCollider))]
    public class MonsterView : AbstractView
    {
        [SerializeField]
        MonsterState _monsterState;

        [SerializeField]
        CollisionHandleComponent _collisionHandleComponent;

        protected override void Awake()
        {
            base.Awake();

            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;

            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void Move(Vector3 deltaDirection)
        {
            transform.position += deltaDirection * _monsterState.MovementSpeed;
        }

        public IDisposable SubscribeTriggerEnter(Action<Collider> action)
        {
            return _collisionHandleComponent.SubscribeTriggerEnter(action);
        }
        public IDisposable SubscribeDeadLineEnter(Action<DeadLineView> action)
        {
            return _collisionHandleComponent.SubscribeComponentTriggerEnter(action);
        }
    }
}
