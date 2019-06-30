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
            var columns = pieces.GetLength(0);
            var rows = pieces.GetLength(1);

            //見える部分のピースの色を変える
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    _puzzlePieces[column + 1, row + 1].SetColor(pieces[column, row]);
                }
                yield return null;
            }

            ArrayPrinter<PieceColor>.Print(pieces);
            /*
            //演出用のピースの色を変える
            //上下の行の色を変える．
            for (var column = 0; column < columns; column++)
            {
                //演出用ピースは行列の上下左右の飛び出た部分である為，+1した列が対応する．
                //同様に，行の最大値を入れても問題ない．
                //また，上には見える部分の一番下を入れ，下はその逆になっているためやや煩雑になってしまった．
                _puzzlePieces[column + 1, 0].SetColor(pieces[column, rows - 1]);
                _puzzlePieces[column + 1, rows + 1].SetColor(pieces[column, 0]);
            }
            //左右の列の色を変える．
            for (var row = 0; row < rows; row++)
            {
                _puzzlePieces[0, row + 1].SetColor(pieces[columns - 1, row]);
                _puzzlePieces[columns + 1, row + 1].SetColor(pieces[0, row]);
            }*/
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
                    piece.transform.localPosition = _startPositionTransform.localPosition + new Vector3(column * piece.GetWidth(), row * -piece.GetHeight());
                    _puzzlePieces[column, row] = piece;
                }
                yield return null;
            }
        }
    }
}
