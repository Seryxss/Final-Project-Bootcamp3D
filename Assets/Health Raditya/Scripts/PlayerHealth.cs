using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI Settings")]
    public Image[] hearts;             // tempatkan gambar hati di inspector
    public Sprite fullHeart;           // sprite hati penuh
    public Sprite emptyHeart;          // sprite hati kosong

    [Header("References")]
    public Animator playerAnimator;    // drag animator player
    public CharacterMovement movement;    // script movement player
    private PlayerBlinkEffect blinkEffect;

    [Header("Camera Effect (optional)")]
    public Camera mainCamera;
    private Vector3 originalCamPos;

    private bool isDead = false;

    [Header("UI Game Over")]
    public GameObject gameOverUI;

    [Header("Game Over")]
    public AudioSource gameOverSound;
    public bool isGameOver = false;

    [HideInInspector] public bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();

        blinkEffect = GetComponent<PlayerBlinkEffect>();

        if (mainCamera != null)
            originalCamPos = mainCamera.transform.localPosition;
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isDead)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        UpdateHeartsUI();
        GetComponent<DamageBlink>().TriggerBlink();

        if (blinkEffect != null)
            blinkEffect.Blink(); // efek samar

        if (playerAnimator != null)
            playerAnimator.SetTrigger("Hit"); // mainkan animasi hit (kalau ada)

        StartCoroutine(CameraShake(0.15f, 0.1f)); // efek kamera bergetar kecil

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        if (mainCamera == null) yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            mainCamera.transform.localPosition = new Vector3(x, y, originalCamPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.localPosition = originalCamPos;
    }

    void GameOver()
    {
        isDead = true;

        if (playerAnimator != null)
            playerAnimator.SetTrigger("Die");

        if (movement != null)
            movement.enabled = false; // matikan script gerak player

        FindObjectOfType<GameManager>().GameOver(); 

          // munculkan UI GameOver
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        // GameOverUI.SetActive(true);
    
        // mainkan suara game over
        if (gameOverSound != null)
            gameOverSound.Play();
            
        Debug.Log("Game Over!");
    } 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isInvincible)
        {
            TakeDamage();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player kena obstacle! Health sekarang: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
            // Kamu bisa tambahkan logika game over di sini
        }

        StartCoroutine(InvincibleTime());
    }

    private IEnumerator InvincibleTime()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
}

