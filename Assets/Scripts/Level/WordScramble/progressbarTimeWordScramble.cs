using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class progressbarTimeWordScramble : MonoBehaviour
{
    public float maxlenghtTime;
    public float currentTime = 100;
    public Image timeprogress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFillTime();
    }
    void GetCurrentFillTime()
    {
        float fillAmount = (float)currentTime / (float)maxlenghtTime;
        timeprogress.fillAmount = fillAmount;
    }
}
