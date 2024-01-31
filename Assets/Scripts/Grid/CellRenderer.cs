using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(MeshRenderer))]
    public class CellRenderer : MonoBehaviour
    {
        public void Hide()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        public void Show()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}