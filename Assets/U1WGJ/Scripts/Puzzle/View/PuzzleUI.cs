using System;
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
        /// <summary>
        /// パズルスフィアコンテナ
        /// </summary>
        [SerializeField]
        PuzzleSphereContainer _puzzleSphereContainer;
        /// <summary>
        /// 入力を扱うオブジェクト
        /// </summary>
        [SerializeField]
        InputHandlerComponent _inputHandlerComponent;

        public TapSquareContainer GetTapSquareContainer() => _tapSquareContainer;
        public PuzzlePieceContainer GetPuzzleContainer() => _puzzlePieceContainer;

        /// <summary>
        /// キー入力を購読する．
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IDisposable SubscribeInputKey(Action<KeyCode> action) => _inputHandlerComponent.SubscribeInputKey(action);


        public IEnumerable Initialize(PieceColor[,] initialPieces)
        {
            foreach (var _ in _tapSquareContainer.InstantiateSquares((byte)initialPieces.GetLength(0), (byte)initialPieces.GetLength(1)))
            {
                yield return null;
            }
            foreach (var _ in _puzzlePieceContainer.Initialize(initialPieces))
            {
                yield return null;
            }
            foreach (var _ in _puzzleSphereContainer.Initialize(initialPieces))
            {
                yield return null;
            }
        }
        /// <summary>
        /// ピース更新処理
        /// </summary>
        /// <param name="directionAndCoordinate"></param>
        /// <returns></returns>
        public IEnumerable UpdatePiecesProcess((View.TapSquareComponent.Direction direction, byte column, byte row) directionAndCoordinate, PieceColor[,] nextPieces)
        {
            _setInputActive(false);
            foreach (var _ in _puzzlePieceContainer.UpdatePieces(directionAndCoordinate, nextPieces))
            {
                yield return null;
            }
            _setInputActive(true);
        }
        /// <summary>
        /// 結果処理
        /// </summary>
        /// <param name="nextPieces">次のピース群</param>
        /// <returns></returns>
        public IEnumerable ResultProcess(PieceColor[,] nextPieces)
        {
            foreach (var _ in _puzzleSphereContainer.EffectAnimation(nextPieces))
            {
                yield return null;
            }
            foreach (var _ in _puzzlePieceContainer.ResultProcess(nextPieces))
            {
                yield return null;
            }
        }

        /// <summary>
        /// 入力の有効化セット
        /// </summary>
        /// <param name="isActivate"></param>
        void _setInputActive(bool isActivate)
        {
            _inputHandlerComponent.IsActive = isActivate;
            _tapSquareContainer.IsActive = isActivate;
        }
    }
}
