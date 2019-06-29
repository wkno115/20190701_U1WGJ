using Puzzle.Model;
using Puzzle.View;
using Pyke;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
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
            //using(_domain.Su)
            using (_ui.GetTapSquareContainer().SubscribeTap(_domain.ChangePieces))
            {
                while (true)
                {
                    yield return null;
                }
            }
        }
    }
}
