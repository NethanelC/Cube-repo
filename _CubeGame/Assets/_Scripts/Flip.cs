using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : Triggers
{
    public static event Action Flipped;
    protected override Action Triggered()
    {
        return Flipped;
    }
}
