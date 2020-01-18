using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseAction
{
    public bool required = false;

    public event Action onDone;

    protected bool _isDone = false;

    public bool IsDone()
    {
        return _isDone;
    }

    protected void Done()
    {
        _isDone = true;
        onDone?.Invoke();
    }
}
