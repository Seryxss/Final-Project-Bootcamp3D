using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    public GameObject gameTimeLeft;

    [Header("Game Over")]
    public AudioSource gameOverSound;

    void Start()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void GameOver(string deathCause)
    {
        isGameOver = true;
        Time.timeScale = 0f;

        //Tampilkan UI Game Over
        if (gameLoseUI != null)
            gameLoseUI.SetActive(true);
        FindObjectOfType<PlayerBonusTime>().setDeadCause(deathCause);
        //Lepas kursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // mainkan suara game over
        if (gameOverSound != null)
            gameOverSound.Play();

        FindObjectOfType<SoundManager>().stopBackgroundMusic();
    }

    public void GameClear()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<SoundManager>().stopBackgroundMusic();

        var timer = FindObjectOfType<Timer>();
        float timeLeft = timer != null ? timer.GetElapsedTime() : 0f;
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        string formatted = string.Format("Time Remaining : {0:00}:{1:00}", minutes, seconds);

        if (gameTimeLeft != null)
        {
            var tmp = gameTimeLeft.GetComponent<TextMeshProUGUI>();
            if (tmp != null) tmp.text = formatted;
            else
            {
                var uiText = gameTimeLeft.GetComponent<Text>();
                if (uiText != null) uiText.text = formatted;
            }
        }

        FindObjectOfType<PlayerBonusTime>().setTextClear("Stage Clear!");
        SoundManager.PlaySound(SoundType.Clear, 0.7f);
        if (gameWinUI != null)
        {
            gameWinUI.SetActive(true);
            var gw = gameWinUI.GetComponent<GameWin>();
            if (gw == null) gw = FindObjectOfType<GameWin>();
            if (gw != null) 
            {
                int starsEarned = gw.Evaluate();
                string levelKey = "Stars_" + SceneManager.GetActiveScene().name;
                int prevStars = PlayerPrefs.GetInt(levelKey, 0);
                if (starsEarned > prevStars)
                {
                    PlayerPrefs.SetInt(levelKey, starsEarned);
                    PlayerPrefs.Save();
                }
            }
        }

        FindObjectOfType<Timer>().StopTimer();
        FindObjectOfType<GameOverUI>().setGameClear();
    }

    public void RestartGame()
    {
        Debug.Log("Restart ditekan");
        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f); // tunggu sebentar (supaya UI sempat respon)
        Time.timeScale = 1f;
        isGameOver = false;

        // Kunci kursor lagi setelah restart
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BacktoStartMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Back to StartMenu!");
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Game quit!");
        Application.Quit();
    }
}