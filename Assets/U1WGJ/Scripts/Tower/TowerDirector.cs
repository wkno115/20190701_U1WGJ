using Play;
using Puzzle;
using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tower.Cannon;
using Tower.Lane;
using Tower.Monster;
using Tower.UI;
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
        TowerUI _towerUI;
        [SerializeField]
        float _interval = .5f;
        [SerializeField]
        int _sameColorScore = 100;
        [SerializeField]
        int _diffColorScore = 0;

        List<MonsterView> _activatedMonsterViews = new List<MonsterView>();
        Dictionary<float, List<MonsterView>> _spawnTimeToMonsterViews = new Dictionary<float, List<MonsterView>>();

        PuzzleProjectileFactory _puzzleProjectileFactory;

        EventPublisher<PlayResult> _playResultEventPublisher = new EventPublisher<PlayResult>();

        float _timer;
        int _score;
        bool _shouldContinue;
        bool _shouldPause;

        float _lane1Interval;
        float _lane2Interval;
        float _lane3Interval;
        float _lane4Interval;

        Queue<PieceColor> _lane1PieceQueue = new Queue<PieceColor>();
        Queue<PieceColor> _lane2PieceQueue = new Queue<PieceColor>();
        Queue<PieceColor> _lane3PieceQueue = new Queue<PieceColor>();
        Queue<PieceColor> _lane4PieceQueue = new Queue<PieceColor>();

        void Awake()
        {
            _lane1Interval = _interval;
            _lane2Interval = _interval;
            _lane3Interval = _interval;
            _lane4Interval = _interval;
        }

        public IEnumerable Run()
        {
            var monsterViewFactory = new MonsterViewFactory(_monsterViewContainer, _laneViewContainer);
            _puzzleProjectileFactory = new PuzzleProjectileFactory(_puzzleProjectileViewContainer);

            var monsterSpawnInfo = _getSpawnInfo();

            var spawnMonsterViews = new MonsterView[monsterSpawnInfo.Length];
            foreach (var (info, index) in monsterSpawnInfo.Index())
            {
                var monsterView = monsterViewFactory.CreateMonster(info.MonsterType, info.Lane);
                monsterView.SetActive(false);

                if (_spawnTimeToMonsterViews.ContainsKey(info.Time))
                {
                    _spawnTimeToMonsterViews[info.Time].Add(monsterView);
                }
                else
                {
                    _spawnTimeToMonsterViews.Add(info.Time, new List<MonsterView> { monsterView });
                }

                spawnMonsterViews[index] = monsterView;
            }

            _shouldContinue = true;
            using (_laneViewContainer.DeadLineView.SubscribeMonsterViewEnter(monsterView => _shouldContinue = false))
            using (new DisposeComposer(spawnMonsterViews.Select(view => view.SubscribeDead(_onMonsterDead)).ToArray()))
            {
                while (_shouldContinue)
                {
                    _timer += Time.deltaTime;
                    _towerUI.SetTime(_timer);

                    _testProjectiles();
                    _activateMonster();
                    _shootProjectiles();
                    _moveMonster();

                    yield return null;
                }
            }

            _playResultEventPublisher.Publish(new PlayResult(_timer, _score));
            yield return null;
        }

        MonsterSpawnInfo[] _getSpawnInfo()
        {
            var monsterSpawnInfo = new MonsterSpawnInfo[100];
            var spawnTimeDispersionMax = 5;
            var spawnTimeDispersionMin = -5;
            for (int i = 0; i < monsterSpawnInfo.Length; i++)
            {
                var spawnMonsterType = EnumCommon.Random<MonsterType>();
                var spawnLane = (byte)RandomValueFactory.CreateRandomValue(1, 4);
                var spawnTime = RandomValueFactory.CreateRandomValue(i + spawnTimeDispersionMin, i + spawnTimeDispersionMax);
                if (spawnTime < 0)
                {
                    spawnTime = 0;
                }
                monsterSpawnInfo[i] = new MonsterSpawnInfo(spawnMonsterType, spawnLane, spawnTime);
            }

            return monsterSpawnInfo;
        }

        void _onMonsterDead(MonsterView monsterView)
        {
            _activatedMonsterViews.Remove(monsterView);
        }
        void _testProjectiles()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Shoot(PieceColor.Red, 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Shoot(PieceColor.Blue, 2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Shoot(PieceColor.Yellow, 3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Shoot(PieceColor.Green, 4);
            }
        }
        List<float> _removedTimes = new List<float>();
        void _activateMonster()
        {
            foreach (var timeToView in _spawnTimeToMonsterViews)
            {
                var spawnTime = timeToView.Key;
                var monsterViews = timeToView.Value;

                if (spawnTime < _timer)
                {
                    foreach (var monsterView in monsterViews)
                    {
                        monsterView.SetActive(true);
                        _activatedMonsterViews.Add(monsterView);
                    }
                    _removedTimes.Add(spawnTime);
                }
            }
            foreach (var time in _removedTimes)
            {
                _spawnTimeToMonsterViews.Remove(time);
            }
            _removedTimes.Clear();
        }
        void _moveMonster()
        {
            foreach (var monsterView in _activatedMonsterViews)
            {
                if (!_shouldPause)
                {
                    monsterView.Move(Vector3.back * Time.deltaTime);
                }
            }
        }

        void _shootProjectiles()
        {
            _lane1Interval -= Time.deltaTime;
            if (_lane1Interval < 0 && _lane1PieceQueue.Count > 0)
            {
                StartCoroutine(_shoot(_lane1PieceQueue.Dequeue(), 1));
                _lane1Interval = _interval;
            }
            _lane2Interval -= Time.deltaTime;
            if (_lane2Interval < 0 && _lane2PieceQueue.Count > 0)
            {
                StartCoroutine(_shoot(_lane2PieceQueue.Dequeue(), 2));
                _lane2Interval = _interval;
            }
            _lane3Interval -= Time.deltaTime;
            if (_lane3Interval < 0 && _lane3PieceQueue.Count > 0)
            {
                StartCoroutine(_shoot(_lane3PieceQueue.Dequeue(), 3));
                _lane3Interval = _interval;
            }
            _lane4Interval -= Time.deltaTime;
            if (_lane4Interval < 0 && _lane4PieceQueue.Count > 0)
            {
                StartCoroutine(_shoot(_lane4PieceQueue.Dequeue(), 4));
                _lane4Interval = _interval;
            }
        }

        public void Shoot(PieceColor pieceColor, byte lane)
        {
            switch (lane)
            {
                case 1:
                    _lane1PieceQueue.Enqueue(pieceColor);
                    break;
                case 2:
                    _lane2PieceQueue.Enqueue(pieceColor);
                    break;
                case 3:
                    _lane3PieceQueue.Enqueue(pieceColor);
                    break;
                case 4:
                    _lane4PieceQueue.Enqueue(pieceColor);
                    break;
            }
        }

        IEnumerator _shoot(PieceColor pieceColor, byte lane)
        {
            SoundPlayComponent.Instance.PlayCannonFireSe();

            var cannonView = _laneViewContainer.GetLaneViewFromLaneNumber(lane).CannonView;
            var puzzleProjectileView = _puzzleProjectileFactory.CreatePuzzleProjectile(pieceColor);
            foreach (var hitTarget in cannonView.Shoot(puzzleProjectileView))
            {
                if (!_shouldContinue)
                {
                    break;
                }
                if (_shouldPause)
                {
                    yield return null;
                    continue;
                }
                if (hitTarget != null)
                {
                    SoundPlayComponent.Instance.PlayProjectileHitSe();

                    var damage = puzzleProjectileView.AttackPower;
                    if (puzzleProjectileView.PieceColor == hitTarget.PieceColor)
                    {
                        damage *= 2;
                        _score += _sameColorScore;
                    }
                    else
                    {
                        damage = 0;
                        _score += _diffColorScore;
                    }
                    _towerUI.SetScore(_score);
                    hitTarget.ChangeHp(-damage);
                    break;
                }
                yield return null;
            }
            puzzleProjectileView.Dead();
        }

        public void Pause(bool isOn)
        {
            _shouldPause = isOn;
        }

        public IDisposable SubscribePlayResult(Action<PlayResult> action)
        {
            return _playResultEventPublisher.Subscribe(action);
        }
    }
}

