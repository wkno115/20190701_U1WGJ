using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.View
{
    public class PuzzleSphereContainer : AbstractUIMonoBehaviour
    {
        [SerializeField]
        PuzzleSphereComponent _puzzleSpherePrefab;
        [SerializeField]
        Transform _startPositionTransform;

        PuzzleSphereComponent[,] _spheres;


        IEnumerable Initialize(PieceColor[,] pieces)
        {
            _spheres = new PuzzleSphereComponent[pieces.GetLength(0) + 2, pieces.GetLength(1) + 2];
            foreach (var _ in _instantiate((byte)_spheres.GetLength(0), (byte)_spheres.GetLength(1)))
            {
                yield return null;
            }
            _setColor(pieces);
        }
        public IEnumerable EffectAnimation()
        {
            yield return null;
        }

        void _setColor(PieceColor[,] pieces)
        {
            var columns = pieces.GetLength(0);
            var rows = pieces.GetLength(1);

            //見える部分のピースの色を変える
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    _spheres[column + 1, row + 1].SetColor(pieces[column, row]);
                }
            }
        }
        IEnumerable _instantiate(byte columns, byte rows)
        {
            PuzzleSphereComponent sphere;
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    sphere = Instantiate(_puzzleSpherePrefab, Transform);
                    sphere.Transform.localPosition = _startPositionTransform.localPosition + new Vector3(column * 80, row * -80);
                    _spheres[column, row] = sphere;
                }
                yield return null;
            }
        }
    }
}
