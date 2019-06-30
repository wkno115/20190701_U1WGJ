using Puzzle;
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
            var monsterView = _monsterViewContainer.GetMonsterViewFromMonsterType(monsterType);
            var spawnPosition = _laneViewContainer.GetLaneViewFromLaneNumber(lane).MonsterSpawnPosition;
            var createdView = Object.Instantiate(monsterView, spawnPosition, Quaternion.AngleAxis(180, Vector3.up));
            createdView.gameObject.SetActive(true);
            var randomPieceColor = EnumCommon.Random<PieceColor>();
            createdView.SetColor(randomPieceColor);
            return createdView;
        }
    }
}
