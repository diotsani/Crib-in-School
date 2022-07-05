using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    //audio manager
    [SerializeField] private SoundManager audiomanager;
    [SerializeField] private int nomorPopup;//menentukan sound popup mana yang akan keluar

    public GameObject maincamera;
    public GameObject camera;
    //variabel btn popup puzzle
    public GameObject btnPopuppuzzle;

    [SerializeField] public Material material, defaultmaterial;
    private Transform _slection;
    public GameObject puzzle;
    public PuzzleManager pzm;
    [SerializeField] private string selecttag = "player2";

    [Header("Player")]
    public characterscript[] playerpref;
    public Transform[] posisi;
    public Transform[] posisibarplayer;
    public int[] nomorKursi;

    public int jumlahPlayer;
    [SerializeField]private int pemainAwal;//jumlah dari pencontek awal
    public int PencontekAwal; //Nomer Kursi Player awal
    public int[] temanPencontek;
    public int acakpencontek;
    [Header("murid cepu")]
    public MuridCepu[] muridcepu;
    public Transform[] posisiMuridCepu;
    [Header("Progress Player")]
    public ProgressBarPlayer[] progresplayer;
    public int nourut;

    // int depan,tengah,acak;
    //public cekposisi[] cekpos;

    public bool munculPuzzle;//kondisi memunculkan puzzle
    [Header("transfer")]
    //untuk transfer contekan
    public bool _transfer;
    public float speedTransfer;
    public float speedTransferDefault;
    public Transform targetpostransfer;//target player yang akan di bagikan contekan
    public Transform postransferawal;//target player awal contekan
    public Transform pesawatpos;//posisi pesawat
    public GameObject pesawat;//objek pesawat
    public GameObject tanda;//objek tanda dan deteksi
    public GameObject peringatanObj;//peringatan
    public Transform posisibar;//membaca posisi bar kalo mau ganti objek tambahin function transform baru

    //animasi
    public bool ketahuan;
    public bool ketahuan2;

    [Header("pause")]
    public PauseManager pause;


    public Skill3 skill3;
    public GameObject ManagerPesawat;
    public bool transferpesawat;

    public bool ketahuanMuridCepu;

    public  bool stargame;
    private void Awake()
    {
        foreach (var item in progresplayer)
        {
            item.maxlenght = pzm.jumlahSoal;
        }
        audiomanager = FindObjectOfType<SoundManager>();
        pause = FindObjectOfType<PauseManager>();
        stargame = false;
        acakpencontek = Random.Range(0, 2);
        if (acakpencontek == 0)
        {
            nourut = 0;
            pemainAwal=PencontekAwal;
        }
        else
        {
            nourut = 1;
            pemainAwal = temanPencontek[0];
        }
        foreach (var player in playerpref)
        {
            player.name = "player2";
        }
    }
    private void Start()
    {
        ketahuan = false;
        ketahuan2 = false;
        ketahuanMuridCepu = false;
        speedTransfer = speedTransferDefault;
        skill3 = FindObjectOfType<Skill3>();
        pesawat.SetActive(false);
        munculPuzzle = false;
        Invoke("addplayer", 0.2f);
    }
    void addplayer()
    {
        for (int i = 0; i < playerpref.Length; i++)
        {

            if (i == PencontekAwal)
            {
                playerpref[i].pencontekawal = true;
            }
            else
            {
                playerpref[i].pencontekawal = false;
            }
            if (nomorKursi.Length == posisi.Length)
            {
                playerpref[i].name = "player" + i;

                if (muridcepu.Length>0)
                {
                    if (posisi[nomorKursi[i]] == posisiMuridCepu[0])
                    {
                        playerpref[i].enabled = false;
                        Instantiate(muridcepu[0], posisi[nomorKursi[i]]);
                    }
                    else
                    {
                        Instantiate(playerpref[i], posisi[nomorKursi[i]]);
                    }
                }
                else
                {
                    Instantiate(playerpref[i], posisi[nomorKursi[i]]);
                }

            }
            else
            {
                Debug.Log("jumlah nomor kurang");
            }
        }
        
        
        targetpostransfer.position=posisibarplayer[pemainAwal].position;
        posisibar.position = posisibarplayer[pemainAwal].position;
        pesawatpos.position = posisibar.position;
        postransferawal.position = posisibar.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (pzm.end.ketahuan==true)
        {
            ketahuan = true;
            pzm.keluarketahuan();
        }
        if (ketahuanMuridCepu==true)
        {
            ketahuan2 = true;
            Invoke("ending", 1f);
        }
       
        //==============================skil3 setting speed===========================================

        if (skill3!=null)
        {
            if (transferpesawat == true)
            {
                speedTransfer = speedTransfer + skill3.speedpesawat;
            }
            else
            {
                speedTransfer = speedTransferDefault;
            }
        }
        //============================================================================================
        skill3 = FindObjectOfType<Skill3>();

        if (pause.gameIsPaused==false)
       
        if (_slection != null)
        {
            var selectionrenderer = _slection.GetComponent<Renderer>();
            selectionrenderer.material = defaultmaterial;
            _slection = null;
        }
        if (munculPuzzle == false)
        {
            if (_transfer == true)//jika transfer bernilai true maka dapat pindah posisi
            {
                transfer(targetpostransfer);
            }
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag("Player"))
                {
                    print("click");
                    transferPesawatMethod(selection);
                }
                else
                {
                    print("bukan pemain");
                }
            }

        }
    }
    void ending()
    {
        pzm.end.ketahuan = true;
        PesawatScript.FindObjectOfType<PesawatScript>()._endPanel.SetActive(true);
    }
    public void transferPesawatMethod(Transform selection)//berfungsi kalo mau menambahkan player
    {
        var selectionrenderer = selection.GetComponent<Renderer>();
        if (selectionrenderer != null)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (_transfer == false&&selection.position!=targetpostransfer.position)
                {
                    if (selection.position!=targetpostransfer.position)
                    {
                        stargame = true;
                    }
                    if (stargame==true)
                    {
                        if (skill3!=null)
                        {
                           
                            if (skill3.invisible==true)
                            {
                                pesawat.SetActive(false);
                                transferpesawat = true;
                            }
                            else
                            {
                                speedTransfer = speedTransferDefault;
                                pesawat.SetActive(true);
                            }
                        }
                        else
                        {
                            pesawat.SetActive(true);
                        }
                        tanda.SetActive(false);
                        peringatanObj.SetActive(false);
                        audiomanager.transferMethod(0);
                        _transfer = true;//jika posisi player tidak sama maka transfer true
                        targetpostransfer = selection;//mengubah posisi target sesuai player yang di klik
                    }
                }
            }
        }
        _slection = selection;
    }

    //function atau method untuk script tranfer
    void transfer(Transform postransfer)
    {
        if (skill3 != null)
        {

            if (skill3.invisible == true)
            {
                pesawat.SetActive(false);
                speedTransfer = speedTransfer +skill3.speedpesawat;
            }
            else
            {
                speedTransfer = speedTransferDefault;
                pesawat.SetActive(true);
            }
        }
        pesawatpos.position = Vector3.MoveTowards(pesawatpos.position, postransfer.position, speedTransfer * Time.deltaTime);
        pesawatpos.LookAt(postransfer);
    }

    public void openPuzzle()
    {
        if (_transfer == false)
        {
            audiomanager.popupMetohod(nomorPopup);

            maincamera.SetActive(false);
            camera.SetActive(true);
            puzzle.SetActive(true);
            btnPopuppuzzle.SetActive(false);
            munculPuzzle = true;
        }
    }
}
