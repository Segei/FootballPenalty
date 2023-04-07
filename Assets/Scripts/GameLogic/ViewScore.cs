using TMPro;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class ViewScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreField, bestScoreField;

        public void ShowScore(int score, int bestScore)
        {
            scoreField.text = score.ToString();
            bestScoreField.text = bestScore.ToString();
        }
    }
}
