using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.View
{
    /// <summary>
    /// タップマスコンテナ
    /// </summary>
    public class TapSquareContainer : MonoBehaviour
    {
        /// <summary>
        /// 開始位置 Transform
        /// </summary>
        [SerializeField]
        Transform _startPositionTransform;
        /// <summary>
        /// マス Prefab
        /// </summary>
        [SerializeField]
        TapSquareComponent _squarePrefab;

        readonly EventPublisher<(TapSquareComponent.Direction direction, byte column, byte row)> _tapEventPublisher = new EventPublisher<(TapSquareComponent.Direction direction, byte column, byte row)>();

        /// <summary>
        /// 自 Tranform
        /// </summary>
        Transform _transform;
        /// <summary>
        /// 全マス
        /// </summary>
        TapSquareComponent[] _squares = new TapSquareComponent[4];
        /// <summary>
        /// まとめて Dispose
        /// </summary>
        CompositeDisposable _disposables = new CompositeDisposable();

        /// <summary>
        /// くそ雑有効状態
        /// </summary>
        public bool IsActive = true;

        /// <summary>
        /// タップを購読
        /// </summary>
        /// <param name="onTap"></param>
        /// <returns></returns>
        public IDisposable SubscribeTap(Action<(TapSquareComponent.Direction direction, byte column, byte row)> onTap) => _tapEventPublisher.Subscribe(onTap);

        private void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// マスをインスタンス化．
        /// </summary>
        /// <returns>処理中</returns>
        public IEnumerable InstantiateSquares(byte columns, byte rows)
        {
            _disposables.Dispose();
            TapSquareComponent square;
            for (byte column = 0; column < columns; column++)
            {
                for (byte row = 0; row < rows; row++)
                {
                    square = Instantiate(_squarePrefab, _transform);
                    square.Initialize(column, row);
                    square.transform.localPosition = _startPositionTransform.localPosition + new Vector3(row * square.GetWidth(), column * -square.GetHeight());
                    _disposables.Add(square.SubscribeTap(direction =>
                    {
                        if (IsActive)
                        {
                            _tapEventPublisher.Publish((direction, square.Column, square.Row));
                        }
                    }));
                }
                yield return null;
            }
        }

        void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}
