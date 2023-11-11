using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
{
    private int _sceneIndex = 2;

    public int SceneIndex => _sceneIndex;

    public void NextScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void ShowScene()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void InitScene(int sceneIndex)
    {
        _sceneIndex = sceneIndex;
    }
}
