using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Model
{
    /// <summary>
    /// パズルドメインロジック
    /// </summary>
    public class PuzzleDomain
    {
        /// <summary>
        /// 結果イベント発行者
        /// </summary>
        readonly EventPublisher<PuzzleSphere[]> _resultEventPublisher = new EventPublisher<PuzzleSphere[]>();
        /// <summary>
        /// パズル移動イベント発行者
        /// </summary>
        readonly EventPublisher<(View.TapSquareComponent.Direction direction, int column, int row)> _moveEventPublisher = new EventPublisher<(View.TapSquareComponent.Direction direction, int column, int row)>();

        /// <summary>
        /// 全マス
        /// </summary>
        PuzzlePieceCollection _pieces;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="rows">行</param>
        public PuzzleDomain(int columns, int rows)
        {
            _pieces = new PuzzlePieceCollection(columns, rows);
        }

        /// <summary>
        /// 結果を購読する．
        /// </summary>
        /// <param name="action">紐づけ処理</param>
        /// <returns>購読解除</returns>
        public IDisposable SubscribeResult(Action<PuzzleSphere[]> action) => _resultEventPublisher.Subscribe(action);
        /// <summary>
        /// 移動を購読する．
        /// </summary>
        /// <param name="action">紐づけ処理</param>
        /// <returns>購読解除</returns>
        public IDisposable SubscribeMove(Action<(View.TapSquareComponent.Direction direction, int column, int row)> action) => _moveEventPublisher.Subscribe(action);

        public IEnumerable Initialize()
        {
            yield return null;
        }

        public void ChangePieces((View.TapSquareComponent.Direction direction, int column, int row) directionAndCoordinate)
        {
            //ピースの変更
            _moveEventPublisher.Publish(directionAndCoordinate);
        }

        public void Result()
        {
            PuzzleSphere[] results = new PuzzleSphere[4];
            //結果をなんかいろいろして詰める．
            _resultEventPublisher.Publish(results);
        }

        public void Reset()
        {
            //ピースの配列内をリセットする．
        }
    }
}
