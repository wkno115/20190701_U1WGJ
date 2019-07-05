using System.Collections;
using System.Linq;
using UnityEngine;

namespace Puzzle.View
{
    public class PuzzleSphereContainer : AbstractUIMonoBehaviour
    {
        [SerializeField, Range(0, 10)]
        float _scaleTime = 1;
        [SerializeField]
        float _moveTime = 1;
        [SerializeField]
        PuzzleSphereComponent _puzzleSpherePrefab;
        [SerializeField]
        Transform _startPositionTransform;
        [SerializeField]
        Transform[] _cannonPositions;

        PuzzleSphereComponent[,] _spheres;


        public IEnumerable Initialize(PieceColor[,] pieces)
        {
            _spheres = new PuzzleSphereComponent[pieces.GetLength(0), pieces.GetLength(1)];
            foreach (var _ in _instantiate((byte)_spheres.GetLength(0), (byte)_spheres.GetLength(1)))
            {
                yield return null;
            }
            _setColor(pieces);
        }
        public IEnumerable EffectAnimation(PieceColor[,] pieces)
        {
            //スケール変更
            var startTime = Time.timeSinceLevelLoad;
            while (true)
            {
                var diff = Time.timeSinceLevelLoad - startTime;
                var rate = diff / _scaleTime;
                var scale = new Vector3(rate, rate, rate);
                foreach (var sphere in _spheres)
                {
                    sphere.Transform.localScale = scale;
                }
                if (diff > _scaleTime)
                {
                    foreach (var sphere in _spheres)
                    {
                        sphere.Transform.localScale = Vector3.one;
                    }
                    break;
                }
                yield return null;
            }

            //移動
            //TODO:とてもまともとは思えないループをしているので，変えたい
            var columns = _spheres.GetLength(0);
            var rows = _spheres.GetLength(1);
            var movingFlags = new bool[columns * rows];
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    _spheres[column, row].Move(_cannonPositions[column].localPosition, 1f + 0.03f * RandomValueFactory.CreateRandomValue(0, 10));
                    movingFlags[column * rows + row] = _spheres[column, row].IsMoving;
                }
            }
            //全ての球が動き終わったら
            while (movingFlags.Contains(true))
            {
                for (var column = 0; column < columns; column++)
                {
                    for (var row = 0; row < rows; row++)
                    {
                        movingFlags[column * rows + row] = _spheres[column, row].IsMoving;
                    }
                }
                yield return null;
            }
            var initialScale = new Vector3(0, 0, 0);
            foreach (var sphere in _spheres)
            {
                sphere.Transform.localScale = initialScale;
                yield return null;
            }
            _resetPosition();
            _setColor(pieces);
        }

        /// <summary>
        /// ポジションをリセット
        /// </summary>
        void _resetPosition()
        {
            //TODO:最適化
            var columns = _spheres.GetLength(0);
            var rows = _spheres.GetLength(1);

            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    _spheres[column, row].Transform.localPosition = _startPositionTransform.localPosition + new Vector3(column * _spheres[column, row].GetWidth(), row * -_spheres[column, row].GetHeight());
                }
            }
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
                    _spheres[column, row].SetColor(pieces[column, row]);
                }
            }
        }
        IEnumerable _instantiate(byte columns, byte rows)
        {
            PuzzleSphereComponent sphere;
            var initialScale = new Vector3(0, 0, 0);
            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    sphere = Instantiate(_puzzleSpherePrefab, Transform);
                    sphere.Transform.localPosition = _startPositionTransform.localPosition + new Vector3(column * sphere.GetWidth(), row * -sphere.GetHeight());
                    sphere.Transform.localScale = initialScale;
                    _spheres[column, row] = sphere;
                }
                yield return null;
            }
        }
    }
}
