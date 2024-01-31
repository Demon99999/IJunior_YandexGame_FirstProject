using TMPro;
using UnityEngine;

namespace UnitLogic
{
    [RequireComponent(typeof(Unit))]
    public class UnitLevel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _gradeText;

        private UnitCard _unitCard;

        private void Start()
        {
            ShowLevel();
        }

        public void HideLevel()
        {
            _levelText.text = null;
            _gradeText.text = null;
        }

        private void ShowLevel()
        {
            _unitCard = GetComponent<Unit>().Card;
            _gradeText.text = _unitCard.Grade.ToString();
        }
    }
}