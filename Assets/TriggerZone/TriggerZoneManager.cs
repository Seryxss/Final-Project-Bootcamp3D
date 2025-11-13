using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    private List<GameObject> _triggerZones = new List<GameObject>();
    public Transform arrowTransform; // The arrow GameObject
    public float rotationOffsetY = -45f; // Y-axis offset only

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
            if (arrowTransform != null) {
                arrowTransform.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (_triggerZones.Count > 0 && _triggerZones[0] != null && arrowTransform != null)
        {
            PointArrowToCurrentTarget();
        }
    }

    private void PointArrowToCurrentTarget()
    {
        GameObject currentTarget = _triggerZones[0];
        
        if (currentTarget == null) return;

        Vector3 relativePos = currentTarget.transform.position - arrowTransform.position;
        relativePos.y = 0;
        
        if (relativePos != Vector3.zero)
        {
            float angle = Mathf.Atan2(relativePos.x, relativePos.z) * Mathf.Rad2Deg;
            arrowTransform.rotation = Quaternion.Euler(0, angle + rotationOffsetY, 0);
        }
    }
}