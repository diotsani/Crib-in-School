using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lvSelected : MonoBehaviour
{
    public levelmanager lm;
    [SerializeField] private GameObject lvselectobj;//objek dari panel game select
    public SoundManager sfx;
    [SerializeField] private GameObject btnatas;
    [SerializeField] private GameObject btnbawah;
    [Header("farme level select")]
    public Sprite[] framelv;//asset gambar farame lv
    public int lvnumber;//nomor kolom level
    public Image[] frame;//farame 0 yang terlihat selebihnya agak transparan
    public GameObject[]frameobj;//objekframe
    
    [Header("nomor kolom")]
    public Image[] selectlcnumber;//nomor lv pada kolom frame
    public Sprite[] selectlcnumberimg;//asset gambar nomor lv pada kolom frame
    public int urutanNumber;//nomor urut angka
    [Header("pengawas")]
    public Image[] pengawas;//objek pengawas 0 pengawas inti
    public Sprite[] imgPengawas;//aasset gambar pengawas
    public Text[] nama;//namapengawas 0 bagian inti
    public string[] nameList;//list nama pengawas

    public int nomorlv;

    [Header("lock lv")]
    public Image[] lockimg;
    public Image[] cominngsoonimg;

    private void Awake()
    {
        lm = FindObjectOfType<levelmanager>();
        sfx = FindObjectOfType<SoundManager>();
        lvnumber = 0;
        urutanNumber = 0;
    }
    
    private void Reset()
    {
        Awake();   
    }
    // Update is called once per frame
    void Update()
    {
        /*catatan
         * selecnumber 0-2 merupakan frame inti
         * 3-5 frame atas
         * selain itu frame bawah
         */
        selectlcnumber[0].sprite = selectlcnumberimg[urutanNumber + 0];
        selectlcnumber[1].sprite = selectlcnumberimg[urutanNumber + 1];
        selectlcnumber[2].sprite = selectlcnumberimg[urutanNumber + 2];
        lvcomingsoon();
        lvlockManager();
        frame[0].sprite = framelv[lvnumber];
        pengawas[0].sprite = imgPengawas[lvnumber];
        siluet();
        nama[0].text = nameList[lvnumber];
        if (lvnumber == 0)
        {
            frameobj[1].SetActive( false);
            btnatas.SetActive(false);
            pengawas[1].enabled = false;
        }
        else if(lvnumber==framelv.Length-1)
        {
            frameobj[2].SetActive( false);
            btnbawah.SetActive(false);
            pengawas[2].enabled = false;

        }

        lm.urutanNumber = urutanNumber;
        lm.nomorlv = nomorlv;
    }
    void lvlockManager()
    {
        if (lm.lvUnlock[urutanNumber + 0] == true)
        {
            lockimg[urutanNumber + 0].enabled = false;
        }
        else
        {
            lockimg[urutanNumber + 0].enabled = true;
        }
        if (lm.lvUnlock[urutanNumber + 1] == true)
        {
            lockimg[urutanNumber + 1].enabled = false;
        }
        else
        {
            lockimg[urutanNumber + 1].enabled = true;
        }
        if (lm.lvUnlock[urutanNumber + 2] == true)
        {
            lockimg[urutanNumber + 2].enabled = false;
        }
        else
        {
            lockimg[urutanNumber + 2].enabled = true;
        }
    }
    void lvcomingsoon()
    {
        if (lm.lvcomingsoon[urutanNumber + 0] == true)
        {
            cominngsoonimg[urutanNumber + 0].enabled = false;
        }
        else
        {
            cominngsoonimg[urutanNumber + 0].enabled = true;
        }
        if (lm.lvcomingsoon[urutanNumber + 1] == true)
        {
            cominngsoonimg[urutanNumber + 1].enabled = false;
        }
        else
        {
            cominngsoonimg[urutanNumber + 1].enabled = true;
        }
        if (lm.lvcomingsoon[urutanNumber + 2] == true)
        {
            cominngsoonimg[urutanNumber + 2].enabled = false;
        }
        else
        {
            cominngsoonimg[urutanNumber + 2].enabled = true;
        }
    }
    void siluet()
    {
        if (lvnumber > 0)
        {
            selectlcnumber[3].sprite = selectlcnumberimg[urutanNumber - 3 + 0];
            selectlcnumber[4].sprite = selectlcnumberimg[urutanNumber - 3 + 1];
            selectlcnumber[5].sprite = selectlcnumberimg[urutanNumber - 3 + 2];
            nama[1].text = nameList[lvnumber-1];
            frame[1].sprite = framelv[lvnumber - 1];
            pengawas[1].sprite = imgPengawas[lvnumber-1];
        }
        if (lvnumber < framelv.Length - 1)
        {
            selectlcnumber[6].sprite = selectlcnumberimg[urutanNumber + 3 + 0];
            selectlcnumber[7].sprite = selectlcnumberimg[urutanNumber + 3 + 1];
            selectlcnumber[8].sprite = selectlcnumberimg[urutanNumber + 3 + 2];
            nama[2].text = nameList[lvnumber+1];
            frame[2].sprite = framelv[lvnumber + 1];
            pengawas[2].sprite = imgPengawas[lvnumber+1];
        }
    }
    public void nextbtn()
    {
        sfx.buttonclickMethod();
        btnatas.SetActive(true);
        frameobj[1].SetActive( true);
        pengawas[1].enabled = true;
        if (lvnumber<framelv.Length-1)
        {
            lvnumber += 1;
            urutanNumber += 3;
        }
       
    }
    public void backbtn()
    {
        pengawas[2].enabled = true;
        frameobj[2].SetActive( true);
        sfx.buttonclickMethod();
        btnbawah.SetActive(true);
        if (lvnumber > 0)
        {
            lvnumber -= 1;
            urutanNumber -= 3;
        }
    }
    public void open()
    {
        sfx.buttonclickMethod();
        Reset();
        this.gameObject.SetActive(true);
    }
    public void close()
    {
        sfx.buttonclickMethod();
        this.gameObject.SetActive(false);
    }
    public void btnpertama()
    {
        nomorlv = 1;
    }
    public void btnkedua()
    {
        nomorlv = 2;
    }
    public void btnketiga()
    {
        nomorlv = 3;
    }
}
