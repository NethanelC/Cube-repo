using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action Obstacled;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacled?.Invoke();
    }
}
