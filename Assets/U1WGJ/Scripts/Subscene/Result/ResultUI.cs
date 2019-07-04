using Play;
using System;
using System.Collections;
using System.Collections.Generic;
using Title.View;
using TMPro;
using UnityEngine;

namespace Result.View
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField]
        StandardButton _finishButton;
        [SerializeField]
        TextMeshProUGUI _scoreText;
        [SerializeField]
        TextMeshProUGUI _rankText;

        public IDisposable SubscribeFinishTap(Action onTap) => _finishButton.SubscribeLeftClick(_ => onTap());

        public IEnumerable Initialize(float score, PlayResultRank rank)
        {
            _scoreText.text = score.ToString();
            switch (rank)
            {
                case PlayResultRank.S:
                    _rankText.text = "S";
                    break;
                case PlayResultRank.A:
                    _rankText.text = "A";
                    break;
                case PlayResultRank.B:
                    _rankText.text = "B";
                    break;
                case PlayResultRank.C:
                    _rankText.text = "C";
                    break;
            }
            yield return null;
        }
    }
}
