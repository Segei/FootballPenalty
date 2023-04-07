using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameLogic
{
    public class ScoreControll : MonoBehaviour
    {
        [SerializeField] private ViewScore view;
        private int bestScore;
        private int score  = 0;
        public UnityEvent OnNewBestScore;



        private void Start()
        {
            bestScore = PlayerPrefs.GetInt("BestScore", 0);
        }

        public void Goal()
        {
            score++;
        }

        public void SaveScore()
        {
            view.ShowScore(score, bestScore);
            if (score <= bestScore)
            {
                return;
            }
            OnNewBestScore?.Invoke();
            PlayerPrefs.SetInt("BestScore", score);
        }

    }
}
