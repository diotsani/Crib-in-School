using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarPlayer : MonoBehaviour
{
    public float maxlenght = 4;

    public int current = 0;

    public Image imgProgressPuzzle;
    public endManager panelscript;
    public bool selesai;

    void Start()
    {
        selesai = false;
        panelscript = FindObjectOfType<endManager>();
    }

    void Update()
    {
        
        if (imgProgressPuzzle.fillAmount>=maxlenght/0.8f)
        {
            panelscript.totallulus += 1;
        }
        GetCurrentFill();

    }
    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maxlenght;
        imgProgressPuzzle.fillAmount = fillAmount;
    }
}
