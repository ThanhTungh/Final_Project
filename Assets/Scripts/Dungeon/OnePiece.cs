using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePiece : MonoBehaviour
{
    public static event Action OnTouchEndGameEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTouchEndGameEvent?.Invoke();
        }
    }
}