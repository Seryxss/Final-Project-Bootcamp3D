using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    float MaxTime;

    public GameObject PanelGameOver;
    public bool isTimerRunning = false;

    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = MaxTime;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // hidupkan kembali waktu
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
        UpdateTimerDisplay();
    }

    public void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public float GetElapsedTime()
    {
        return currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning == true && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime < 0)
        {
            currentTime = 0;
            isTimerRunning = false;
            GameOver();
        }
        UpdateTimerDisplay();
        // Debug.Log("time : " + currentTime);
    }

    void GameOver()
    {   
        // FindObjectOfType<GameManager>().GameOver("Waktu habis..."); 
        FindObjectOfType<PlayerHealth>().GameOver("Waktu habis...");
        currentTime = 0;
    }
}
