using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] Timer timer1;

    public GameObject pausePanel;

    public GameObject puzzleManager;

    bool aktif;
    bool nonaktif;

    private void Awake()
    {
        BeginPause();
    }

    private void Start()
    {
        aktif = true;
        nonaktif = false;

        timer1.SetDuration(120).Begin();
    }

    public void Update()
    {
        if (Input.GetKeyUp("p"))
        {
            //pausePanel.SetActive(true);
            timer1.SetPaused(!timer1.IsPaused);
        }
    }

    public void BeginPause()
    {
        //if (puzzleManager.SetActive(false))
        {
            //timer1.SetPaused(!timer1.IsPaused);
        }
    }
}
