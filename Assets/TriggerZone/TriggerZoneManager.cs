using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    private List<GameObject> _triggerZones = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitTriggerZonesList();
    }

    private void InitTriggerZonesList()
    {
        TriggerZone[] triggerZones = FindObjectsOfType<TriggerZone>();
        for (int i = 0; i < triggerZones.Length; i++) {
            _triggerZones.Add(triggerZones[i].gameObject);
            triggerZones[i].OnTrigger += OnAreaTrigger;
        }
        Debug.Log("Total Paket/Rumah: " + _triggerZones.Count);
    }

    private void OnAreaTrigger(TriggerZone triggerZone)
    {
        _triggerZones.Remove(triggerZone.gameObject);
        Destroy(triggerZone.gameObject);
        Debug.Log("Paket/Rumah Tersisa: " + _triggerZones.Count);
        if (_triggerZones.Count == 0) {
            Debug.Log("Semua Paket/Rumah Telah Selesai");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
