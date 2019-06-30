using Puzzle;
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
        LaneViewContainer _laneViewContainer;
        [SerializeField]
        PuzzleProjectileViewContainer _puzzleProjectileViewContainer;

        MonsterViewFactory _monsterViewFactory;
        List<MonsterView> _spawnMonsterViews = new List<MonsterView>();
        MonsterSpawnInfo[] _monsterSpawnInfoOrderedByTime;
        int _nextSpawnMonsterIndex;
        bool _shouldSpawnMonster = true;

        PuzzleProjectileFactory _puzzleProjectileFactory;

        float _timer;

        IEnumerator Start()
        {
            _monsterViewFactory = new MonsterViewFactory(_monsterViewContainer, _laneViewContainer);
            _monsterSpawnInfoOrderedByTime = _monsterSpawnInfo.OrderBy(info => info.Time).ToArray();

            _puzzleProjectileFactory = new PuzzleProjectileFactory(_puzzleProjectileViewContainer);

            var shouldContinue = true;
            using (_laneViewContainer.DeadLineView.SubscribeMonsterViewEnter(monsterView => shouldContinue = false))
            {
                while (shouldContinue)
                {
                    _timer += Time.deltaTime;

                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        StartCoroutine(Shoot(PieceColor.Red, 1));
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        StartCoroutine(Shoot(PieceColor.Red, 2));
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        StartCoroutine(Shoot(PieceColor.Red, 3));
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        StartCoroutine(Shoot(PieceColor.Red, 4));
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

        IEnumerator Shoot(PieceColor pieceColor, byte lane)
        {
            CannonView cannonView = _laneViewContainer.GetLaneViewFromLaneNumber(lane).CannonView;

            var puzzleProjectileView = _puzzleProjectileFactory.CreatePuzzleProjectile(pieceColor);
            foreach (var hitTarget in cannonView.Shoot(puzzleProjectileView))
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

