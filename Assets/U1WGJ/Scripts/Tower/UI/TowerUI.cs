using TMPro;
using UnityEngine;

namespace Tower.UI
{
    public class TowerUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _timeText;
        [SerializeField]
        TextMeshProUGUI _scoreText;

        void Awake()
        {
            SetScore(0);
            SetTime(0);
        }

        public void SetTime(float time)
        {
            _timeText.text = $"Time{time.ToString("F0")}";
        }
        public void SetScore(int score)
        {
            _scoreText.text = $"Score{score}";
        }
    }
}

