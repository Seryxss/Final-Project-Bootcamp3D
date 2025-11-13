using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI GameOverText;
    
    public void RestartGame()
    {
        Debug.Log("RestartGame() Terpanggil");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<GameManager>().RestartGame();
    }

    public void QuitGame()
    {
        Debug.Log("Game quit!"); // Bisa nanti kamu ganti dengan Application.Quit()
        Application.Quit();
    }

    public void setGameClear()
  {
    GameOverText.text = "~CLEAR~";
  }
    
}
