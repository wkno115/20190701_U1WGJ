using Puzzle;
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
        LaneViewContainer _laneViewContainer;
        [SerializeField]
        PuzzleProjectileViewContainer _puzzleProjectileViewContainer;
        [SerializeField]
        MonsterSpawnInfo[] _monsterSpawnInfo;

        List<MonsterView> _activatedMonsterViews = new List<MonsterView>();
        Dictionary<float, MonsterView> _spawnTimeToMonsterView = new Dictionary<float, MonsterView>();
        int _nextSpawnMonsterIndex;
        bool _shouldSpawnMonster = true;

        PuzzleProjectileFactory _puzzleProjectileFactory;

        float _timer;

        IEnumerator Start()
        {
            var monsterViewFactory = new MonsterViewFactory(_monsterViewContainer, _laneViewContainer);
            _puzzleProjectileFactory = new PuzzleProjectileFactory(_puzzleProjectileViewContainer);

            var spawnMonsterViews = new MonsterView[_monsterSpawnInfo.Length];
            foreach (var (info, index) in _monsterSpawnInfo.Index())
            {
                var monsterView = monsterViewFactory.CreateMonster(info.MonsterType, info.Lane);
                monsterView.SetActive(false);

                _spawnTimeToMonsterView.Add(info.Time, monsterView);
                spawnMonsterViews[index] = monsterView;
            }

            var shouldContinue = true;
            using (_laneViewContainer.DeadLineView.SubscribeMonsterViewEnter(monsterView => shouldContinue = false))
            using (new DisposeComposer(spawnMonsterViews.Select(view => view.SubscribeDead(_onMonsterDead)).ToArray()))
            {
                while (shouldContinue)
                {
                    _timer += Time.deltaTime;

                    _testProjectiles();
                    _activateMonster();
                    _moveMonster();

                    yield return null;
                }
            }

        }

        void _onMonsterDead(MonsterView monsterView)
        {
            _activatedMonsterViews.Remove(monsterView);
        }
        void _testProjectiles()
        {
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
        }
        List<float> _removedTimes = new List<float>();
        void _activateMonster()
        {
            foreach (var timeToView in _spawnTimeToMonsterView)
            {
                var spawnTime = timeToView.Key;
                var monsterView = timeToView.Value;

                if (spawnTime < _timer)
                {
                    monsterView.SetActive(true);
                    _activatedMonsterViews.Add(monsterView);
                    _removedTimes.Add(spawnTime);
                }
            }
            foreach (var time in _removedTimes)
            {
                _spawnTimeToMonsterView.Remove(time);
            }
            _removedTimes.Clear();
        }
        void _moveMonster()
        {
            foreach (var monsterView in _activatedMonsterViews)
            {
                monsterView.Move(Vector3.back * Time.deltaTime);
            }
        }

        public IEnumerator Shoot(PieceColor pieceColor, byte lane)
        {
            CannonView cannonView = _laneViewContainer.GetLaneViewFromLaneNumber(lane).CannonView;

            var puzzleProjectileView = _puzzleProjectileFactory.CreatePuzzleProjectile(pieceColor);
            foreach (var hitTarget in cannonView.Shoot(puzzleProjectileView))
            {
                if (hitTarget != null)
                {
                    hitTarget.ChangeHp(-puzzleProjectileView.AttackPower);
                }
                yield return null;
            }
        }
    }
}

