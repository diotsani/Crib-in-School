using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelmanager : MonoBehaviour
{
    public int nomorlv;
    public int urutanNumber;
    public GameObject lvpanel;
    public bool[] lvUnlock;

    public bool[] lvcomingsoon;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < UserDataManager.Progress.lvunlock; i++)
        {
            lvUnlock[i] = true;
        }
        for (int i = 0; i < UserDataManager.Progress.lvcomingsoon; i++)
        {
            lvcomingsoon[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
