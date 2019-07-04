using Play;
using Result.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Result
{
    public class ResultSubsceneDirector : MonoBehaviour
    {
        [SerializeField]
        ResultUI _ui;

        public IEnumerable Run(PlayResult? result)
        {
            if (result.HasValue)
            {
                foreach (var _ in _ui.Initialize(result.Value.DefeatScore, _getRank(result.Value)))
                {
                    yield return null;
                }
            }
            else
            {
                yield break;
            }
            var shouldContinue = true;
            _ui.gameObject.SetActive(true);
            using (_ui.SubscribeFinishTap(() => shouldContinue = false))
            {
                while (shouldContinue)
                {
                    yield return null;
                }
            }
            _ui.gameObject.SetActive(false);
        }

        PlayResultRank _getRank(PlayResult result)
        {
            if (result.DefeatScore >= 10000)
            {
                return PlayResultRank.S;
            }
            else if (result.DefeatScore >= 5000)
            {
                return PlayResultRank.A;
            }
            else if (result.DefeatScore >= 2500)
            {
                return PlayResultRank.B;
            }
            else
            {
                return PlayResultRank.C;
            }
        }
    }
}
