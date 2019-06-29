using System.Collections.Generic;
using System.Linq;
using Tower.Lane;
using Tower.Monster;
using UnityEngine;

namespace Tower
{
    public class TowerDirector : MonoBehaviour
    {
        [SerializeField]
        MonsterViewContainer _monsterViewContainer;
        [SerializeField]
        MonsterSpawnInfo[] _monsterSpawnInfo;
        [SerializeField]
        LaneInfoContainer _laneInfoContainer;

        MonsterViewFactory _monsterViewFactory;
        MonsterSpawnInfo[] _monsterSpawnInfoOrderedByTime;
        float _timer;
        int _nextSpawnMonsterIndex;
        bool _shouldSpawnMonster = true;

        List<MonsterView> _spawnMonsterViews = new List<MonsterView>();

        void Start()
        {
            _monsterViewFactory = new MonsterViewFactory(_monsterViewContainer, _laneInfoContainer);
            _monsterSpawnInfoOrderedByTime = _monsterSpawnInfo.OrderBy(info => info.Time).ToArray();
        }

        void Update()
        {
            _timer += Time.deltaTime;

            _spawnMonster();
            _moveMonster();
        }

        void _spawnMonster()
        {
            if (_monsterSpawnInfoOrderedByTime[_nextSpawnMonsterIndex].Time < _timer && _shouldSpawnMonster)
            {
                var monsterType = _monsterSpawnInfoOrderedByTime[_nextSpawnMonsterIndex].MonsterType;
                var spawnLane = _monsterSpawnInfoOrderedByTime[_nextSpawnMonsterIndex].Lane;
                var monsterView = _monsterViewFactory.CreateMonster(monsterType, spawnLane);
                _spawnMonsterViews.Add(monsterView);
                ++_nextSpawnMonsterIndex;
                if (_nextSpawnMonsterIndex == _monsterSpawnInfo.Length)
                {
                    _nextSpawnMonsterIndex = 0;
                    _shouldSpawnMonster = false;
                }
            }
        }
        void _moveMonster()
        {
            foreach (var monsterView in _spawnMonsterViews)
            {
                monsterView.Move(Vector3.back * Time.deltaTime);
            }
        }
    }
}

