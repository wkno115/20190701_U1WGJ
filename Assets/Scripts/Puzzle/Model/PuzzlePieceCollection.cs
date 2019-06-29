using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Model
{
    public class PuzzlePieceCollection
    {
        PieceColor[,] _pieces;

        public PuzzlePieceCollection(int columns, int rows)
        {
            _pieces = new PieceColor[columns, rows];
        }

        public void ChangePieces((View.TapSquareComponent.Direction direction, int column, int row) directionAndCoordinate)
        {
            switch (directionAndCoordinate.direction)
            {
                case View.TapSquareComponent.Direction.Up:
                    break;
                case View.TapSquareComponent.Direction.Down:
                    break;
                case View.TapSquareComponent.Direction.Left:
                    break;
                case View.TapSquareComponent.Direction.Right:
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 循環する値を取得
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        int _getLoopValue(int value, int max, int min)
        {
            return max < value ? (value % max) : value < min ? max - (value - min) : value;
        }
    }
}
