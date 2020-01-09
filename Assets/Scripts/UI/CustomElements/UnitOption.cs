using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitNameText;

    void Start()
    {
        
    }

    public void Initialize(Unit unitData)
    {
        _unitNameText.text = unitData.name;

        //TODO: Add the unit image(s)

        //TODO: Temporarly add the unit information about its movement (the
        // unit information wiil be shown according to the phase)

    }
}
