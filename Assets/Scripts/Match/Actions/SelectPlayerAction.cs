using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerAction : PhaseAction
{
    private int _selectedPlayerId;

    public void SelectPlayer(int playerId)
    {
        _selectedPlayerId = playerId;
        _isDone = true;
    }
}
