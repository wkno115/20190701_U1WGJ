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

        IEnumerator Start()
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
        }

        IEnumerable<PlayResult?> _run()
        {
            PlayResult? playResult = null;
            PuzzleSphere[] puzzleResult = null;

            using (_towerDirector.SubscribeFinish((time, score) =>
            {
                playResult = new PlayResult(time, score);
            }))
            {
                while (!playResult.HasValue)
                {
                    foreach (var element in _puzzleDirector.Run(() => true))
                    {
                        puzzleResult = element;
                        yield return null;
                    }
                    foreach (var _ in _towerDirector.MultiShoot(puzzleResult))
                    {
                        yield return null;
                    }
                }
            }
            yield return playResult;
        }
    }
}
