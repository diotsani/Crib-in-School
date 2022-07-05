using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Selection : MonoBehaviour
{
    public EquipmentSlot equipSlotSkill_1;
    //public EquipmentSlot skill_2;
    SoundManager sfx;
    BgmManager bgm;
    public int selectSkill_1;
    //public int selectSkill_2 = 0;

    public void Awake()
    {
        sfx = FindObjectOfType<SoundManager>();
        bgm = FindObjectOfType<BgmManager>();
    }

    public void Update()
    {
        selectSkill_1 = equipSlotSkill_1.nomer;
        //selectSkill_2 = skill_2.nomer;
    }

    public void StartGame()
    {
        sfx.buttonclickMethod();
        if (equipSlotSkill_1.nomer!=0)
        {
            PlayerPrefs.SetInt("selectSkill_1", selectSkill_1);
            SceneManager.LoadScene("lodingScane", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("pilih salah satu skill");
        }

        //PlayerPrefs.SetInt("selectSkill_2", selectSkill_2);
    }
    public void home()
    {
        sfx.buttonclickMethod();
        SceneManager.LoadScene(1);
        bgm.bgm.Stop();
    }
}
