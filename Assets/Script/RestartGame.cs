using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RestartScene()
    {
        // Ambil nama scene aktif sekarang
        string currentScene = SceneManager.GetActiveScene().name;

        // Muat ulang scene tersebut
        SceneManager.LoadScene(currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
