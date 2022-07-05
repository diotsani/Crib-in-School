using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pesawatKetahuan : MonoBehaviour
{
    public player playermanager;
    public GameObject _endPanel;
    public endManager panelmanager;

    //audio manager
    private SoundManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="deteksi pesawat")
        {

            audiomanager.resultMethod(1);
            print("ketahuan");
            Destroy(playermanager.pesawat);
            panelmanager.ketahuan = true;
            _endPanel.SetActive(true);
        }
    }
}
