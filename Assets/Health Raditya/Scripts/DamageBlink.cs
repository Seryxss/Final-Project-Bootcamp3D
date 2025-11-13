using UnityEngine;
using System.Collections;

public class DamageBlink : MonoBehaviour
{
    private Material originalMaterial;
    public Material blinkMaterial;  // Material warna putih/merah sementara
    private SkinnedMeshRenderer rend;  // Ubah ke SkinnedMeshRenderer untuk karakter 3D

    public float blinkDuration = 0.1f;  // durasi satu kedipan
    public int blinkCount = 3;          // berapa kali berkedip

    void Start()
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();  //ambil dari child
        if (rend != null)
            originalMaterial = rend.material;
        else
            Debug.LogWarning("Renderer tidak ditemukan! Pastikan Player memiliki SkinnedMehshRenderer."); 
    }

    public void TriggerBlink()
    {
        if (rend != null)
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