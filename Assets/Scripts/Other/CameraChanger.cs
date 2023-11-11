using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    private int _currentIndex;

    public void OnSwitchCamera()
    {
        _virtualCameras[_currentIndex].gameObject.SetActive(false);

        _currentIndex++;
        
        if (_currentIndex >= _virtualCameras.Length)
        {
            _currentIndex = 0;
        }

        _virtualCameras[_currentIndex].gameObject.SetActive(true);
    }
}
