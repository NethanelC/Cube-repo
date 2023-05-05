using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedy : MonoBehaviour
{
    public static event Action Speedied;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Speedied?.Invoke();
    }
}
