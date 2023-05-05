using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy : Triggers
{
    public static event Action Flappied;
    protected override Action Triggered()
    {
        return Flappied;
    }
}
