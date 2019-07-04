using System;
using UnityEngine;

namespace Pyke
{
    public class CollisionHandleComponent : MonoBehaviour
    {
        [SerializeField]
        bool _shouldDebug;

        readonly EventPublisher<Collider> _triggerEnterEventProvider = new EventPublisher<Collider>();
        readonly EventPublisher<Collider> _triggerExitEventProvider = new EventPublisher<Collider>();
        readonly EventPublisher<Collider> _triggerStayEventProvider = new EventPublisher<Collider>();

        readonly EventPublisher<Collision> _collisionEnterEventProvider = new EventPublisher<Collision>();
        readonly EventPublisher<Collision> _collisionExitEventProvider = new EventPublisher<Collision>();
        readonly EventPublisher<Collision> _collisionStayEventProvider = new EventPublisher<Collision>();

        readonly EventPublisher<int> _overlapCollidersCountChangeEventProvider = new EventPublisher<int>();

        public ObservableCollection<Collider> OverlapColliders { get; } = new ObservableCollection<Collider>();
        public Collider Collider { get; private set; }

        void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        void Update()
        {
            if (_shouldDebug)
            {
                foreach (var collider in OverlapColliders)
                {
                    if (collider != null)
                    {
                        print(string.Format("{0} : {1} is contained", name, collider.name));

                    }
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (_shouldDebug)
            {
                print(other.name + "'s trigger enter");
            }

            OverlapColliders.Add(other);
            _triggerEnterEventProvider.Publish(other);
        }

        void OnTriggerExit(Collider other)
        {
            if (_shouldDebug)
            {
                print(other.name + "'s trigger exit");
            }

            OverlapColliders.Remove(other);
            _triggerExitEventProvider.Publish(other);
        }
        void OnTriggerStay(Collider other)
        {
            _triggerStayEventProvider.Publish(other);
        }
        void OnCollisionEnter(Collision collision)
        {
            if (_shouldDebug)
            {
                print(collision.collider.name + "'s collision enter");
            }

            _collisionEnterEventProvider.Publish(collision);
        }
        void OnCollisionExit(Collision collision)
        {
            if (_shouldDebug)
            {
                print(collision.collider.name + "'s collision exit");
            }

            _collisionExitEventProvider.Publish(collision);
        }
        void OnCollisionStay(Collision collision)
        {
            _collisionStayEventProvider.Publish(collision);
        }

        public IDisposable SubscribeTriggerEnter(Action<Collider> action)
        {
            return _triggerEnterEventProvider.Subscribe(action);
        }
        public IDisposable SubscribeTriggerExit(Action<Collider> action)
        {
            return _triggerExitEventProvider.Subscribe(action);
        }
        public IDisposable SubscribeTriggerStay(Action<Collider> action)
        {
            return _triggerStayEventProvider.Subscribe(action);
        }
        public IDisposable SubscribeComponentTriggerEnter<T>(Action<T> action)
        {
            return SubscribeTriggerEnter(collider =>
            {
                var tComponent = collider.GetComponent<T>();
                if (tComponent != null)
                {
                    action(tComponent);
                }
            });
        }
        public IDisposable SubscribeComponentTriggerExit<T>(Action<T> action)
        {
            return SubscribeTriggerExit(collider =>
            {
                var tComponent = collider.GetComponent<T>();
                if (tComponent != null)
                {
                    action(tComponent);
                }
            });
        }
        public IDisposable SubscribeComponentTriggerStay<T>(Action<T> action)
        {
            return SubscribeTriggerStay(collider =>
            {
                var tComponent = collider.GetComponent<T>();
                if (tComponent != null)
                {
                    action(tComponent);
                }
            });
        }

        public IDisposable SubscribeCollisionEnter(Action<Collision> action)
        {
            return _collisionEnterEventProvider.Subscribe(action);
        }
        public IDisposable SubscribeCollisionExit(Action<Collision> action)
        {
            return _collisionExitEventProvider.Subscribe(action);
        }
        public IDisposable SubscribeCollisionStay(Action<Collision> action)
        {
            return _collisionStayEventProvider.Subscribe(action);
        }
        public IDisposable SubscribeOverlapCollidersCountChange(Action<int, int> action)
        {
            return OverlapColliders.SubscribeCountChangeEvent(action);
        }

        public IDisposable SubscribeOverlapCollidersCountSomeToZero(Action action)
        {
            return SubscribeOverlapCollidersCountChange((oldCount, newCount) =>
            {
                if (oldCount != 0 && newCount == 0)
                {
                    action();
                }
            });
        }
        public IDisposable SubscribeOverlapCollidersCountZeroToSome(Action action)
        {
            return SubscribeOverlapCollidersCountChange((oldCount, newCount) =>
            {
                if (oldCount == 0 && newCount != 0)
                {
                    action();
                }
            });
        }
    }
}