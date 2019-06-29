using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Model
{
    public struct PuzzlePieceModel
    {
        public PieceColor Color { get; }
        public PuzzlePieceModel(PieceColor color)
        {
            Color = color;
        }
    }
}
