using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Sprite[] tutorialimg;
    public Image tutorialobj;
    public int urutan;
    //setting manager;
    [SerializeField] private setting seting;
    // Start is called before the first frame update
    private void Awake()
    {
        seting = FindObjectOfType<setting>();
        
    }
    void Start()
    {
        tutorialobj.sprite = tutorialimg[urutan];
    }

    // Update is called once per frame
    void Update()
    {
        tutorialobj.sprite = tutorialimg[urutan];
    }
    public void next()
    {
        seting.sfx.buttonclickMethod();
        if (urutan < tutorialimg.Length-1)
        {
            urutan++;
        }
        else if(urutan==tutorialimg.Length-1)
        {
            urutan = 0;
        }
    }
    public void back()
    {
        seting.sfx.buttonclickMethod();

        if (urutan > 0)
        {
            urutan--;
        }
        else if(urutan==0)
        {
            urutan = tutorialimg.Length-1;
        }
    }
    public void close()
    {
        seting.sfx.buttonclickMethod();
        this.gameObject.SetActive(false);
    }
    public void open()
    {
        this.gameObject.SetActive(true);
        seting.sfx.buttonclickMethod();
    }
}
