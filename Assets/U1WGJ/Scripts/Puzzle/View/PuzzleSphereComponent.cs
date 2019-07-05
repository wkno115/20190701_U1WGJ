using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Puzzle.View
{
    public class PuzzleSphereComponent : AbstractUIMonoBehaviour
    {
        [SerializeField]
        Image _image;

        public bool IsMoving;

        public void Move(Vector3 endPosition, float duration)
        {
            IsMoving = true;
            Transform.DOLocalMove(endPosition, duration).SetEase(Ease.InBack).OnComplete(() => IsMoving = false);
            Transform.DOScale(0, duration).SetEase(Ease.InQuint);
        }
        public void SetColor(PieceColor color)
        {
            switch (color)
            {
                case PieceColor.Red:
                    _image.color = new Color(1, 0, 0);
                    break;
                case PieceColor.Green:
                    _image.color = new Color(0, 1, 0);
                    break;
                case PieceColor.Blue:
                    _image.color = new Color(0, 0, 1);
                    break;
                case PieceColor.Yellow:
                    _image.color = new Color(1, 1, 0);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
