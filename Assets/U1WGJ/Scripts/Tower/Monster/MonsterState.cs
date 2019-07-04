using Puzzle;
using System;
using UnityEngine;

namespace Tower.Monster
{
    [Serializable]
    public struct MonsterState
    {
        [SerializeField]
        MonsterType _monsterType;

        public MonsterType MonsterType => _monsterType;
        public PieceColor PieceColor;
        public float MovementSpeed;
        public float Hp;
        public float AttackDamage;
    }
}
