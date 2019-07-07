using Puzzle.Model;
using Puzzle.View;
using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    /// <summary>
    /// パズルコントローラ
    /// </summary>
    public class PuzzleController
    {
        readonly PuzzleUI _ui;
        readonly PuzzleDomain _domain;
        readonly EventPublisher _resultAnimationStart = new EventPublisher();

        public PuzzleController(PuzzleUI ui, PuzzleDomain domain)
        {
            _ui = ui;
            _domain = domain;
        }

        public IDisposable SubscribeResultEffectStart(Action action) => _resultAnimationStart.Subscribe(action);

        public IEnumerable Initialize()
        {
            yield return null;
        }

        public IEnumerable<PuzzleSphere[]> Run()
        {
            IEnumerable animationProcess = null;
            PuzzleSphere[] resultSphere = null;

            using (_ui.GetTapSquareContainer().SubscribeTap(_domain.ChangePieces))
            using (_ui.SubscribeInputKey(_ => _domain.Result()))
            using (_domain.SubscribeUpdatePieces(directionAndCoordinate => animationProcess = _ui.UpdatePiecesProcess(directionAndCoordinate, _domain.GetAllPieces())))
            using (_domain.SubscribeResult(result =>
            {
                animationProcess = _ui.ResultProcess(_domain.GetAllPieces());
                _resultAnimationStart.Publish();
                resultSphere = result.GetResultSpheres();
                _domain.Reset();
            }))
            {
                while (true)
                {
                    if (animationProcess != null)
                    {
                        foreach (var _ in animationProcess)
                        {
                            yield return null;
                        }
                        animationProcess = null;
                        if (resultSphere != null)
                        {
                            break;
                        }
                    }
                    yield return null;
                }
                yield return resultSphere;
            }
        }
    }
}
