using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1f);
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

    private void playerCollided(GameObject gameObject)
    {
            if (gameObject.CompareTag("Obstacle") && !isDead && !isInvincible)
            {
                Debug.Log("Stumbled into: " + name);
                TakeDamage(1);
            }
            else if (gameObject.CompareTag("Danger") && !isDead && !isInvincible)
            {
                Debug.Log("Hit by car: " + name);
                TakeDamage(3);
            }
    }

    public void OnControllerColliderHit(ControllerColliderHit collision)
    {
        playerCollided(collision.gameObject);
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

    public void GameOver(string deathCause)
    {
        isDead = true;
        isGameOver = true;

        if (playerAnimator != null)
            playerAnimator.SetTrigger("Die");

        if (movement != null)
            movement.enabled = false; // matikan script gerak player

        FindObjectOfType<GameManager>().GameOver(deathCause);
        Debug.Log("Game Over!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TOnTriggerEnter: " + other.tag + "| other.tag:" + (other.tag == "Obstacle") + "| isInvincible:" + isInvincible + "|isDead." + isDead);
        playerCollided(other.gameObject);
    }

    public void TakeDamage(int amount)
    {
        SoundManager.PlaySound(SoundType.HitObstacle, 0.7f);
        currentHealth -= amount;
        Debug.Log("Player kena obstacle! Health sekarang: " + currentHealth);

        UpdateHeartsUI();
        GetComponent<DamageBlink>().TriggerBlink();

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
            if (amount > 1)
            {
                GameOver("Tertabrak mobil...");
            }
            else
            {
                GameOver("Terjatuh...");
            }
        }
        else
        {
            StartCoroutine(InvincibleTime());
        }
    }

    private IEnumerator InvincibleTime()
    {
        Debug.Log("Player is invincible");
        isInvincible = true;
        yield return _waitForSeconds1;
        isInvincible = false;
        Debug.Log("Player is now non invincible");
    }
}

