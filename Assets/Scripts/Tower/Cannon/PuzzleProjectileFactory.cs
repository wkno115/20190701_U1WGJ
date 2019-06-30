using Puzzle;
using UnityEngine;

namespace Tower.Cannon
{
    public class PuzzleProjectileFactory
    {
        PuzzleProjectileViewContainer _puzzleProjectileViewContainer;

        public PuzzleProjectileFactory(PuzzleProjectileViewContainer puzzleProjectileViewContainer)
        {
            _puzzleProjectileViewContainer = puzzleProjectileViewContainer;
        }

        public PuzzleProjectileView CreatePuzzleProjectile(PieceColor pieceColor)
        {
            var originalPuzzleProjectile = _puzzleProjectileViewContainer.GetProjectileViewFromColor(pieceColor);
            var createdPuzzleProjectile = Object.Instantiate(originalPuzzleProjectile);

            return createdPuzzleProjectile;
        }
    }
}
