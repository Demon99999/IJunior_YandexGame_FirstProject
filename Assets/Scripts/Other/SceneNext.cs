using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class SceneNext : MonoBehaviour
    {
        private int _sceneIndexMax = 5;
        private int _sceneIndexMin = 2;
        private int _sceneIndex = 2;

        public int SceneIndex => _sceneIndex;

        public void ShowScene()
        {
            if (_sceneIndex < _sceneIndexMax)
            {
                _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            }
            else
            {
                _sceneIndex = _sceneIndexMin;
            }
        }

        public void InitScene(int sceneIndex)
        {
            _sceneIndex = sceneIndex;
        }

        public void OpenScene()
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}