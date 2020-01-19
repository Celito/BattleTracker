using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitNameText;
    [SerializeField] private Image _unitImage;

    void Start()
    {
        
    }

    public void Initialize(Unit unitData)
    {
        _unitNameText.text = unitData.name;

        _unitImage.sprite = Resources.Load<Sprite>("Images/Units/" + unitData.entryUnitId);

        //TODO: Add the unit image(s)

        //TODO: Temporarly add the unit information about its movement (the
        // unit information wiil be shown according to the phase)

    }
}
