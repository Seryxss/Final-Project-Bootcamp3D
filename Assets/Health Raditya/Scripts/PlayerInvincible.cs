using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    public float invincibleDuration = 2f;
    private bool isInvincible = false;
    private Material playerMaterial;
    private Color originalColor;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerMaterial = GetComponent<MeshRenderer>().material;
        originalColor = playerMaterial.color;
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isInvincible)
        {
            StartCoroutine(BecomeInvincible());
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        playerHealth.isInvincible = true;

        // efek transparan
        Color newColor = originalColor;
        newColor.a = 0.3f;
        playerMaterial.color = newColor;

        yield return new WaitForSeconds(invincibleDuration);

        // kembalikan normal
        playerMaterial.color = originalColor;
        playerHealth.isInvincible = false;
        isInvincible = false;
    }
}