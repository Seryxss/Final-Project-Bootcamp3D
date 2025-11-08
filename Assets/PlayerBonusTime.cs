using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using TMPro;

public class PlayerBonusTime : MonoBehaviour
{
    public Timer GameTimer;
    [SerializeField] float TambahanWaktu;
    public GameObject GameFinish;
    public GameObject BoxB;
    public TextMeshProUGUI TextFinish;
    public TextMeshProUGUI TextKeterangan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        string Kriteria = "";
        float SisaWaktu = 0;


        if (other.CompareTag("Box A"))
        {
            Destroy(other.gameObject);
            GameTimer.AddTime(TambahanWaktu);
            GameTimer.UpdateTimerDisplay();
            TextKeterangan.text = "Waktu + 10 detik, Silahkan ke Kotak Hitam";
            BoxB.SetActive(true);

        } else if (other.CompareTag("Start Box"))
        {
            Destroy(other.gameObject);
            GameTimer.isTimerJalan = true;
            
        }
        else if (other.CompareTag("Box B"))
        {
            Destroy(other.gameObject);
            TextKeterangan.text = "";
//            GameTimer.isTimerJalan = true;


            GameFinish.SetActive(true);
            GameTimer.SelesaiGame();
            SisaWaktu = GameTimer.GetElapsedTime();

            TextFinish.text = "Sisa Waktu Anda Adalah : " + SisaWaktu ;
            if(SisaWaktu < 10)
            {
                Kriteria = "BINTANG 1 -> MEPET BANGET";
            }else if (SisaWaktu<=20)
            {
                Kriteria = "BINTANG 2 -> LUMAYAN LAH";
            }
            else 
            {
                Kriteria = "BINTANG 3 -> PERFECT";
            }
            TextFinish.text = "Sisa Waktu Anda Adalah : " + SisaWaktu + " detik " + Kriteria;

//            Debug.Log(GameTimer.GetElapsedTime());
        }
    }
}
