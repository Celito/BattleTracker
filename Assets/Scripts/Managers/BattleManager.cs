using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<PlayerManager> players = new List<PlayerManager>();

    [SerializeField] private GameObject _unitOptionMold;

    [SerializeField] private HorizontalSelector _tempModelSelector;

    void Start()
    {
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
