using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            _setColor(pieces);
        }

        public IEnumerable UpdatePieces((View.TapSquareComponent.Direction direction, byte column, byte row) directionAndCoordinate, PieceColor[,] nextPieces)
        {
            yield return null;
            _setColor(nextPieces);
        }
        /// <summary>
        /// 結果処理
        /// </summary>
        /// <returns></returns>
        public IEnumerable ResultProcess(PieceColor[,] nextPieces)
        {
            yield return null;
        }

        /// <summary>
        /// ピースに色をセット
        /// </summary>
        /// <param name="pieces">ピースカラー</param>
        /// <returns></returns>
        void _setColor(PieceColor[,] pieces)
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
            }

            ArrayPrinter<PieceColor>.Print(pieces);

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
            }
        }
        /// <summary>
        /// 移動アニメーション
        /// </summary>
        /// <param name="directionAndCoordinate"></param>
        /// <returns></returns>
        IEnumerable _moveAnimation((View.TapSquareComponent.Direction direction, byte column, byte row) directionAndCoordinate)
        {
            //移動を行う．
            var targetPieces = new List<PuzzlePieceComponent>();
            var distance = 0f;
            if (directionAndCoordinate.direction == TapSquareComponent.Direction.Up || directionAndCoordinate.direction == TapSquareComponent.Direction.Down)
            {
                for (var row = 0; row < _puzzlePieces.GetLength(1); row++)
                {
                    targetPieces.Add(_puzzlePieces[directionAndCoordinate.column, row]);
                }
                distance = directionAndCoordinate.direction == TapSquareComponent.Direction.Up ? _puzzlePiecePrefab.GetHeight() : -_puzzlePiecePrefab.GetHeight();
            }
            else
            {
                for (var column = 0; column < _puzzlePieces.GetLength(0); column++)
                {
                    targetPieces.Add(_puzzlePieces[column, directionAndCoordinate.row]);
                }
                distance = directionAndCoordinate.direction == TapSquareComponent.Direction.Up ? _puzzlePiecePrefab.GetWidth() : -_puzzlePiecePrefab.GetWidth();
            }
            yield return null;
        }
        /// <summary>
        /// パズルピースをインスタンス化．
        /// </summary>
        /// <param name="columns">列数</param>
        /// <param name="rows">行数</param>
        /// <returns>処理中</returns>
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
