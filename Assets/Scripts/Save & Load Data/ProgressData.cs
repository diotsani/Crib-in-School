using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ProgressData
{
    public int lvunlock=1; // awal = 1
    public int lvcomingsoon=3; // tergantung lv yang telah dibuat
    //setting
    public int qualityvalue=1;
    public float volumebgm=100;
    public float volumesfx=100;
    //skill
    public int[] lvskill= { 1, 0, 0 };
    public float skill1=0;//menambah durasi frezzer
    public float skill2=0;//menambah kecepatan guru
    public float skill3=0;//menambah kecepatan pesawat saat menghilang
    public bool[] lockskill = { false, true, true };//menyimpan progress skill yang terkunci

    //exp dan koin
    public float expPlayer=0;//jumlah exp pemain // awal = 0
    public int koin;//jumlah koin pemain

}
