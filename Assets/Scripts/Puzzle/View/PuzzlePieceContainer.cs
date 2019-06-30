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
        [SerializeField]
        float _moveTime = 0.4f;

        Transform _transform;
        PuzzlePieceComponent[,] _puzzlePieces;


        void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="pieces"></param>
        /// <returns></returns>
        public IEnumerable Initialize(PieceColor[,] pieces)
        {
            //上下左右にループ演出用のピースを余分に用意する．
            _puzzlePieces = new PuzzlePieceComponent[pieces.GetLength(0) + 2, pieces.GetLength(1) + 2];
            foreach (var _ in _instantiatePieces((byte)_puzzlePieces.GetLength(0), (byte)_puzzlePieces.GetLength(1)))
            {
                yield return null;
            }
            foreach (var _ in SetColor(pieces))
            {
                yield return null;
            }
        }
        /// <summary>
        /// ピースに色をセット
        /// </summary>
        /// <param name="pieces">ピースカラー</param>
        /// <returns></returns>
        public IEnumerable SetColor(PieceColor[,] pieces)
        {
            for (var column = 0; column < pieces.GetLength(0); column++)
            {
                for (var row = 0; row < pieces.GetLength(1); row++)
                {
                    _puzzlePieces[column + 1, row + 1].SetColor(pieces[column, row]);
                }
                yield return null;
            }
            //演出用のピースの色を変える
//            for(var column=0; )
        }

        /// <summary>
        /// 移動アニメーション
        /// </summary>
        /// <param name="directionAndCoordinate"></param>
        /// <returns></returns>
        public IEnumerable MoveAnimation((View.TapSquareComponent.Direction direction, byte column, byte row) directionAndCoordinate)
        {
            //移動を行う．
            yield return null;
        }
        /// <summary>
        /// 結果アニメーション
        /// </summary>
        /// <returns></returns>
        public IEnumerable ResultAnimation()
        {
            yield return null;
        }

        /// <summary>
        /// パズルピースをインスタンス化．
        /// </summary>
        /// <returns></returns>
        IEnumerable _instantiatePieces(byte columns, byte rows)
        {
            PuzzlePieceComponent piece;
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    piece = Instantiate(_puzzlePiecePrefab, _transform);
                    piece.transform.localPosition = _startPositionTransform.localPosition + new Vector3(row * piece.GetWidth(), column * -piece.GetHeight());
                    _puzzlePieces[column, row] = piece;
                }
                yield return null;
            }
        }
    }
}
