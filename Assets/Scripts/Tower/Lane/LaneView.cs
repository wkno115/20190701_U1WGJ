using Pyke;
using Tower.Cannon;
using UnityEngine;

namespace Tower.Lane
{
    public class LaneView : AbstractView
    {
        [SerializeField]
        LaneState _laneState;
        [SerializeField]
        CannonView _cannonView;

        public uint LaneNumber => _laneState.LaneNumber;
        public Vector3 MonsterSpawnPosition => _laneState.MonsterSpawnPoint.position;
        public CannonView CannonView => _cannonView;
    }
}
