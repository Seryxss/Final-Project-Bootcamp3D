using UnityEngine;
using System.Collections;

public class DamageBlink : MonoBehaviour
{
    private Material originalMaterial;
    public Material blinkMaterial;  // Material warna putih/merah sementara
    private Renderer rend;

    public float blinkDuration = 0.1f;  // durasi satu kedipan
    public int blinkCount = 3;          // berapa kali berkedip

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void TriggerBlink()
    {
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            rend.material = blinkMaterial;
            yield return new WaitForSeconds(blinkDuration);
            rend.material = originalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}