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
    public GameObject BoxA;

    public GameObject BoxB;
    public GameObject BoxC;

    public TextMeshProUGUI TextFinish;
    public TextMeshProUGUI TextKeterangan;

    public string LokasiA,LokasiB, LokasiC;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        int posA,posB,posC;
        posA = Random.Range(1, 100);
        if (posA <=50)
        {
            //BoxA.transform.position = new Vector3(48f, 62f, -60f);
            BoxA.transform.position = new Vector3(-29.66f, 1f, 18.42f);
            LokasiA = "Dekat Gudang " + posA;
        } else if(posA > 50){
           BoxA.transform.position = new Vector3(-27.66f, 1f, 18.42f);
            LokasiA = "Belok kiri, terus kiri lagi ,untuk melihat kotak merah " + posA;
        }
        else
        {
            LokasiA = "TIDAK JELAS " + posA + " LOKASI X : " + BoxA.transform.position.x + " LOKASI Y : " + BoxA.transform.position.y + " LOKASI Z : " + BoxA.transform.position.z;
        }
            TextKeterangan.text = "Silahkan ke Kotak Merah di " + LokasiA;
        BoxA.SetActive(true);

        posB = Random.Range(1, 100);
        if (posB <=30)
        {
            BoxB.transform.position = new Vector3(-27.66f, 1f, -50f);
            LokasiB = "Pinggir Jalan";
        }
        else if (posB <= 60)
        {
            BoxB.transform.position = new Vector3(-26.9f, 1f, -74.77f);
            LokasiB = "Apartemen Merah";
        }else 
        {
            BoxB.transform.position = new Vector3(5.1f, 1f, -85.4f);
            LokasiB = "Tumpukan Kotak";

        }
        LokasiB = LokasiB + " " + posB;

        posC = Random.Range(1, 100);

        if (posC <=50)
        {
            BoxC.transform.position = new Vector3(-29.66f, 1f, 18.42f);
            LokasiC = "Dekat Gudang Titik Awal";
        }else if (posC > 50)
        {
            BoxC.transform.position = new Vector3(-57.24f, 1f, -17.09f);
            LokasiC = "Dekat Bangunan Hijau";
        }
        LokasiC = LokasiC + " " + posC;

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
            Debug.Log("A Ditabrak");
            Destroy(other.gameObject);
            GameTimer.AddTime(TambahanWaktu);
            GameTimer.UpdateTimerDisplay();
            TextKeterangan.text = "Pesan : Waktu + 15 detik, Silahkan ke Kotak Hijau di " + LokasiB ;
            BoxB.SetActive(true);

        } else if (other.CompareTag("Start Box"))
        {
            Destroy(other.gameObject);
            GameTimer.isTimerJalan = true;


        }else if(other.CompareTag("Box B"))
        {
            Destroy(other.gameObject);
            GameTimer.AddTime(TambahanWaktu);
            GameTimer.UpdateTimerDisplay();
            TextKeterangan.text = "Pesan : Waktu + 15 detik, Silahkan ke Kotak Biru di " + LokasiC ;
            BoxC.SetActive(true);
        }
        else if (other.CompareTag("Box C"))
        {
            Destroy(other.gameObject);
            TextFinish.text = "";
            //            GameTimer.isTimerJalan = true;


            GameFinish.SetActive(true);
            GameTimer.SelesaiGame();
            SisaWaktu = GameTimer.GetElapsedTime();

            TextFinish.text = "Sisa Waktu Anda Adalah : " + SisaWaktu;
            if (SisaWaktu < 10)
            {
                Kriteria = "BINTANG 1 -> MEPET BANGET";
            } else if (SisaWaktu <= 20)
            {
                Kriteria = "BINTANG 2 -> LUMAYAN LAH";
            }
            else
            {
                Kriteria = "BINTANG 3 -> PERFECT";
            }
            TextFinish.text= "Permainan Selesai - " + "Sisa Waktu Anda Adalah : " + SisaWaktu + " detik " + Kriteria;
//            TextFinish.text = "Sisa Waktu Anda Adalah : " + SisaWaktu + " detik " + Kriteria;

            //            Debug.Log(GameTimer.GetElapsedTime());
        }
    }
}
