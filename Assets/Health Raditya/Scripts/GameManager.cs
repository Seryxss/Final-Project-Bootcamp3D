using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public GameObject gameOverUI;
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
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        FindObjectOfType<PlayerBonusTime>().setPermanentText(deathCause);
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
        FindObjectOfType<PlayerBonusTime>().setPermanentText("Stage Clear!");
        SoundManager.PlaySound(SoundType.Clear, 0.7f);
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
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

    public void QuitGame()
    {
        Debug.Log("Game quit!");
        Application.Quit();
    }
}