using Tower.Lane;
using UnityEngine;

namespace Tower.Monster
{
    public class MonsterViewFactory
    {
        MonsterViewContainer _monsterViewContainer;
        LaneViewContainer _laneViewContainer;

        public MonsterViewFactory(MonsterViewContainer monsterViewContainer, LaneViewContainer laneInfoContainer)
        {
            _monsterViewContainer = monsterViewContainer;
            _laneViewContainer = laneInfoContainer;
        }

        public MonsterView CreateMonster(MonsterType monsterType, byte lane)
        {
            var spawnPosition = _laneViewContainer.GetLaneViewFromLaneNumber(lane).MonsterSpawnPosition;

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

            var createdView = Object.Instantiate(monsterView, spawnPosition, Quaternion.identity);
            createdView.gameObject.SetActive(true);
            return createdView;
        }
    }
}
