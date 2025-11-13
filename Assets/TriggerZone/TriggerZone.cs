using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    // [SerializeField] private GameObject _textPick;
    public Action<TriggerZone> OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            if (OnTrigger != null)
            {
                OnTrigger.Invoke(this);
                SoundManager.PlaySound(SoundType.ReachingDestination, 0.7f);
            }
            //Debug.Log("Paket Berhasil Diserahkan");
            //// _textPick.SetActive(true);
            //Destroy(gameObject);
        }
    }
}
