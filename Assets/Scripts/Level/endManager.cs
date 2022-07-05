using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(endManager))]
public class endManager : MonoBehaviour
{
    public LoadSkill lskl;
    //======================================================================================================================
    //lv unlock variabel
    levelmanager lm;
    public int bukalv;//nomor lv yang ingin dibuka

    //exp dan koin variabel
    public Text exptext;
    public float expRecived;
    public int poin;

    //======================================================================================================================

    [SerializeField] private SoundManager audiomanager=null;
    [SerializeField] private BgmManager bgm=null;
    //isi panel
    public Text lulustxt;
    public int totallulus;
    private float persen;

    //jumlah puzzle benar
    public Text hasilAkhir;
    public Image title,grade;
    public Sprite[] berhasil,gagal;

    public GameObject papan;
    public GameObject endPanel;

    public int jawabanBenar;
    //berdasarkan jumlah puzzle
    public float nilaitertinggi;

    public PuzzleManager puzzle;
    private bool ending;
    private float timestop=1f;

    public bool ketahuan;//ketahuan guru dan murid cepu

    //player manager
    [SerializeField] private player playermanager;
    // tombol restar dan home
    public GameObject btnFiled;
    public GameObject btnwin;

    void Awake()
    {
        //puzzle = FindObjectOfType<PuzzleManager>();
        lskl = FindObjectOfType<LoadSkill>();
        lm = FindObjectOfType<levelmanager>();
        playermanager = FindObjectOfType<player>();
        audiomanager = FindObjectOfType<SoundManager>();
        bgm = FindObjectOfType<BgmManager>();
        ending = true;
    }
    private void Start()
    {
        nilaitertinggi = puzzle.jumlahSoal;
        bgm.bgm.Stop();
        lskl.clone_1.SetActive(false);
        lskl.panelskill.SetActive(false);
        //audiomanager.resultMethod(2);
        persen = (puzzle.score / nilaitertinggi) * 100;
        lulustxt.text = totallulus.ToString();

        hasilAkhir.text = puzzle.score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //panel akhir setting
       if (persen==100&& ketahuan==false)
        {
            title.sprite = berhasil[0];
            grade.sprite = berhasil[1];
            expRecived = persen;
            poin = +1;
            if (lm.lvUnlock[bukalv - 1] == false)
            {
                UserDataManager.Progress.lvunlock = bukalv;
                lm.lvUnlock[bukalv - 1] = true;
            }
            btnFiled.SetActive(false);
            btnwin.SetActive(true);

        }
        else if (persen>=75&& ketahuan == false)
        {
            poin = +1;
            title.sprite = berhasil[0];
            grade.sprite = gagal[1];
            if (lm.lvUnlock[bukalv - 1] == false)
            {
                UserDataManager.Progress.lvunlock = bukalv;
                lm.lvUnlock[bukalv - 1] = true;
            }
            expRecived = persen;
            btnFiled.SetActive(false);
            btnwin.SetActive(true);
        }
        else if(ketahuan==true||persen<80)
        {
            expRecived = persen;
            title.sprite = gagal[0];
            grade.sprite = gagal[2];
            btnFiled.SetActive(true);
            btnwin.SetActive(false);
        }
        //exptext.text = "+"+expRecived+"Exp";
        exptext.text = "+"+poin+"Poin";
        if (ending==true)
        {
            UserDataManager.Save();
            timestop -= Time.deltaTime;
            if (timestop<0.1f)
            {
                Time.timeScale = 0;
            }
        }
    }
    public void restart()
    {
        SceneManager.LoadScene(Application.loadedLevel);
        audiomanager.result.Stop();
    }
    public void keluar()
    {
        Application.Quit();
    }
    public void close()
    {
        papan.SetActive(false);
    }

    public void Next()
    {
        audiomanager.result.Stop();
        UserDataManager.Progress.expPlayer += expRecived;
        UserDataManager.Progress.koin += poin;
        UserDataManager.Save();
        Time.timeScale = 1;
    }
}
