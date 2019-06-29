using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.View
{
    public class PuzzleUI : MonoBehaviour
    {
        //TODO:Panelを経由

        /// <summary>
        /// タップマスコンテナ
        /// </summary>
        [SerializeField]
        TapSquareContainer _tapSquareContainer;
        /// <summary>
        /// パズルピースコンテナ
        /// </summary>
        [SerializeField]
        PuzzlePieceContainer _puzzlePieceContainer;

        public TapSquareContainer GetTapSquareContainer() => _tapSquareContainer;
        public PuzzlePieceContainer GetPuzzleContainer() => _puzzlePieceContainer;

        public IEnumerable Initialize(int columns, int rows)
        {
            foreach (var _ in _tapSquareContainer.InstantiateSquares(columns, rows))
            {
                yield return null;
            }
            foreach(var _ in _puzzlePieceContainer.InstantiatePieces(columns,rows))
            {
                yield return null;
            }
        }
    }
}
