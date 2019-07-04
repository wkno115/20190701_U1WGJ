using Puzzle;
using Pyke;
using UnityEngine;

namespace Tower.Cannon
{
    public class PuzzleProjectileView : ProjectileView
    {
        [SerializeField]
        PuzzleProjectileState _puzzleProjectileState;

        public PieceColor PieceColor => _puzzleProjectileState.PieceColor;
        public float AttackPower => _puzzleProjectileState.AttackPower;
    }
}
