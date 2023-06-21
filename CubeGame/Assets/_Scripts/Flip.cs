using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : BaseTrigger
{
    protected override void Trigger() => _player.Flipped();
}
