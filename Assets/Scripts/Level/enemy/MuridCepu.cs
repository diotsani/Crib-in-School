using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuridCepu : MonoBehaviour
{
    public Animator anim;
    public bool ketahuan;
    [SerializeField] private player playermanager;
    // Start is called before the first frame update
    void Start()
    {
        playermanager = FindObjectOfType<player>();
        ketahuan = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playermanager.ketahuan2==true)
        {
           anim.SetBool("Lapor", true);
        }
    }
}
