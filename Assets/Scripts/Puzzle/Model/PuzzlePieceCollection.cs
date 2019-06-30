using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Model
{
    /// <summary>
    /// パズルピースの集合を扱う
    /// </summary>
    public class PuzzlePieceCollection
    {
        PieceColor[,] _pieces;

        public PuzzlePieceCollection(byte columns, byte rows)
        {
            _pieces = new PieceColor[columns, rows];
        }

        public void ChangePieces((View.TapSquareComponent.Direction direction, byte column, byte row) directionAndCoordinate)
        {
            switch (directionAndCoordinate.direction)
            {
                case View.TapSquareComponent.Direction.Up:
                    _columnMove(directionAndCoordinate.column, false);
                    break;
                case View.TapSquareComponent.Direction.Down:
                    _columnMove(directionAndCoordinate.column, true);
                    break;
                case View.TapSquareComponent.Direction.Left:
                    _rowMove(directionAndCoordinate.row, false);
                    break;
                case View.TapSquareComponent.Direction.Right:
                    _rowMove(directionAndCoordinate.row, true);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 結果となる玉を取得
        /// </summary>
        /// <returns></returns>
        public PuzzleSphere[] GetResultSpheres()
        {
            PuzzleSphere[] result = new PuzzleSphere[_pieces.LongLength];
            for (byte column = 0; column < _pieces.GetLength(0); column++)
            {
                for (byte row = 0; row < _pieces.GetLength(1); row++)
                {
                    //列が1ずれると，個数としては1行分ずれる．
                    result[column * _pieces.GetLength(1) + row] = new PuzzleSphere(column, _pieces[column, row], 1);
                }
            }
            return result;
        }
        /// <summary>
        /// 全ピースを取得．
        /// </summary>
        /// <returns></returns>
        public PieceColor[,] GetPieces()
        {
            return _pieces;
        }
        /// <summary>
        /// リセット
        /// </summary>
        public void Reset()
        {
            //TODO:ランダムに突っ込んでるけど，もう少しいい感じに突っ込みたい
            for (byte column = 0; column < _pieces.GetLength(0); column++)
            {
                for (byte row = 0; row < _pieces.GetLength(1); row++)
                {
                    _pieces[column, row] = EnumCommon.Random<PieceColor>();
                }
            }
        }
        public override string ToString()
        {
            return _pieces.ToString();
        }

        /////////////多分行列の移動は共通化できる////////////////////////

        /// <summary>
        /// 列移動
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="isDown">下移動なら真</param>
        void _columnMove(byte column, bool isDown)
        {
            var count = isDown ? _pieces.GetLength(1) - 1 : 0;
            var diff = isDown ? -1 : 1;
            var end = isDown ? 0 : _pieces.GetLength(1) - 1;
            var temp = _pieces[count, count];

            var judge = isDown ? count + diff > end : count + diff < end;
            while (judge)
            {
                _pieces[column, count] = _pieces[column, count + diff];
                count += diff;
                judge = isDown ? count + diff > end : count + diff < end;
            }
            _pieces[column, count] = temp;
        }
        /// <summary>
        /// 行移動
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="isRight">右移動なら真</param>
        void _rowMove(byte row, bool isRight)
        {
            //右に動くときはカウントを最後尾から初めて，順に入れ替えていく．
            //tempに最初に入れ替える値を保持しておいて，最後に変える
            var count = isRight ? _pieces.GetLength(0) - 1 : 0;
            var diff = isRight ? -1 : 1;
            var end = isRight ? 0 : _pieces.GetLength(0) - 1;
            var temp = _pieces[count, row];
            var judge = isRight ? count + diff > end : count + diff < end;
            while (judge)
            {
                _pieces[count, row] = _pieces[count + diff, row];
                count += diff;
                judge = isRight ? count + diff > end : count + diff < end;
            }
            _pieces[count, row] = temp;
        }
    }
}
