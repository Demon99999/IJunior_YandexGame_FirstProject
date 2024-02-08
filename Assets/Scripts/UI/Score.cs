using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        protected void OnScoreChanged(int value)
        {
            _score.text = value.ToString();
        }
    }
}