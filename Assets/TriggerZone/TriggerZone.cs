using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public Action<TriggerZone> OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnTrigger != null)
            {
                OnTrigger.Invoke(this);
            }
        }
    }
}
