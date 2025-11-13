using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    private List<GameObject> _triggerZones = new List<GameObject>();
    public Transform arrowTransform; // The arrow GameObject
    public float rotationOffsetY = -45f; // Y-axis offset only
    private ObjectiveManager objectiveManager;
    private GameObject player;

    void Start()
    {   
        GameObject targetGameObject = GameObject.Find("objManager");
        if (targetGameObject != null)
        {
            objectiveManager = targetGameObject.GetComponent<ObjectiveManager>();
        }
        player = GameObject.Find("Player");
        InitTriggerZonesList();
    }

    private void InitTriggerZonesList()
    {
        _triggerZones.Clear();
        TriggerZone[] triggerZones = FindObjectsOfType<TriggerZone>();
        for (int i = 0; i < triggerZones.Length; i++) {
            triggerZones[i].OnTrigger += OnAreaTrigger;
            _triggerZones.Add(triggerZones[i].gameObject);
        }
        Debug.Log("Total Paket/Rumah: " + _triggerZones.Count);
    }

    private void OnAreaTrigger(TriggerZone triggerZone)
    {
        _triggerZones.Remove(triggerZone.gameObject);
        Destroy(triggerZone.gameObject);
        
        if (!triggerZone.isStart)
        {
            Debug.Log("Paket/Rumah Tersisa: " + _triggerZones.Count);
            if (_triggerZones.Count == 0 ) {
                Debug.Log("Semua Paket/Rumah Telah Selesai");
                if (arrowTransform != null) {
                    arrowTransform.gameObject.SetActive(false);
                }
            }
        } else {
            Debug.Log("Game dimulai!");
            // trigger zone isStart == true
            // spawn random triggerZone here
            List<GameObject> tempTriggerZone = objectiveManager.spawnRandomTriggerZone();
            foreach(GameObject tempObj in tempTriggerZone)
            {
              TriggerZone triggerZoneTemp = tempObj.GetComponent<TriggerZone>();
              triggerZoneTemp.OnTrigger += OnAreaTrigger;
              _triggerZones.Add(triggerZoneTemp.gameObject);
            }

            Debug.Log("Paket/Rumah Tersisa: " + _triggerZones.Count);
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
        GameObject currentTarget = null;
        foreach(GameObject currentLoopedobj in _triggerZones){
            // Debug.Log("Player location:" + player.transform.position);
            if (currentTarget == null) {
                currentTarget = currentLoopedobj;
                continue;
            }
            float oldDist = Vector3.Distance(player.transform.position, currentTarget.transform.position);
            float newDist = Vector3.Distance(player.transform.position, currentLoopedobj.transform.position);

            if(newDist < oldDist)
            {
              currentTarget = currentLoopedobj;
            }
        }
        
        
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