using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    [SerializeField] public GameObject triggerZonePrefab;
    public int countLocation = 3;

    private GameObject getRandomRecipient()
    {
        RecipientLocationScript[] recLocs = FindObjectsOfType<RecipientLocationScript>();
        Debug.Log("TotalRandomLocation: " + recLocs.Length + " recipient location");
        int randomNumber = Random.Range(0, recLocs.Length);
        return recLocs[randomNumber].gameObject;
        
    }

    public List<GameObject> spawnRandomTriggerZone()
    {
        List<GameObject> _triggerZones = new List<GameObject>();
        for (int i = 0; i < countLocation; i++)
        {
            
            // Instantiation at a specific position and rotation
            GameObject randomRecipientLocation = getRandomRecipient();
            Vector3 spawnPosition = randomRecipientLocation.transform.position;
            Debug.Log("randomRecipientLocation: " + spawnPosition + " ready to spawn");
            Quaternion spawnRotation = Quaternion.identity; // No rotation
            GameObject newObject = Instantiate(triggerZonePrefab, spawnPosition, spawnRotation); 
            // newObject
            Debug.Log("Created: #" + i + " recipient location");
            _triggerZones.Add(newObject);
        }
        return _triggerZones;
    }
}
