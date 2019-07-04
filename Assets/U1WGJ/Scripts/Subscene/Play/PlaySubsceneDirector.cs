using Puzzle;
using System.Collections;
using System.Collections.Generic;
using Tower;
using UnityEngine;

namespace Play
{
    public class PlaySubsceneDirector : MonoBehaviour
    {
        [SerializeField]
        PuzzleDirector _puzzleDirector;
        [SerializeField]
        TowerDirector _towerDirector;

        public IEnumerable<PlayResult?> Run()
        {
            foreach (var _ in _puzzleDirector.Initialize())
            {
                yield return null;
            }
            StartCoroutine(_towerDirector.Run());

            PlayResult? result = null;
            foreach (var element in _run())
            {
                result = element;
                yield return null;
            }
            yield return result;
        }

        IEnumerable<PlayResult?> _run()
        {
            PlayResult? playResult = null;
            PuzzleSphere[] puzzleResult = null;

            /*TowerDirectorから時間とスコアをもらう
            using (_towerDirector.Subscribe((time, score) =>
            {
                playResult = new PlayResult(time, score);
            }))*/
            {
                while (!playResult.HasValue)
                {
                    foreach (var element in _puzzleDirector.Run(() => true))
                    {
                        puzzleResult = element;
                        yield return null;
                    }
                    foreach(var spher in puzzleResult)
                    {
                        _towerDirector.Shoot(spher.Color,spher.Lane);
                    }
                }
            }
            yield return playResult;
        }
    }
}
