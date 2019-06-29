using Tower.Lane;
using UnityEngine;

namespace Tower.Monster
{
    public class MonsterViewFactory
    {
        MonsterViewContainer _monsterViewContainer;
        LaneInfoContainer _laneInfoContainer;

        public MonsterViewFactory(MonsterViewContainer monsterViewContainer, LaneInfoContainer laneInfoContainer)
        {
            _monsterViewContainer = monsterViewContainer;
            _laneInfoContainer = laneInfoContainer;
        }

        public MonsterView CreateMonster(MonsterType monsterType, byte lane)
        {
            Vector3 spawnPosition = Vector3.zero;
            switch (lane)
            {
                case 1:
                    spawnPosition = _laneInfoContainer.Lane1SpawnPointTransform.position;
                    break;
                case 2:
                    spawnPosition = _laneInfoContainer.Lane2SpawnPointTransform.position;
                    break;
                case 3:
                    spawnPosition = _laneInfoContainer.Lane3SpawnPointTransform.position;
                    break;
                case 4:
                    spawnPosition = _laneInfoContainer.Lane4SpawnPointTransform.position;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException("lane must be between 1 and 4.");
            }
            MonsterView monsterView = null;
            switch (monsterType)
            {
                case MonsterType.ork:
                    monsterView = _monsterViewContainer.OrkView;
                    break;
                case MonsterType.witch:
                    monsterView = _monsterViewContainer.WitchView;
                    break;
                case MonsterType.elf:
                    monsterView = _monsterViewContainer.ElfView;
                    break;
            }

            return Object.Instantiate(monsterView, spawnPosition, Quaternion.identity);
        }
    }
}
