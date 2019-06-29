using System;
using UnityEngine;

namespace Tower.Monster
{
    [Serializable]
    public struct MonsterSpawnInfo
    {
        [SerializeField]
        MonsterType _monsterType;
        [SerializeField]
        byte _lane;
        [SerializeField]
        float _time;

        public MonsterType MonsterType => _monsterType;
        public byte Lane => _lane;
        public float Time => _time;
    }
}
