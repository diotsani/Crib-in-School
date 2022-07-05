using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public AudioClip[] bgmclip;//kusus bgm game
    public AudioSource bgm;
    public float maxvol=0.4f;
    public float volume;
    // Start is called before the first frame update
    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
        if (bgm==null)
        {
            print("tidak ada");
        }
    }
    void Start()
    {
        bgm.playOnAwake=true;
        bgm.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        bgm.volume = volume;
    }
    public void bgmMethod(int nomorbgm)
    {
        /*keterangan :
        *nomorbgm 0 bgm class
        * nomorbgm 1 blm dimasukan 
        */
        //bgm.PlayOneShot(bgmclip[nomorbgm]);
        if (bgm!=null)
        {
            bgm.clip = bgmclip[nomorbgm];
            bgm.Play();
        }
    }
}
