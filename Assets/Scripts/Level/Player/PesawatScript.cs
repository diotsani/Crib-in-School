using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesawatScript : MonoBehaviour
{
    [Header("Menentukan posisi level")]
    [SerializeField] private int lv;

    public GameObject CameraWin;
    public GameObject maincamera;

    public player playermanager;
    public GameObject pesawat;
    public Gamemanager gamemanager;
    public GameObject _endPanel;
    public endManager panelscript;
    public SoundManager sfx;
    public ProgressBarPlayer[] pemain;
    public endManager end;
    //audio manager
    [SerializeField] private SoundManager audiomanager;
    int urutan;

    public int progress1=0;
    public int progress2=1;
    //skill
    Skill3 skill3;

    //public MuridCepu muridCepu;
    //public GameObject[] objectMarker;
    public Marker[] markerPlayer;


    private void Awake()
    {
        if (lv!=1)
        {
            //muridCepu = GameObject.FindGameObjectWithTag("enemy").GetComponent<MuridCepu>();

        }
        CameraWin.SetActive(false);
        while (progress2==progress1)
        {
            progress2 = Random.Range(0, 1);
        }
        sfx = FindObjectOfType<SoundManager>();
        //end = FindObjectOfType<endManager>();
        urutan = playermanager.acakpencontek;
        audiomanager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        ////for (int i = 0; i < playermanager.progresplayer.Length-1; i++)
        ////{
        ////    progress1 = i;
        ////    if (transform.position==playermanager.progresplayer[i].transform.position)
        ////    {
        ////        for (int j = 0; j < playermanager.progresplayer.Length-1; j++)
        ////        {
        ////            progress2=j;
        ////            if (transform.position!=playermanager.progresplayer[i].transform.position)
        ////            {
        ////                triggercallplayer(progress1, progress2);
        ////            }
        ////            if (transform.position == playermanager.progresplayer[j].transform.position)
        ////            {
        ////                int progress = i;
        ////                j = progress;
        ////                i = j;
        ////            }
        ////        }
        ////        if (playermanager.stargame == true)
        ////        {
        ////            playermanager.nourut = progress1;
        ////            int switchprogress = progress1;
        ////            progress1 = progress2;
        ////            progress2 = switchprogress;

        ////        }
        ////    }
        //}

        skill3 = FindObjectOfType<Skill3>();

        if (lv != 1)
        {
            //muridCepu = GameObject.FindGameObjectWithTag("enemy").GetComponent<MuridCepu>();
        }


        //objectMarker = GameObject.FindGameObjectsWithTag("Marker");
        //markerPlayer = new Marker[objectMarker.Length];
        //for (int i = 0; i < objectMarker.Length; i++)
        //{
        //markerPlayer[i] = objectMarker[i].GetComponent<Marker>();
        //}
    }
    void endpanelMethod()
    {
        CameraWin.SetActive(true);
        maincamera.SetActive(false);
        _endPanel.SetActive(true);
    }
    void triggercallplayer(int pemain1, int pemain2)//jika menambah player tambah fungsi ini
    {
        var pemegangcontekan = pemain1;
        pemain[pemegangcontekan].current = pemain[pemain2].current;
        playermanager.pesawatpos.position = playermanager.targetpostransfer.position;
        playermanager.posisibar.position = playermanager.targetpostransfer.position;
        playermanager.pesawat.SetActive(false);
        playermanager.tanda.SetActive(true);
        playermanager.postransferawal.position = playermanager.targetpostransfer.position;
        playermanager._transfer = false;
        audiomanager.transferMethod(1);
        playermanager.nourut = urutan;
        //skill setting speed
        playermanager.transferpesawat = false;
        playermanager.pesawatpos.rotation = playermanager.targetpostransfer.rotation;
        if (playermanager.pzm.solvedPuzzle == true)
        {

            if (playermanager.progresplayer[progress1].selesai == false)
            {
                Debug.Log("sudah .....");

                panelscript.totallulus += 1;
                playermanager.jumlahPlayer--;
                playermanager.progresplayer[progress1].selesai = true;
            }
            else if(playermanager.progresplayer[progress1].selesai == true)
            {
                Debug.Log("sudah selesai");
            }
            if (playermanager.jumlahPlayer==0)
            {
                audiomanager.resultMethod(3);
                
                Invoke("endpanelMethod", 1f);
            }
        }
    }
    public void lvMethod(Collider other)
    {
        if(lv == 1)
        {
            if (other.transform.tag == "barplayer0")
            {
                progress1 = 0;
                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[0].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                    //int switchprogress = progress1;
                    //progress1 = progress2;
                    //progress2 = switchprogress;

                }
            }
            else if (other.transform.tag == "barplayer1")
            {
                progress1 = 1;

                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[1].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                    //playermanager.pesawat.transform.localRotation = playermanager.targetpostransfer.rotation;
                }
            }
        }
        else if (lv == 2)
        {
            if (other.transform.tag == "barplayer0")
            {
                progress1 = 0;
                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[0].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                    //int switchprogress = progress1;
                    //progress1 = progress2;
                    //progress2 = switchprogress;

                }
            }
            else if (other.transform.tag == "barplayer1")
            {
                progress1 = 1;

                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[1].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                    print(playermanager.nourut);

                }
            }
            else if (other.transform.tag == "barplayer2")
            {
                progress1 = 2;
                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[2].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                }
            }
        }
        else if (lv == 3)
        {
            if (other.transform.tag == "barplayer0")
            {
                progress1 = 0;
                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[0].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                    //int switchprogress = progress1;
                    //progress1 = progress2;
                    //progress2 = switchprogress;

                }
            }
            else if (other.transform.tag == "barplayer1")
            {
                progress1 = 1;

                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[1].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                    print(playermanager.nourut);

                }
            }
            else if (other.transform.tag == "barplayer2")
            {
                progress1 = 2;
                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[2].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                }
            }
            else if (other.transform.tag == "barplayer3")
            {
                progress1 = 3;
                triggercallplayer(progress1, progress2);
                progress2 = progress1;
                markerPlayer[3].markerAktif = true;
                if (playermanager.stargame == true)
                {
                    playermanager.nourut = progress1;
                }
            }
        }
    }
    /*
     *  jika terkena tag enemy bakal keluar panel end
     */
    private void OnTriggerEnter(Collider other)
    {
        lvMethod(other);
        if (other.transform.tag == "enemy")//jika terkena murid cepu 
        {
            if (skill3!=null)
            {
                if (skill3.invisible==false)
                {
                    playermanager.ketahuanMuridCepu = true;
                    playermanager.postransferawal.position = playermanager.targetpostransfer.position;

                    Destroy(playermanager.pesawat);
                    sfx.resultMethod(1);
                    Invoke("ending", 2f);
                }
            }
            else
            {
                playermanager.ketahuanMuridCepu = true;
                playermanager.postransferawal.position = playermanager.targetpostransfer.position;

                Destroy(playermanager.pesawat);

                sfx.resultMethod(1);
                Invoke("ending", 2f);

            }
            print("lewat");
        }

    }
    public void ending()
    {
        playermanager.pzm.keluarketahuan();
    }
    private void OnTriggerExit(Collider other)
    {
        if (lv == 1)
        {
            if (other.transform.tag == "barplayer0")
            {
                markerPlayer[0].markerAktif = false;
            }
            else if (other.transform.tag == "barplayer1")
            {
                markerPlayer[1].markerAktif = false;
            }
        }

        if (lv == 2)
        {
            if (other.transform.tag == "barplayer0")
            {
                markerPlayer[0].markerAktif = false;
            }
            else if (other.transform.tag == "barplayer1")
            {
                markerPlayer[1].markerAktif = false;
            }

            else if (other.transform.tag == "barplayer2")
            {
                markerPlayer[2].markerAktif = false;
            }
        }

        if (lv == 3)
        {
            if (other.transform.tag == "barplayer0")
            {
                markerPlayer[0].markerAktif = false;
            }
            else if (other.transform.tag == "barplayer1")
            {
                markerPlayer[1].markerAktif = false;
            }

            else if (other.transform.tag == "barplayer2")
            {
                markerPlayer[2].markerAktif = false;
            }
            else if (other.transform.tag == "barplayer3")
            {
                markerPlayer[3].markerAktif = false;
            }
        }
    }
}
