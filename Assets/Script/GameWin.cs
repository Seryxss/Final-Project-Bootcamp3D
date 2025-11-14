using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    [Header("Star UI")]
    public GameObject star1;  
    public GameObject star2Obj;
    public GameObject star3Obj; 

    [Header("Star Sprites")]
    public Sprite starOnSprite; 
    public Sprite starOffSprite;

    [Header("Text UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI twoStarThresholdText;
    public TextMeshProUGUI threeStarThresholdText;

    [Header("Thresholds")]
    public float twoStarThreshold;
    public float threeStarThreshold;

    void Start()
    {
        if (twoStarThresholdText != null)
        {
            int m = Mathf.FloorToInt(twoStarThreshold / 60f);
            int s = Mathf.FloorToInt(twoStarThreshold % 60f);
            twoStarThresholdText.text = string.Format("{0:00}:{1:00} < Time Remaining", m, s);
        }

        if (threeStarThresholdText != null)
        {
            int m = Mathf.FloorToInt(threeStarThreshold / 60f);
            int s = Mathf.FloorToInt(threeStarThreshold % 60f);
            threeStarThresholdText.text = string.Format("{0:00}:{1:00} < Time Remaining", m, s);
        }
    }

    public int Evaluate()
    {
        var timer = FindObjectOfType<Timer>();
        float timeLeft = timer != null ? timer.GetElapsedTime() : 0f;

        bool star2 = timeLeft > twoStarThreshold;
        bool star3 = timeLeft > threeStarThreshold;

        SetStarSprite(star1, true);
        SetStarSprite(star2Obj, star2);
        SetStarSprite(star3Obj, star3);

        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            timeText.text = string.Format("Time Remaining : {0:00}:{1:00}", minutes, seconds);
        }
        return 1 + (star2 ? 1 : 0) + (star3 ? 1 : 0);
    }

    void SetStarSprite(GameObject starObj, bool active)
    {
        if (starObj == null || starOnSprite == null || starOffSprite == null) return;

        // Try UI Image 
        var img = starObj.GetComponent<Image>();
        if (img != null)
        {
            img.sprite = active ? starOnSprite : starOffSprite;
            return;
        }

        // Try SpriteRenderer
        var sr = starObj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = active ? starOnSprite : starOffSprite;
            return;
        }
    }
}