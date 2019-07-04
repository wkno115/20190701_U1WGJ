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

        public MonsterSpawnInfo(MonsterType monsterType, byte lane, float time)
        {
            _monsterType = monsterType;
            _lane = lane;
            _time = time;
        }

        public MonsterType MonsterType => _monsterType;
        public byte Lane => _lane;
        public float Time => _time;
    }
}
