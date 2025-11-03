using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlinkEffect : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>(); // ambil renderer dari model anak
        originalColor = rend.material.color;
    }

    public void Blink()
    {
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        rend.material.color = Color.white; // jadi putih terang
        yield return new WaitForSeconds(0.15f);
        rend.material.color = originalColor; // kembali normal
    }
}