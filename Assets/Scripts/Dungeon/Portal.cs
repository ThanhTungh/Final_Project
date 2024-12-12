using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static event Action OnPortalEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPortalEvent?.Invoke();
        }
    }

    /* -------------------------------------------------------------------------------------------------------- */

}
