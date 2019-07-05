using Play.View;
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
        [SerializeField]
        PlayUI _ui;

        public IEnumerable<PlayResult?> Run()
        {
            foreach (var _ in _puzzleDirector.Initialize())
            {
                yield return null;
            }
            StartCoroutine(_towerDirector.Run().GetEnumerator());

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

            //TowerDirectorから時間とスコアをもらう
            using (_towerDirector.SubscribePlayResult(result =>
            {
                playResult = result;
            }))
            using (_puzzleDirector.SubscribeResultEffectStart(() => _towerDirector.Pause(true)))
            {
                while (!playResult.HasValue)
                {
                    foreach (var element in _puzzleDirector.Run())
                    {
                        if (playResult.HasValue)
                        {
                            break;
                        }
                        puzzleResult = element;
                        yield return null;
                    }
                    if (puzzleResult != null)
                    {
                        foreach (var spher in puzzleResult)
                        {
                            _towerDirector.Shoot(spher.Color, spher.Lane);
                        }
                        _towerDirector.Pause(false);
                    }
                }
            }
            _ui.gameObject.SetActive(true);
            foreach(var _ in _ui.Run())
            {
                yield return null;
            }
            _ui.gameObject.SetActive(false);
            yield return playResult;
        }
    }
}
