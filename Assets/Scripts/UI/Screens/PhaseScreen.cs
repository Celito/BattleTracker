using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _name;

    private Phase _phase;
    private BattleManager _manager;

    public void Initialize(BattleManager manager, Phase phase)
    {
        _phase = phase;

        _name.text = _phase.name;
        gameObject.name = _phase.name.Replace(' ', '_') + "PhaseScreen";
        
    }
}
