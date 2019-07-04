using Pyke;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Puzzle.View
{
    /// <summary>
    /// タップするマスのコンポーネント
    /// </summary>
    public class TapSquareComponent : AbstractUIMonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// タップされた方向
        /// </summary>
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
        }

        /// <summary>
        /// タップイベント発行者
        /// </summary>
        readonly EventPublisher<Direction, byte, byte> _tapEventPublisher = new EventPublisher<Direction, byte, byte>();

        public byte Column { get; private set; }
        public byte Row { get; private set; }

        /// <summary>
        /// タップを購読する
        /// </summary>
        /// <param name="onTap">紐づけ処理</param>
        /// <returns></returns>
        public IDisposable SubscribeTap(Action<Direction, byte, byte> onTap) => _tapEventPublisher.Subscribe(onTap);

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void Initialize(byte column, byte row)
        {
            Column = column;
            Row = row;
        }


        /// <summary>
        /// クリック時コールバック
        /// </summary>
        /// <param name="eventData">イベントデータ</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            var position = transform.InverseTransformDirection(eventData.pressPosition);
            position -= _rectTransform.position;
            Direction? direction = null;

            //差分座標から，方向を決定．ど真ん中の時は方向を発行しない．
            if (Mathf.Abs(position.x) > Mathf.Abs(position.y))
            {
                if (Mathf.Sign(position.x) > 0)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Left;
                }
            }
            else if (Mathf.Abs(position.y) > Mathf.Abs(position.x))
            {
                if (Mathf.Sign(position.y) > 0)
                {
                    direction = Direction.Up;
                }
                else
                {
                    direction = Direction.Down;
                }
            }

            if (direction.HasValue)
            {
                _tapEventPublisher.Publish(direction.Value, Column, Row);
            }
        }
    }
}
