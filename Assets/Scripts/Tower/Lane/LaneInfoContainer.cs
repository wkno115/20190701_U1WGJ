using System;
using UnityEngine;

namespace Tower.Lane
{
    [Serializable]
    public class LaneInfoContainer
    {
        [SerializeField]
        Transform _lane1SpawnPointTransform;
        [SerializeField]
        Transform _lane2SpawnPointTransform;
        [SerializeField]
        Transform _lane3SpawnPointTransform;
        [SerializeField]
        Transform _lane4SpawnPointTransform;

        public Transform Lane1SpawnPointTransform => _lane1SpawnPointTransform;
        public Transform Lane2SpawnPointTransform => _lane2SpawnPointTransform;
        public Transform Lane3SpawnPointTransform => _lane3SpawnPointTransform;
        public Transform Lane4SpawnPointTransform => _lane4SpawnPointTransform;
    }
}
