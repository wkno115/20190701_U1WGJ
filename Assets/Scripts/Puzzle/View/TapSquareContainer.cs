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

        readonly EventPublisher<(TapSquareComponent.Direction direction, int column, int row)> _tapEventPublisher = new EventPublisher<(TapSquareComponent.Direction direction, int column, int row)>();

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
        /// タップを購読
        /// </summary>
        /// <param name="onTap"></param>
        /// <returns></returns>
        public IDisposable SubscribeTap(Action<(TapSquareComponent.Direction direction, int column, int row)> onTap) => _tapEventPublisher.Subscribe(onTap);

        private void Awake()
        {
            _transform = transform;
        }

        /// <summary>
        /// マスをインスタンス化．
        /// </summary>
        /// <returns></returns>
        public IEnumerable InstantiateSquares(int columns, int rows)
        {
            _disposables.Dispose();
            TapSquareComponent square;
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    square = Instantiate(_squarePrefab, _transform);
                    square.transform.localPosition = _startPositionTransform.localPosition + new Vector3(row * square.GetWidth(), column * -square.GetHeight());
                    _disposables.Add(square.SubscribeTap(direction => _tapEventPublisher.Publish((direction, column, row))));
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
