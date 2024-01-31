using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TutorialPannel : MonoBehaviour
    {
        private const string Level = "Level";

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _completeButton;
        [SerializeField] private GameObject _firstSlide;
        [SerializeField] private GameObject _secondSlide;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnNextSlide);
            _completeButton.onClick.AddListener(OnTutorialFinish);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnNextSlide);
            _completeButton.onClick.RemoveListener(OnTutorialFinish);
        }

        private void Start()
        {
            int level = PlayerPrefs.GetInt(Level);

            if (level == 1)
            {
                Time.timeScale = 0;
                _firstSlide.SetActive(true);
            }
        }

        private void OnNextSlide()
        {
            _firstSlide.SetActive(false);
            _secondSlide.SetActive(true);
        }

        private void OnTutorialFinish()
        {
            Time.timeScale = 1;
            _secondSlide.SetActive(false);
        }
    }
}