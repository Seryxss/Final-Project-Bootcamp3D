using UnityEngine;
using UnityEngine.UI;

public class LevelStarsDisplay : MonoBehaviour
{
    [SerializeField] private string levelName = "Gameplay"; // your level scene name
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private Sprite starOnSprite;
    [SerializeField] private Sprite starOffSprite;

    void Start()
    {
        DisplayStars();
    }

    void DisplayStars()
    {
        int stars = PlayerPrefs.GetInt("Stars_" + levelName, 0);
        SetStarSprite(star1, stars >= 1);
        SetStarSprite(star2, stars >= 2);
        SetStarSprite(star3, stars >= 3);
    }

    void SetStarSprite(GameObject starObj, bool earned)
    {
        if (starObj == null) return;
        var img = starObj.GetComponent<Image>();
        if (img != null) img.sprite = earned ? starOnSprite : starOffSprite;
    }
}