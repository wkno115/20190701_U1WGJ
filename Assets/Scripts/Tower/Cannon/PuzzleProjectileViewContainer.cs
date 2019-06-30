using Puzzle;
using System.Linq;
using UnityEngine;

namespace Tower.Cannon
{
    public class PuzzleProjectileViewContainer : MonoBehaviour
    {
        [SerializeField]
        PuzzleProjectileView[] _puzzleProjectileViews;

        public PuzzleProjectileView GetProjectileViewFromColor(PieceColor pieceColor)
        {
            var projectileView = _puzzleProjectileViews
                .Where(view => view.PieceColor == pieceColor)
                .FirstOrDefault();
            if (projectileView == null)
            {
                throw new System.ArgumentOutOfRangeException($"{pieceColor} isnt contained.");
            }
            return projectileView;
        }
    }
}
