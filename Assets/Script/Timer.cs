using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float MaxTime;
    [SerializeField] float MaxTimeTargetB;

    public GameObject BoxA;
    public GameObject BoxB;
    public int A;
    public GameObject PanelGameOver;
    public GameObject player;
    private bool isGameOver = false;
    public bool isTimerJalan = false;



    public float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = MaxTime;
        List<int> numbers = new List<int>();

        // Isi list dengan angka 1 sampai 10
        for (int i = 1; i <= 3; i++)
            numbers.Add(i);
        int hasilrnd;
        hasilrnd = Random.Range(1,3);
        Debug.Log("HASIL RND : " + hasilrnd);

        if (hasilrnd == 1)
        {
            BoxA.transform.position = new Vector3(48f, 62f, -60f);

        }else if (hasilrnd == 2)
        {
            BoxA.transform.position = new Vector3(48f, 62f, 0f);

        }else if (hasilrnd == 3)
        {
            BoxA.transform.position = new Vector3(80f, 62f, -60f);
        }


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
