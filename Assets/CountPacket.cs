using UnityEngine;
using TMPro;

public class TriggerUICounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private TriggerZoneManager triggerZoneManager;
    private ObjectiveManager objectiveManager;

    void Start()
    {
        // TriggerZoneManager ada di Player
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            triggerZoneManager = player.GetComponent<TriggerZoneManager>();
        }

        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    void Update()
    {
        if (counterText == null) return;
        if (triggerZoneManager == null || objectiveManager == null) return;

        int total = objectiveManager.countLocation;
        int complete = triggerZoneManager.completedCount;

        counterText.text = complete + " / " + total;
    }
}
