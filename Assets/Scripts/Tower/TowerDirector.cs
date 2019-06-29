using Pyke;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tower.Cannon;
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
        [SerializeField]
        DeadLineView _deadLineView;
        [SerializeField]
        CannonView _cannonView1;
        [SerializeField]
        CannonView _cannonView2;
        [SerializeField]
        CannonView _cannonView3;
        [SerializeField]
        CannonView _cannonView4;
        [SerializeField]
        ProjectileView _testProjectileView;

        MonsterViewFactory _monsterViewFactory;
        MonsterSpawnInfo[] _monsterSpawnInfoOrderedByTime;
        float _timer;
        int _nextSpawnMonsterIndex;
        bool _shouldSpawnMonster = true;

        List<MonsterView> _spawnMonsterViews = new List<MonsterView>();

        IEnumerator Start()
        {
            _monsterViewFactory = new MonsterViewFactory(_monsterViewContainer, _laneInfoContainer);
            _monsterSpawnInfoOrderedByTime = _monsterSpawnInfo.OrderBy(info => info.Time).ToArray();

            var shouldContinue = true;
            using (_deadLineView.SubscribeMonsterViewEnter(monsterView => shouldContinue = false))
            {
                while (shouldContinue)
                {
                    _timer += Time.deltaTime;

                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        StartCoroutine(Shoot(0, 1));
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        StartCoroutine(Shoot(0, 2));
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        StartCoroutine(Shoot(0, 3));
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        StartCoroutine(Shoot(0, 4));
                    }

                    _spawnMonster();
                    _moveMonster();

                    yield return null;
                }
            }

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

        IEnumerator Shoot(int projectileTypeId, byte lane)
        {
            CannonView cannonView = null;
            switch (lane)
            {
                case 1:
                    cannonView = _cannonView1;
                    break;
                case 2:
                    cannonView = _cannonView2;
                    break;
                case 3:
                    cannonView = _cannonView3;
                    break;
                case 4:
                    cannonView = _cannonView4;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException("lane must be between 1 and 4.");
            }

            foreach (var hitTarget in cannonView.Shoot(_testProjectileView))
            {
                if (hitTarget != null)
                {
                    print(hitTarget.name);
                }
                yield return null;
            }
        }
    }
}

