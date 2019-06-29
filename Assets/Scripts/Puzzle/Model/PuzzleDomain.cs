using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Model
{
    public class PuzzleDomain
    {
        /// <summary>
        /// 結果イベント発行者
        /// </summary>
        readonly EventPublisher<PuzzleSphere[]> _resultEventPublisher = new EventPublisher<PuzzleSphere[]>();

        /// <summary>
        /// 全マス
        /// </summary>
        PieceColor[,] _squares = new PieceColor[4, 4];


        public PuzzleDomain(int columns, int rows)
        {

        }

        /// <summary>
        /// 結果を購読する．
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IDisposable SubscribeResult(Action<PuzzleSphere[]> action) => _resultEventPublisher.Subscribe(action);

        public IEnumerable Initialize()
        {
            yield return null;
        }

        public void ChangeSquares((View.TapSquareComponent.Direction direction, int column, int row) directionAndCoordinate)
        {

        }

        public void Result()
        {
            //結果を出す．
            PuzzleSphere[] results = new PuzzleSphere[4];
            _resultEventPublisher.Publish(results);
        }

        public void Reset()
        {
            //ピースの配列内をリセットする．
        }
    }
}
