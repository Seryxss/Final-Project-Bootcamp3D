using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using TMPro;
using System;

public class PlayerBonusTime : MonoBehaviour
{
    public Timer GameTimer;
    [SerializeField] float TambahanWaktu;

    // public TextMeshProUGUI TextFinish;
    public TextMeshProUGUI TextKeterangan;

    private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5f);
    private static WaitForSeconds _waitForSeconds15 = new WaitForSeconds(15f);



    public void setPermanentText(string str)
    {
        TextKeterangan.text = str;
    }

    public void playerPickedupBox()
    {

        {
            GameTimer.AddTime(TambahanWaktu);
            GameTimer.UpdateTimerDisplay();
            SetText5Seconds("+" + TambahanWaktu + " seconds");
        }
    }

    public void SetText15Seconds(string str)
    {
        StartCoroutine(ClearText(str, _waitForSeconds15));
    }

    public void SetText5Seconds(string str)
    {
        StartCoroutine(ClearText(str, _waitForSeconds5));
    }
    IEnumerator ClearText(string str, WaitForSeconds yieldtime)
    {   
        TextKeterangan.text = str;
        yield return yieldtime;
        TextKeterangan.text = "";
        Debug.Log("TextCleared");
    }

    void UpdateTextUI()
  {
    
  }

}
