using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsEmployed { get; private set; } = false;

    public void ChangeValue()
    {
        IsEmployed = !IsEmployed;
    }

    public void Hide()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void Show()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        IsEmployed = false;
    }
}
