using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.View
{
    public class PuzzlePieceComponent : AbstractUIMonoBehaviour
    {
        [SerializeField]
        Image _image;

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
