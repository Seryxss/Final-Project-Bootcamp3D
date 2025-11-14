// ...existing code...
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    [Header("Text UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI twoStarThresholdText;
    public TextMeshProUGUI threeStarThresholdText;

    [Header("Reference")]
    public GameWin gameWin;
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();

        if (gameWin != null)
        {
            if (twoStarThresholdText != null)
                twoStarThresholdText.text = string.Format("{0} < Time Remaining", (int)gameWin.twoStarThreshold);

            if (threeStarThresholdText != null)
                threeStarThresholdText.text = string.Format("{0} < Time Remaining", (int)gameWin.threeStarThreshold);
        }
    }

    void UpdateTimeDisplay()
    {
        if (timeText == null) return;

        float current = (timer != null) ? timer.currentTime : 0f;

        int m = Mathf.FloorToInt(current / 60f);
        int s = Mathf.FloorToInt(current % 60f);
        timeText.text = string.Format("Time Remaining : {0:00}:{1:00}", m, s);
    }

    public void TogglePause()
    {
        if (GameManager.isGameOver) return; // block pause after win/lose

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(isPaused);

        if (isPaused)
            UpdateTimeDisplay();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
    }
}