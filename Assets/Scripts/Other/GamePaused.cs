using Agava.WebUtility;
using UnityEngine;

namespace GameLogic
{
    public class GamePaused : MonoBehaviour
    {
        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                ChangeState(true, 0);
            }
            else
            {
                ChangeState(false, 1);
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                ChangeState(true, 0);
            }
            else
            {
                ChangeState(false, 1);
            }
        }

        private void ChangeState(bool isValue, int value)
        {
            Time.timeScale = value;
            AudioListener.pause = isValue;
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            if (inBackground)
            {
                ChangeState(true, 0);
            }
            else
            {
                ChangeState(false, 1);
            }
        }
    }
}