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
        readonly EventPublisher<(View.TapSquareComponent.Direction direction, byte column, byte row)> _updatePiecesEventPublisher = new EventPublisher<(View.TapSquareComponent.Direction direction, byte column, byte row)>();

        /// <summary>
        /// 全マス
        /// </summary>
        PuzzlePieceCollection _pieceCollection;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="rows">行</param>
        public PuzzleDomain(byte columns, byte rows)
        {
            _pieceCollection = new PuzzlePieceCollection(columns, rows);
        }

        /// <summary>
        /// 結果を購読する．
        /// </summary>
        /// <param name="action">紐づけ処理</param>
        /// <returns>購読解除</returns>
        public IDisposable SubscribeResult(Action<PuzzleSphere[]> action) => _resultEventPublisher.Subscribe(action);
        /// <summary>
        /// ピースの更新を購読する．
        /// </summary>
        /// <param name="action">紐づけ処理</param>
        /// <returns>購読解除</returns>
        public IDisposable SubscribeUpdatePieces(Action<(View.TapSquareComponent.Direction direction, byte column, byte row)> action) => _updatePiecesEventPublisher.Subscribe(action);
        /// <summary>
        /// 全ピースを取得
        /// </summary>
        /// <returns>全ピース</returns>
        public PieceColor[,] GetAllPieces() => _pieceCollection.GetPieces();

        public IEnumerable Initialize()
        {
            Reset();
            yield return null;
        }

        public void ChangePieces((View.TapSquareComponent.Direction direction, byte column, byte row) directionAndCoordinate)
        {
            _pieceCollection.ChangePieces(directionAndCoordinate);
            _updatePiecesEventPublisher.Publish(directionAndCoordinate);
        }
        public void Result()
        {
            _resultEventPublisher.Publish(_pieceCollection.GetResultSpheres());
        }
        public void Reset()
        {
            _pieceCollection.Reset();
        }
    }
}
