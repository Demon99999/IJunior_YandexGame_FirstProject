using UnityEngine;

namespace GameLogic
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private CellRenderer _cellRenderer;

        public bool IsEmployed { get; private set; } = false;

        public void ChangeValue()
        {
            IsEmployed = !IsEmployed;
        }

        public void Hide()
        {
            _cellRenderer.Hide();
        }

        public void Show()
        {
            _cellRenderer.Show();
            IsEmployed = false;
        }
    }
}