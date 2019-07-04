using System;
using UnityEngine;

namespace Tower.Lane
{
    [Serializable]
    public struct LaneState
    {
        [SerializeField]
        uint _laneNumber;
        [SerializeField]
        Transform _monsterSpawnPoint;

        public uint LaneNumber => _laneNumber;
        public Transform MonsterSpawnPoint => _monsterSpawnPoint;
    }
}
