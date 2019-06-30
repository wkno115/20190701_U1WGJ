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


        public PuzzleController(PuzzleUI ui, PuzzleDomain domain)
        {
            _ui = ui;
            _domain = domain;
        }

        public IDisposable SubscribeResult(Action<PuzzleSphere[]> action) => _domain.SubscribeResult(action);

        public IEnumerable Initialize()
        {
            yield return null;
        }

        public IEnumerable Run()
        {
            IEnumerable animationProcess = null;

            using (_ui.GetTapSquareContainer().SubscribeTap(_domain.ChangePieces))
            using (_ui.SubscribeInputKey(_ => _domain.Result()))
            using (_domain.SubscribeUpdatePieces(directionAndCoordinate => animationProcess = _ui.UpdatePiecesProcess(directionAndCoordinate, _domain.GetAllPieces())))
            {
                while (true)
                {
                    if (animationProcess != null)
                    {
                        foreach (var _ in animationProcess)
                        {
                            yield return null;
                        }
                    }
                    yield return null;
                }
            }
        }
    }
}
