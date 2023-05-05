using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpy : MonoBehaviour
{
    public static event Action Jumpied;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jumpied?.Invoke();
    }
}
