using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    public GameObject panel;
    public GameObject about;
    //sound
    public Slider volumesetting;
    public Slider volumesettingsfx;
    public BgmManager bgm;
    public SoundManager sfx;

    //QUALITY   setting
    public Dropdown quality;
    public int lvquality=3;
    //popup   setting
    public Dropdown settingpopuppos;
    public GameObject poskanan;
    public GameObject poskiri;

    private void Awake()
    {
        bgm = FindObjectOfType<BgmManager>();
        sfx = FindObjectOfType<SoundManager>();
        poskanan = GameObject.FindGameObjectWithTag("popupbtn");
        poskiri = GameObject.FindGameObjectWithTag("popupbtn2");
    }
    // Start is called before the first frame update
    void Start()
    {
        quality.value = UserDataManager.Progress.qualityvalue;
        //volumesetting.value = bgm.maxvol; 
        volumesetting.maxValue = bgm.maxvol;
        volumesetting.value = UserDataManager.Progress.volumebgm;
        volumesettingsfx.value = UserDataManager.Progress.volumesfx;
        //volumesettingsfx.value = sfx.maxvol; 
        volumesettingsfx.maxValue = 1;
    }
    public void optionex()
    {
        UserDataManager.Save();
        sfx.buttonclickMethod();
        panel.SetActive(false);
    }
    public void About()
    {
        sfx.buttonclickMethod();
        panel.SetActive(false);
        about.SetActive(true);
    }
    public void AboutEx()
    {
        sfx.buttonclickMethod();
        panel.SetActive(true);
        about.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        UserDataManager.Progress.volumebgm = volumesetting.value;
        UserDataManager.Progress.volumesfx = volumesettingsfx.value;
        bgm.volume = volumesetting.value;
        sfx.volumeafx = volumesettingsfx.value;
        qualitysetting();
        popupsetting();
       
    }
    void qualitysetting()
    {
        if (quality.captionText.text == "LOW")
        {
            lvquality = 1;
            UserDataManager.Progress.qualityvalue = 2;
        }
        else if (quality.captionText.text == "MEDIUM")
        {
            lvquality = 2;
            UserDataManager.Progress.qualityvalue = 1;

        }
        else if (quality.captionText.text == "HIGH")
        {
            lvquality = 3;
            UserDataManager.Progress.qualityvalue = 0;

        }
        //UserDataManager.Progress.qualityvalue = lvquality;
        PlayerPrefs.SetInt("masterQuality", lvquality);
        QualitySettings.SetQualityLevel(lvquality);
    }
    void popupsetting()
    {
        if (settingpopuppos.captionText.text=="KANAN")
        {
            poskiri.SetActive(false);
            poskanan.SetActive(true);
        }
        else if (settingpopuppos.captionText.text=="KIRI")
        {
            poskanan.SetActive(false);
            poskiri.SetActive(true);
        }
    }
}
