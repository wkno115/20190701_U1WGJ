using Puzzle.Model;
using Puzzle.View;
using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    /// <summary>
    /// パズルサイド実行者
    /// </summary>
    public class PuzzleDirector : MonoBehaviour
    {
        static int COLUMNS = 4;
        static int ROWS = 4;

        [SerializeField]
        PuzzleUI _ui;

        readonly EventPublisher<PuzzleSphere[]> _resultEventPublisher = new EventPublisher<PuzzleSphere[]>();

        PuzzleController controller;


        public IDisposable SubscribeResult(Action<PuzzleSphere[]> action) => _resultEventPublisher.Subscribe(action);


        private IEnumerator Start()
        {
            foreach (var _ in Initialize())
            {
                yield return null;
            }
            foreach (var _ in Run(() => true))
            {
                yield return null;
            }
        }

        public IEnumerable Initialize()
        {
            var domain = new PuzzleDomain(COLUMNS, ROWS);
            foreach (var _ in _ui.Initialize(COLUMNS, ROWS))
            {
                yield return null;
            }

            controller = new PuzzleController(_ui, domain);

            foreach (var _ in controller.Initialize())
            {
                yield return null;
            }
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <returns>処理中 IEnumerable</returns>
        public IEnumerable Run(Func<bool> shouldContinue)
        {
            foreach (var _ in controller.Run())
            {
                if (!shouldContinue())
                {
                    yield break;
                }
                yield return null;
            }
        }
    }
}

