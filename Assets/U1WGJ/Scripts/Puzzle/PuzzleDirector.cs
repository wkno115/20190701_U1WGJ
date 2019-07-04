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
        static byte COLUMNS = 4;
        static byte ROWS = 4;

        [SerializeField]
        PuzzleUI _ui;

        PuzzleController _puzzleController;

        public IDisposable SubscribeResultEffectStart(Action action) => _puzzleController.SubscribeResultEffectStart(action);

        public IEnumerable Initialize()
        {
            var domain = new PuzzleDomain(COLUMNS, ROWS);
            foreach (var _ in domain.Initialize())
            {
                yield return null;
            }
            foreach (var _ in _ui.Initialize(domain.GetAllPieces()))
            {
                yield return null;
            }

            _puzzleController = new PuzzleController(_ui, domain);

            foreach (var _ in _puzzleController.Initialize())
            {
                yield return null;
            }
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <returns>処理中 IEnumerable</returns>
        public IEnumerable<PuzzleSphere[]> Run(Func<bool> shouldContinue)
        {
            PuzzleSphere[] result = null;
            foreach (var element in _puzzleController.Run())
            {
                yield return null;
                result = element;
                if (result != null)
                {
                    break;
                }
            }
            yield return result;
        }
    }
}

