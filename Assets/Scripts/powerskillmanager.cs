using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class powerskillmanager : MonoBehaviour
{
    public BgmManager bgm;
    private void Awake()
    {
        bgm = FindObjectOfType<BgmManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void home(string screenname)
    {
        SceneManager.LoadScene(screenname);
        bgm.bgm.Stop();
    }
    public void startgame(string screenName)
    {
        SceneManager.LoadScene(screenName);
        bgm.bgm.Stop();
    }
}
