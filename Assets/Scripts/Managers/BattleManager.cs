using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<PlayerManager> players = new List<PlayerManager>();

    [SerializeField] private GameObject _unitOptionMold;
    [SerializeField] private PhaseScreen _phaseScreenModel;
    [SerializeField] private Canvas _canvas;

    [SerializeField] private HorizontalSelector _tempModelSelector;

    void Start()
    {
        // TODO: Select first player
        // TEMP: This data should be load from a rules file;
        var initiativePhase = new Phase();
        initiativePhase.name = "Deployment";
        initiativePhase.AddAction(new SelectPlayerAction());

        // TODO: Create new phase screens on demand
        _phaseScreenModel.Initialize(this, initiativePhase);

        // TODO: Select units to be deployed 

        // TODO: Movement phase

        var units = players[0].lists[0].GetAllUnits();

        foreach (var unit in units)
        {
            GameObject unitOpt = Instantiate(_unitOptionMold);
            unitOpt.GetComponent<UnitOption>()?.Initialize(unit);

            _tempModelSelector.AddOption(unitOpt);
        }

        // TODO: After all the options have been added, update the options 
        // sizes
    }
}
