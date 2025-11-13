using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public Action<TriggerZone> OnTrigger;
    [SerializeField] public bool isStart;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered: " + other.gameObject.name);
        if (other.name == "Player" )
        {
            Debug.Log("OnTrigger: " + OnTrigger);
            if (OnTrigger != null)
            {
                OnTrigger.Invoke(this);
                SoundManager.PlaySound(SoundType.ReachingDestination, 0.7f);
            }
        }
    }
}
