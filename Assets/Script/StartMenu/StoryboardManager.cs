using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryboardManager : MonoBehaviour
{
    [SerializeField]private CanvasGroup[] pages;      // semua halaman storyboard
    [SerializeField]private UIManager ui;             // referensi UIManager
    [SerializeField]private float duration = 0.5f;    // durasi fade
    [SerializeField]private GameObject nextButton;    // tombol play

    private int currentIndex = 0;

    void Start()
    {
        // Tampilkan halaman pertama, sembunyikan yang lain
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].alpha = (i == 0) ? 1f : 0f;
            pages[i].interactable = (i == 0);
            pages[i].blocksRaycasts = (i == 0);
        }

        // Mulai preload gameplay scene
        ui.StartPreload();
    }

    public void NextPage()
    {
        int nextIndex = currentIndex + 1;
        
        // Sembunyikan tombol next pada halaman terakhir
        if (nextIndex >= pages.Length - 1)
        {
            nextButton.SetActive(false);
        }

        // Fade out halaman sekarang
        LeanTween.alphaCanvas(pages[currentIndex], 0f, duration).setOnComplete(() =>
        {
            pages[currentIndex].interactable = false;
            pages[currentIndex].blocksRaycasts = false;

            // Fade in halaman berikutnya
            LeanTween.alphaCanvas(pages[nextIndex], 1f, duration).setOnComplete(() =>
            {
                pages[nextIndex].interactable = true;
                pages[nextIndex].blocksRaycasts = true;
                currentIndex = nextIndex;
            });
        });
    }

    public void PlayGame()
    {
        FadeCanvas fade = FindObjectOfType<FadeCanvas>();
        if (fade != null)
        {
            fade.FadeIn(() => SceneManager.LoadScene("Gameplay"));
        }
        else
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
