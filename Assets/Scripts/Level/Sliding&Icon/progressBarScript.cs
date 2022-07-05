using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBarScript : MonoBehaviour
{
    public float maxlenght;
    public float maxlenghtTime;

    public int current;
    public float currentTime=100;

    public Image imgProgressPuzzle;
    public Image imgTimeProgress;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        GetCurrentFillTime();
    }
    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maxlenght;
        imgProgressPuzzle.fillAmount = fillAmount;
    }
    void GetCurrentFillTime()
    {
        float fillAmount = (float)currentTime / (float)maxlenghtTime;
        imgTimeProgress.fillAmount = fillAmount;
    }
}
