using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class characterscript : MonoBehaviour
{
    public bool pencontekawal;
    public GameObject tanda;
    public Transform posisi;
    //animasi setting
    public Animator anim;

    //public bool dipilih;
    public int jumlah;
    //public Marker marker_0;
    //public Marker marker_1;


    [SerializeField] private player playermanager;

    private void Start()
    {
        
        //dipilih = false;
        playermanager = FindObjectOfType<player>();

        //marker_1 = GameObject.Find("Marker 1").GetComponent<Marker>();
        //marker_0 = GameObject.Find("Marker 2").GetComponent<Marker>();

        //tanda.SetActive(false);
       // manager();
    }
    private void manager()
    {
        for (int i = 0; i < playermanager.jumlahPlayer; i++)
        {
            if (pencontekawal == true)
            {
                transform.gameObject.tag = "Player";
            }
        }
       
    }
    private void Update()
    {
        manager();

        if (playermanager.ketahuan==true)
        {
            anim.SetBool("Ketahuan", true);
        }
        if (playermanager.munculPuzzle == true)
        {
            anim.SetBool("Noleh", false);
        }
        else
        {
            anim.SetBool("Noleh", true);
        }
        

        //if (marker_1.markerAktif == true)
        //{
        //    anim.SetBool("Noleh", true);
        //}

        //if (marker_1.markerAktif == false)
        //{
            //anim.SetBool("Noleh", false);
        //}
    }
}
