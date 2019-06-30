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
        readonly EventPublisher<Direction> _tapEventPublisher = new EventPublisher<Direction>();

        public byte Column { get; private set; }
        public byte Row { get; private set; }

        /// <summary>
        /// タップを購読する
        /// </summary>
        /// <param name="onTap">紐づけ処理</param>
        /// <returns></returns>
        public IDisposable SubscribeTap(Action<Direction> onTap) => _tapEventPublisher.Subscribe(onTap);

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void Initialize(byte column,byte row)
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

            //差分座標から，方向を決定．ど真ん中の時は方向を発行しない．
            if (Mathf.Abs(position.x) > Mathf.Abs(position.y))
            {
                if (Mathf.Sign(position.x) > 0)
                {
                    _tapEventPublisher.Publish(Direction.Right);
                }
                else
                {
                    _tapEventPublisher.Publish(Direction.Left);
                }
            }
            else if (Mathf.Abs(position.y) > Mathf.Abs(position.x))
            {
                if (Mathf.Sign(position.y) > 0)
                {
                    _tapEventPublisher.Publish(Direction.Up);
                }
                else
                {
                    _tapEventPublisher.Publish(Direction.Down);
                }
            }
        }
    }
}
