using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //panel
    public GameObject settingpannel;
    //audio
    private BgmManager bgm;
    private SoundManager sfx;
    public GameObject bgmo;
    public GameObject audiomanager;
    public setting settingmanager;
    public int nomorbgm;
    //about
    public GameObject about;
    //popip setting
    public GameObject popupPos;
    //help
    public GameObject help;
    //lvselect
    public levelmanager level;
    public GameObject levelmanagerobj;

    private void Awake()
    {
        level = FindObjectOfType<levelmanager>();
        bgm = FindObjectOfType<BgmManager>();
        sfx = FindObjectOfType<SoundManager>();
        settingmanager = FindObjectOfType<setting>();
    }
    private void Start()
    {
        bgm.bgmMethod(nomorbgm);
        if (settingpannel!=null)
        {
            settingpannel.SetActive(false);
        }
        else
        {
            print("tidak ada");
        }

    }
    public void LoadScene(string screenName)
    {
        sfx.buttonclickMethod();
        SceneManager.LoadScene(screenName);
        //setting
        //levelobj.SetActive(false);
        DontDestroyOnLoad(levelmanagerobj);
        DontDestroyOnLoad(bgmo);
        DontDestroyOnLoad(about);
        DontDestroyOnLoad(audiomanager);
        DontDestroyOnLoad(settingmanager);
    }
    public void home(string screenName)
    {
        sfx.buttonclickMethod();
        SceneManager.LoadScene(screenName);
        bgm.bgm.Stop();
    }
    public void start(string screenName)
    {
        sfx.buttonclickMethod();
        SceneManager.LoadScene(screenName);
        bgm.bgm.Stop();
    }
    public void option()
    {
        sfx.buttonclickMethod();
        settingmanager.panel.SetActive(true);
    }
   
    public void Exit()
    {
        sfx.buttonclickMethod();
        Application.Quit();
        
    }
    void exitmethod()
    {

    }
    public void btnpertama(string lv)
    {
        SceneManager.LoadScene(lv + " " + (level.nomorlv + level.urutanNumber).ToString());
        UserDataManager.Load();
    }
}
