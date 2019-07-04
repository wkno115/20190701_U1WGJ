using Puzzle;
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
        [SerializeField]
        GameObject _meshMaterialObject;

        EventPublisher<MonsterView> _deadEventPublisher = new EventPublisher<MonsterView>();

        public MonsterType MonsterType => _monsterState.MonsterType;
        public PieceColor PieceColor => _monsterState.PieceColor;

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

        public void ChangeHp(float diff)
        {
            _monsterState.Hp += diff;
            if (_monsterState.Hp <= 0)
            {
                _deadEventPublisher.Publish(this);
                Destroy(gameObject);
            }
        }

        public void SetColor(PieceColor pieceColor)
        {
            _monsterState.PieceColor = pieceColor;
            _meshMaterialObject.GetComponent<Renderer>().material.color = pieceColor.ToColor();
        }

        public IDisposable SubscribeDead(Action<MonsterView> action) => _deadEventPublisher.Subscribe(action);
        public IDisposable SubscribeTriggerEnter(Action<Collider> action) => _collisionHandleComponent.SubscribeTriggerEnter(action);
        public IDisposable SubscribeDeadLineEnter(Action<DeadLineView> action) => _collisionHandleComponent.SubscribeComponentTriggerEnter(action);
    }
}
