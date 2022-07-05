using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //mengambil dari script player
    public player player;
    //untuk menentukan kamera puzzle
    public Camera cam;
    //untuk menentukan level permainan berapa
    public int levelpermainan;

    /*kamera gamepalay 
     * yang atas tag main camera bertujuan untuk select player
     * yang bawah tag default berfungsi agar puzzle yang menggunakan sistem kamera dapat digunakan
    */
    public GameObject gamePlayCamera;
    public GameObject gamePlaycamera2;
    public GameObject cameralose;
    //setiap puzzle yang diselesaikan akan menambah skor atau dengan katalain jumlah puzzle yang telah diselesaikan dengan benar
    public int score = 0;

    //bertujuan mengambil prefab sliding puzzle
    /*
     * menggunakan array bertujuan jika ingin menggunakan sliding puzzle dengan jumlah selain 3x3 dapat memasukan prefab kesini
     */
    public GameObject[] SlidingpuzzlePrefabs;
    public slidingpuzzlescript[] slidingscript;

    public GameObject[] _WordScramble;
    public QuizManager[] wordScrambleScript;

    public GameObject[] wordSearchingPref;
    public WordChecker[] WordSearchingScript;

    public Transform perent;/*berfungsi tempan instance atau tempat munculnya puzzle*/
    //nomor puzzle
    public int PuzzleNUmber;
    public int jumlahPuzzle;
    //nomor urutan cabang puzzle
    public int urutanPuzzle;

    public bool munculpuzzle;
    //fungsi dimana hitung score dapat digunakan
    public bool hitungScore;

    public Animator puzzleanim;

    //memunculkan btn popup
    public GameObject btnPopuppuzzle;

    //menghitung jumlah soal seluruh puzzle
    public int jumlahSoal;
    public int jumlahseluruhpuzzle;

    //end game
    public bool solvedPuzzle;
    public GameObject EndPanel;
    public endManager end;

    private void Start()
    {
        solvedPuzzle = false;
        jumlahSoal = 0;
        puzzleanim = GetComponent<Animator>();/*mengambil komponen animator*/
        PuzzleNUmber = Random.Range(0,jumlahPuzzle);
        SlidingpuzzlePrefabs[urutanPuzzle].SetActive(true);
        munculpuzzle = true;
        hitungScore = false;
    }
    public void PilihanLevel(int lvke)//untuk menambah puzzle tambahkan disini jika berbeda level
    {
        if (lvke==1)
        {
            if (PuzzleNUmber == 0)
            {
                cam.enabled = false;
                slidingPuzzle(0);
            }
            else if (PuzzleNUmber == 1)
            {
                cam.enabled = false;
                WordScramble(0);

            }
            else if (PuzzleNUmber == 2)
            {
                cam.enabled = false;
                WordSearching(0);
            }

            else if (PuzzleNUmber == 3)
            {
                cam.enabled = false;

                WordScramble(1);
            }
            else
            {
                if (jumlahseluruhpuzzle == 0)
                {
                    solved();
                }
                else if (jumlahseluruhpuzzle >= 1 && PuzzleNUmber != 0)
                {
                    PuzzleNUmber = 0;
                }

            }
        }
        else if (lvke==2)
        {
            if (PuzzleNUmber == 0)
            {
                cam.enabled = false;
                slidingPuzzle(0);
            }
            else if (PuzzleNUmber == 1)
            {
                cam.enabled = false;
                WordScramble(0);

            }
            else if (PuzzleNUmber == 2)
            {
                cam.enabled = false;
                WordSearching(0);
            }
            else if (PuzzleNUmber == 3)
            {
                cam.enabled = false;
                slidingPuzzle(1);
            }
            else if (PuzzleNUmber == 4)
            {
                cam.enabled = false;
                WordSearching(1);
                
            }
            else
            {
                if (jumlahseluruhpuzzle == 0)
                {
                    solved();
                }
                else if (jumlahseluruhpuzzle >= 1 && PuzzleNUmber != 0)
                {
                    PuzzleNUmber = 0;
                }

            }
        }
        else if (lvke==3)
        {
            if (PuzzleNUmber == 0)
            {
                cam.enabled = false;
                slidingPuzzle(0);
            }
            else if (PuzzleNUmber == 1)
            {
                cam.enabled = false;
                WordScramble(0);

            }
            else if (PuzzleNUmber == 2)
            {
                cam.enabled = false;
                WordSearching(0);
            }
            else if (PuzzleNUmber == 3)
            {
                cam.enabled = false;
                slidingPuzzle(1);
            }
            else if (PuzzleNUmber == 4)
            {
                cam.enabled = false;
                WordSearching(1);
                
            }
            else
            {
                if (jumlahseluruhpuzzle == 0)
                {
                    solved();
                }
                else if (jumlahseluruhpuzzle >= 1 && PuzzleNUmber != 0)
                {
                    PuzzleNUmber = 0;
                }
            }
        }
       
        if (player.munculPuzzle == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                keluar();
            }
        }
    }
    private void Update()
    {
        PilihanLevel(levelpermainan);
        //end.jawabanBenar = score;
       
    }
    //=============================================================================================================================
    private void WordScramble(int urutan)
    {
        wordScrambleScript[urutan].pzm = GetComponent<PuzzleManager>();
        if (munculpuzzle == true)
        {
            munculpuzzle = false;
            instance(_WordScramble,urutan);
        }
        if (GameObject.Find(_WordScramble[urutan].name + "(Clone)") == null)
        {
            jumlahseluruhpuzzle -= 1;
            if (jumlahseluruhpuzzle > 0)
            {
                PuzzleNUmber += 1;
            }
            else if (jumlahseluruhpuzzle < 1)
            {
                PuzzleNUmber += jumlahPuzzle + 1;
            }
            munculpuzzle = true;
        }
    }    
    //=============================================================================================================================
    private void slidingPuzzle(int urutan)
    {
        slidingscript[urutan].pzm = GetComponent<PuzzleManager>();
        if (munculpuzzle == true)
        {

            munculpuzzle = false;
            instance(SlidingpuzzlePrefabs,urutan);

        }
        if (GameObject.Find(SlidingpuzzlePrefabs[urutan].name + "(Clone)") == null)
        {
            //score += 1;
            jumlahseluruhpuzzle -= 1;
            if (jumlahseluruhpuzzle > 0)
            {
                PuzzleNUmber += 1;
            }
            else if (jumlahseluruhpuzzle < 1)
            {
                PuzzleNUmber += jumlahPuzzle + 1;
            }
            munculpuzzle = true;
            
        }
      
    }
    //=============================================================================================================================
    private void WordSearching(int urutan)
    {
        WordSearchingScript[urutan].pzm = GetComponent<PuzzleManager>();
        if (munculpuzzle == true)
        {
            munculpuzzle = false;
            Instantiate(wordSearchingPref[urutan], perent);

        }
        else if (GameObject.Find(wordSearchingPref[urutan].name + "(Clone)") == null)
        {
            jumlahseluruhpuzzle -= 1;
            if (jumlahseluruhpuzzle > 0)
            {
                PuzzleNUmber += 1;
            }
            else if (jumlahseluruhpuzzle < 1)
            {
                PuzzleNUmber += jumlahPuzzle + 1;
            }
            munculpuzzle = true;
        }
      
    }
    private void instance(GameObject[] jenis,int urutan)
    {
        Instantiate(jenis[urutan], perent);
    }
    public void keluar()
    {
        gamePlaycamera2.SetActive(false);
        gamePlayCamera.SetActive(true);
        gameObject.SetActive(false);
        
        player.munculPuzzle = false;
        if (solvedPuzzle == false)
        {
            btnPopuppuzzle.SetActive(true);
        }
    }
    public void keluarketahuan()
    {
        cameralose.SetActive(true);
        gamePlaycamera2.SetActive(false);
        player.munculPuzzle = false;
        if (solvedPuzzle == false)
        {
            btnPopuppuzzle.SetActive(false);
        }
        gameObject.SetActive(false);

    }
    public void solved()
    {
        solvedPuzzle = true;
        player.jumlahPlayer--;
        player.progresplayer[PesawatScript.FindObjectOfType<PesawatScript>().progress1].selesai = true;
        end.nilaitertinggi = jumlahSoal;
        end.jawabanBenar = score;
        gamePlaycamera2.SetActive(false);
        gamePlayCamera.SetActive(true);
        gameObject.SetActive(false);
        end.totallulus += 1;
        //if (solvedPuzzle==false)
        //{
        //    btnPopuppuzzle.SetActive(true);
        //}
        player.munculPuzzle = false;
        //Invoke("solvedPuzzleResult", 0.5f);
    }
    void solvedPuzzleResult()
    {
        EndPanel.SetActive(true);
    }
}
