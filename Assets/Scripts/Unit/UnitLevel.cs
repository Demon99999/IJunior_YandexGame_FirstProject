using TMPro;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitLevel : MonoBehaviour
{
    private const string Level = "LVL";

    [SerializeField] private TMP_Text _tmpText;

    private UnitCard _unitCard;

    private void Start()
    {
        _unitCard = GetComponent<Unit>().Card;
        _tmpText.text =Level + _unitCard.Grade.ToString();
    }

    public void HideLevel()
    {
        _tmpText.text = null;
    }
}
