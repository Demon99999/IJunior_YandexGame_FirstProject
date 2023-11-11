using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Screen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _panel;

    private const string Open = "Open";
    private const string Close = "Close";

    private Animator _animator;

    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
    }

    public void OpenScreen()
    {
        _panel.blocksRaycasts = true;
        if (_animator != null)
            _animator.SetTrigger(Open);
        _panel.alpha = 1;
    }

    public void CloseScreen()
    {
        _panel.blocksRaycasts = false;
        if (_animator != null)
            _animator.SetTrigger(Close);
        _panel.alpha = 0;
    }
}
