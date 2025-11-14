using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private FadeCanvas fade;
    private AsyncOperation loadOp;

    // Tampilkan start panel
    void Start()
    {
        //PlayerPrefs.DeleteAll(); //=> Kalau Mau hapus data penyimpanan e.g stars
        fade.FadeOut();
    }

    public void ShowStart()
    {
        fade.FadeIn(() =>
        {
            startPanel.SetActive(true);
            levelPanel.SetActive(false);
            storyPanel.SetActive(false);
            creditPanel.SetActive(false);

            fade.FadeOut();
        });
    }

    // Tampilkan level select panel
    public void ShowLevel()
    {
        fade.FadeIn(() =>
        {
            startPanel.SetActive(false);
            levelPanel.SetActive(true);
            storyPanel.SetActive(false);
            creditPanel.SetActive(false);

            fade.FadeOut();
        });
    }

    // Tampilkan storyboard panel
    public void ShowStory()
    {
        fade.FadeIn(() =>
        {
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            storyPanel.SetActive(true);
            creditPanel.SetActive(false);

            fade.FadeOut();
        });
    }

    // Tampilkan credit panel
    public void ShowCredit()
    {
        fade.FadeIn(() =>
        {
            startPanel.SetActive(false);
            levelPanel.SetActive(false);
            storyPanel.SetActive(false);
            creditPanel.SetActive(true);

            fade.FadeOut();
        });
    }

    // Preload gameplay scene
    public void StartPreload()
    {
        loadOp = SceneManager.LoadSceneAsync("Gameplay");
        loadOp.allowSceneActivation = false;
    }

    // Aktifkan gameplay setelah storyboard selesai
    public void ActivateGameplay()
    {
        fade.FadeIn(() =>
        {
            loadOp.allowSceneActivation = true;
        });
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
