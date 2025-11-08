using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float MaxTime;
    [SerializeField] float MaxTimeTargetB;

    public GameObject PanelGameOver;
    public GameObject player;
    private bool isGameOver = false;
    public bool isTimerJalan = false;



    public float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = MaxTime;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // hidupkan kembali waktu
    }

    public void AddTime(float amount)
    {
        elapsedTime += amount;
        UpdateTimerDisplay();
    }

    public void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SelesaiGame()
    {
        Time.timeScale = 0f;

    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerJalan == true && isGameOver == false && elapsedTime > 0)
        {
            elapsedTime = elapsedTime - Time.deltaTime;

        }
        else
        {
            if (elapsedTime < 0)
            {
                elapsedTime = 0;
                isGameOver = true;
            }
        }
        if (isGameOver == true)
        {
            PanelGameOver.SetActive(true);
            Time.timeScale = 0f;
            // player.getComponent<CharacterMovement>().enabled = false;
        }
        UpdateTimerDisplay();
        Debug.Log("Status Game Over : " + isGameOver);

    }
}
