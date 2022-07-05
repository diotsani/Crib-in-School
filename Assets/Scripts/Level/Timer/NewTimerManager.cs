using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class NewTimerManager : MonoBehaviour
{
    
    public float waktu;
    public int menit;
    public Text timeText;

    public float maxLenghtTime;
    public float currentTime;

    public Image imageTimeFill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFilltime();

        TimeSpan time = TimeSpan.FromSeconds(waktu);
        timeText.text = "0" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void GetCurrentFilltime()
    {
        float fillAmount = (float)currentTime / (float)maxLenghtTime;
        imageTimeFill.fillAmount = fillAmount;
    }
}
