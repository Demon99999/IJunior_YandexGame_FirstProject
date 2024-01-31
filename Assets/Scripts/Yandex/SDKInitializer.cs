using System.Collections;
using Agava.YandexGames;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YandexSDK
{
    public sealed class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize(OnInitialized);
#endif
            yield return null;
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene(ScenesNames.Start);
        }
    }
}