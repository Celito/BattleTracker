using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase
{
    public Phase previousPhase;
    public Phase nextPhase;
    public string name;

    public event Action onDone;

    public PhaseAction nextAction
    {
        get
        {
            // TODO: Define how to select next aciton;
            return _acitons[0];
        }
    }

    private List<PhaseAction> _acitons = new List<PhaseAction>();

    public void AddAction(PhaseAction action)
    {
        _acitons.Add(action);
    }
}
