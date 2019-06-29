using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.View
{
    public class PuzzlePieceContainer : MonoBehaviour
    {
        [SerializeField]
        Transform _startPositionTransform;
        [SerializeField]
        PuzzlePieceComponent _puzzlePiecePrefab;

        Transform _transform;


        void Awake()
        {
            _transform = transform;
        }


        /// <summary>
        /// パズルピースをインスタンス化．
        /// </summary>
        /// <returns></returns>
        public IEnumerable InstantiatePieces(int columns,int rows)
        {
            PuzzlePieceComponent piece;
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    piece = Instantiate(_puzzlePiecePrefab, _transform);
                    piece.transform.localPosition = _startPositionTransform.localPosition + new Vector3(row * piece.GetWidth(), column * -piece.GetHeight());
                }
                yield return null;
            }
        }
    }
}
