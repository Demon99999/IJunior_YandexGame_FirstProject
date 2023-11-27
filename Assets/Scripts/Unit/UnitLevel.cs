using TMPro;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpText;

    private UnitCard _unitCard;

    private void Start()
    {
        _unitCard = GetComponent<Unit>().Card;
        _tmpText.text =_unitCard.Grade.ToString();
    }

    public void HideLevel()
    {
        _tmpText.text = null;
    }
}
