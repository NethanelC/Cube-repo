using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flop : Triggers
{
    public static event Action Flopped;
    protected override Action Triggered()
    {
        return Flopped;
    }
}
