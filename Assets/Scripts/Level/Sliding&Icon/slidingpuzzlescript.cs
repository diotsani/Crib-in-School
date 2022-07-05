using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class slidingpuzzlescript : MonoBehaviour
{
    [Header("audioManager")]
    public SoundManager audiomanager;
    [SerializeField] private int nomorSfx;//menentukan sfxpuzzle mana yang akan keluar


    //gamemanager
    public PuzzleManager pzm;
    private player playermanager;

    [Header("ProgressBar")]
    public progressBarScript progressbar;
    //==============================================================
    [Header("camera")]
    //mendeteksi camera
    private Camera _Camera;

    //sliding puzzle script
    //public GameObject contohpuzzle;
    public GameObject[] contohNomerPuzzle;

    public GameObject progressbarpuzzle;
    public GameObject progressbarTime;
    [Header("tiles")]
    public GameObject slidingpuzzleobj;
    public GameObject buttonliat;
    //tiles atau kotak yang ada di dalam permainan
    [SerializeField] private Transform emptyspace = null;
    [SerializeField] public Tiles[] tiles;
    public float jarak;

    //iconsearching script
    public Box[] boxmanager;
    public GameObject iconsearchingObj;
    public SpriteRenderer[] Box;
    public Transform[] poskotak;
    public int poskotakbenar;
    //public Sprite kotakbenar, kotaksalah, kotakdefault;
    public bool lihatKotak;

    public int r = 0;


    //waktu
    public float time;
    public timeManager waktu;
    public bool hitungwaktu;
    private float spawniconsearching = 1f;
    //icon searcing
    public float gantikotak = 0.2f;
    //==============================================================
    public bool _pause;
    //==============================================================

    //jika selesai;
    //public GameObject imgend;
    public GameObject empty;
    public bool solved;
    public bool selesai;
    //===============================================================
    [Header("managerPuzzle")]
    public bool slidingPuzzle;
    public int jawabanBenar;

    private void Awake()
    {
        pzm = FindObjectOfType<PuzzleManager>();
        audiomanager = FindObjectOfType<SoundManager>();
        playermanager = FindObjectOfType<player>();
    }
    private void Start()
    {
        contohNomerPuzzle = GameObject.FindGameObjectsWithTag("NomerSliding");
        //contohpuzzle.SetActive(false);
        contohNomerPuzzle[0].SetActive(false);
        contohNomerPuzzle[1].SetActive(false);
        contohNomerPuzzle[2].SetActive(false);
        contohNomerPuzzle[3].SetActive(false);
        contohNomerPuzzle[4].SetActive(false);
        contohNomerPuzzle[5].SetActive(false);
        contohNomerPuzzle[6].SetActive(false);
        contohNomerPuzzle[7].SetActive(false);
        contohNomerPuzzle[8].SetActive(false);

        pzm.jumlahSoal += 1;
        progressbarpuzzle.SetActive(true);
        poskotakbenar = Random.Range(0, poskotak.Length);
        lihatKotak = false;
        slidingPuzzle = true;
        //waktu ketika awal mulai
        waktu.waktu = waktu.menit * 60;
        hitungwaktu = false;
        progressbar.currentTime = waktu.waktu;
        progressbar.maxlenghtTime = waktu.waktu;

        _Camera = Camera.main;
        // imgend.SetActive(false);
        empty.SetActive(true);
        solved = false;
        _pause = false;
        //back = false;
        //fungsi acak tiles ketika mulai
        shuffle();
        hitungwaktu = true;
    }
    private void Update()
    {
        /*icon searching script*/
        //menghitung waktu kembali kotak ke default
        if (lihatKotak == true)
        {
            if (gantikotak >= 0.1f)
            {
                gantikotak -= Time.deltaTime;
            }
            else
            {
                lihatKotak = false;
                gantikotak = 0.2f;
            }
        }
        //fungsi mengembalikan kotak default
        else
        {
            for (int i = 0; i < Box.Length; i++)
            {
                boxmanager[i].img.sprite = boxmanager[i].kotakdefault;
                lihatKotak = false;
            }
        }
        /*endiconsearching script*/

        //menghitung waktu bermain puzzle
        if (hitungwaktu == true && solved == false)
        {
            waktu.waktu -= Time.deltaTime;
            progressbar.currentTime = waktu.waktu;
        }
        //jika 

        if (solved == true)
        {
            if (hitungwaktu == true)
            {
                time -= Time.deltaTime;
                if (time < 0)
                {
                    hitungwaktu = false;
                    playermanager.progresplayer[playermanager.nourut].current++;
                    pzm.score++;
                    EndGame();
                }
            }

        }
        if (waktu.waktu < 0.1f)
        {
            //gameover
            hitungwaktu = false;
            audiomanager.popupMetohod(4);
            EndGame();
        }
        //ketika solved bernilai true maka kotak tidak bisa dipindahkan lagi
        if (solved == false && waktu.waktu > 0)
        {
            gantipuzzle();
            int posbenar = 0;
            progressbar.current = 0;
            foreach (var i in tiles)
            {
                if (i != null)
                {
                    if (i.benar == true)
                    {
                        posbenar++;
                        progressbar.current = posbenar * 10;
                    }
                }
            }

            if (posbenar == tiles.Length)
            {
                if (slidingPuzzle == true)
                {
                    audiomanager.slidingPuzzleMetohod(1);
                    slidingPuzzle = false;
                }
                progressbarpuzzle.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0) && waktu.waktu > 0)
            {
                manager();
            }
            if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < boxmanager.Length; i++)
                {
                    boxmanager[i].click = false;
                    boxmanager[i].transform.position = boxmanager[i].defaultPosition.position;
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            keluarPuzzle();
        }

    }

    //ganti puzzle
    private void gantipuzzle()
    {
        if (slidingPuzzle == true)
        {
            //slidingpuzzleobj.SetActive(true);
            iconsearchingObj.SetActive(false);
        }
        else
        {
            slidingpuzzleobj.SetActive(false);
            iconsearchingObj.SetActive(true);
            buttonliat.SetActive(false);
        }
    }

    //manager gerak puzzle
    private void manager()
    {
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit)
        {
            if (slidingPuzzle == true)
            {
                if (Vector2.Distance(emptyspace.position, hit.transform.position) < jarak)
                {
                    audiomanager.slidingPuzzleMetohod(nomorSfx);
                    Vector3 lastPosition = emptyspace.position;
                    Tiles tile = hit.transform.GetComponent<Tiles>();
                    emptyspace.position = tile.posisitarget;
                    tile.posisitarget = lastPosition;
                }
            }
            //icon searching script

            else
            {
                SpriteRenderer box = hit.transform.GetComponent<SpriteRenderer>();
                //script kondisi icon benar
                if (hit.transform.position == poskotak[poskotakbenar].position)
                {
                    for (int i = 0; i < Box.Length; i++)
                    {
                        if (lihatKotak == true & poskotak[i].transform.position != box.transform.position)
                        {
                            if (poskotakbenar != i)
                            {
                                boxmanager[poskotakbenar].click = true;
                                //boxmanager[poskotakbenar].lihat = true;
                            }
                            boxmanager[i].img.sprite = boxmanager[i].kotakdefault;
                            lihatKotak = false;

                        }
                        if (lihatKotak == false && box.transform.position == poskotak[poskotakbenar].transform.position)
                        {
                            if (boxmanager[poskotakbenar].lihat == false)
                            {
                                box.sprite = boxmanager[poskotakbenar].kotakbenar;
                                gantikotak = 0.2f;
                                box.transform.tag = "Box2";
                                lihatKotak = true;
                                audiomanager.slidingPuzzleMetohod(nomorSfx);
                            }
                            else
                            {
                                boxmanager[poskotakbenar].click = true;

                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < boxmanager.Length; i++)
                    {
                        if (lihatKotak == true && poskotak[i].transform.position != box.transform.position)
                        {
                            if (r != i)
                            {
                            }
                            boxmanager[i].img.sprite = boxmanager[i].kotakdefault;
                            lihatKotak = false;

                        }

                        if (lihatKotak == false && box.transform.position == poskotak[i].transform.position)
                        {
                            if (boxmanager[i].lihat == false)
                            {
                                boxmanager[i].click = true;

                                box.sprite = boxmanager[r].kotaksalah;
                                gantikotak = 0.2f;
                                audiomanager.slidingPuzzleMetohod(nomorSfx);
                                lihatKotak = true;
                            }
                            else
                            {
                                boxmanager[i].click = true;
                            }
                        }

                    }
                }
                lihatKotak = true;
            }
        }
    }
    //untuk keluar aplikasi. bisa juga dibuat untuk menghilangkan puzzle nya
    public void keluarPuzzle()
    {
        Application.Quit();
    }
    //tombol pause dan resume
    public void pause()
    {
        if (_pause == false)
        {
            _pause = true;
            hitungwaktu = false;
        }
        else
        {
            _pause = false;
            hitungwaktu = true;
        }
    }
    // method ketika puzzle selesai
    private void EndGame()
    {
        Destroy(this.gameObject);
        //EndPanel.SetActive(true);
    }
    public void liatcontoh()
    {
        if (slidingPuzzle == true)
        {
            //slidingpuzzleobj.SetActive(false);
            audiomanager.buttonclickMethod();
            //contohpuzzle.SetActive(true);

            contohNomerPuzzle[0].SetActive(true);
            contohNomerPuzzle[1].SetActive(true);
            contohNomerPuzzle[2].SetActive(true);
            contohNomerPuzzle[3].SetActive(true);
            contohNomerPuzzle[4].SetActive(true);
            contohNomerPuzzle[5].SetActive(true);
            contohNomerPuzzle[6].SetActive(true);
            contohNomerPuzzle[7].SetActive(true);
            contohNomerPuzzle[8].SetActive(true);
        }
        else
        {
            //contohpuzzle.SetActive(false);
            contohNomerPuzzle[0].SetActive(false);
            contohNomerPuzzle[1].SetActive(false);
            contohNomerPuzzle[2].SetActive(false);
            contohNomerPuzzle[3].SetActive(false);
            contohNomerPuzzle[4].SetActive(false);
            contohNomerPuzzle[5].SetActive(false);
            contohNomerPuzzle[6].SetActive(false);
            contohNomerPuzzle[7].SetActive(false);
            contohNomerPuzzle[8].SetActive(false);
        }
    }
    public void lepasliatcontoh()
    {
        if (slidingPuzzle==true)
        {
            contohNomerPuzzle[0].SetActive(false);
            contohNomerPuzzle[1].SetActive(false);
            contohNomerPuzzle[2].SetActive(false);
            contohNomerPuzzle[3].SetActive(false);
            contohNomerPuzzle[4].SetActive(false);
            contohNomerPuzzle[5].SetActive(false);
            contohNomerPuzzle[6].SetActive(false);
            contohNomerPuzzle[7].SetActive(false);
            contohNomerPuzzle[8].SetActive(false);

            //contohpuzzle.SetActive(false);
            slidingpuzzleobj.SetActive(true);
        }
        else
        {
            contohNomerPuzzle[0].SetActive(false);
            contohNomerPuzzle[1].SetActive(false);
            contohNomerPuzzle[2].SetActive(false);
            contohNomerPuzzle[3].SetActive(false);
            contohNomerPuzzle[4].SetActive(false);
            contohNomerPuzzle[5].SetActive(false);
            contohNomerPuzzle[6].SetActive(false);
            contohNomerPuzzle[7].SetActive(false);
            contohNomerPuzzle[8].SetActive(false);
            //contohpuzzle.SetActive(false);
        }
    }


    public void shuffle() //merandom posisi kotak diawal
    {
        //jika jumlah i lebih kecil dari pada jumlah tiles(kotak) maka akan terjadi perulangan
        for (int i = 0; i < tiles.Length - 1; i++)
        {
            //jika koak yang bernilai i tidak null maka akan terjadi pengacakan
            if (tiles[i] != null)
            {
                int random = Random.Range(1, 2);
                int rand = Random.Range(2, 4);
                var lastpos = tiles[i].posisitarget;
                if (rand < tiles.Length - 1)
                {
                    if (random != i + 1 && rand != i + 1 && random != i && rand != i)
                    {
                        tiles[i].posisitarget = tiles[rand].posisitarget;
                        tiles[rand].posisitarget = tiles[random].posisitarget;
                        tiles[random].posisitarget = lastpos;
                    }

                }
                else
                {
                    tiles[i].posisitarget = tiles[random].posisitarget;
                    tiles[random].posisitarget = lastpos;
                }
                var tile = tiles[i];
                tiles[i] = tiles[rand];
                tiles[rand] = tiles[random];
                tiles[random] = tile;

            }
        }
    }

    public int findindex(Tiles ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}