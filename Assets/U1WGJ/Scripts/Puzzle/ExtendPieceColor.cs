using System;
using UnityEngine;

namespace Puzzle
{
    public static class ExtendPieceColor
    {
        public static Color ToColor(this PieceColor pieceColor)
        {
            switch (pieceColor)
            {
                case PieceColor.Red:
                    return Color.red;
                case PieceColor.Green:
                    return Color.green;
                case PieceColor.Blue:
                    return Color.blue;
                case PieceColor.Yellow:
                    return Color.yellow;
                default:
                    throw new ArgumentOutOfRangeException($"{pieceColor} dosent set color.");
            }
        }
    }
}
