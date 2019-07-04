using System;

namespace Pyke
{

    public abstract class AbstractPawnModel
    {

        readonly EventPublisher _stateChangeEvent = new EventPublisher();
        public IDisposable SubscribeChangeState(Action action)
        {
            return _stateChangeEvent.Subscribe(action);
        }

        readonly EventPublisher<float> _hpChangeEventPublisher = new EventPublisher<float>();
        readonly EventPublisher _deadEventPublisher = new EventPublisher();

        readonly uint _id;

        protected string _name = "";
        protected string _description = "";
        protected float _maxHp = 100;
        protected float _hp = 100;
        protected float _maxMana = 100;
        protected float _mana = 100;
        protected float _attackDamage = 100;
        protected float _attackSpeed = 2f;
        protected float _armor = 100;
        protected float _abilityPower = 100;
        protected float _magicRegist = 100;
        protected float _movementSpeed = 100;
        protected float _criticalRate = 100;
        protected bool _isAlive = true;
        protected bool _isAttackable = true;
        protected bool _isDamageable = true;

        public uint Id => _id;
        public string Name => _name;
        public float MaxHp => _maxHp;
        public float Hp => _hp;
        public float MaxMana => _maxMana;
        public float Mana => _mana;
        public float AttackDamage => _attackDamage;
        public float AttackSpeed => _attackSpeed;
        public float Armor => _armor;
        public float AbilityPower => _abilityPower;
        public float MagicRegist => _magicRegist;
        public float MovementSpeed => _movementSpeed;
        public float CriticalRate => _criticalRate;
        public bool IsAlive => _isAlive;
        public bool IsAttackable => _isAttackable;
        public bool IsDamageable => _isDamageable;

        public AbstractPawnModel(uint id)
        {
            _id = id;
        }

        public void SetIsAttackable(bool isAttackable)
        {
            _isAttackable = isAttackable;
            _stateChangeEvent.Publish();
        }

        public void ChangeHp(float diff)
        {
            _hp += diff;
            _hpChangeEventPublisher.Publish(diff);
            _stateChangeEvent.Publish();

            if (_hp <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            _isAlive = false;
            _deadEventPublisher.Publish();
            _stateChangeEvent.Publish();
        }

        public IDisposable SubscribeDamage(Action<float> action)
        {
            return _hpChangeEventPublisher.Subscribe(action);
        }
        public IDisposable SubscribeDead(Action action)
        {
            return _deadEventPublisher.Subscribe(action);
        }
    }

}