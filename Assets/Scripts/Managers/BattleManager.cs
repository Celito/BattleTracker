using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<PlayerManager> players = new List<PlayerManager>();

    [SerializeField] private HorizontalSelector _tempModelSelector;

    void Start()
    {
        var units = players[0].lists[0].GetAllUnits();

        foreach (var unit in units)
        {
            _tempModelSelector.AddUnit(unit);
        }
    }
}
