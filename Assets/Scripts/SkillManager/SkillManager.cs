using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Text kointxt;
    public Text exptxt;
    //expscript
    public int koin = 1;
    public int exprequitment = 500;
    public float expPemain;

    public static SkillManager instance;
    public Skill[] skills;
    public SkillButton[] skillButton;

    public Skill activateSkill;

    public void Awake()
    {
        koin = UserDataManager.Progress.koin;
        expPemain = UserDataManager.Progress.expPlayer;
        kointxt.text = koin+" Poin";
        exptxt.text = expPemain + "/" + exprequitment + " exp";

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        kointxt.text = koin+" Poin";
        exptxt.text = expPemain + "/" + exprequitment + " exp";
        if (expPemain >= exprequitment)
        {
            UserDataManager.Progress.expPlayer -= exprequitment;
            expPemain -= exprequitment;
            UserDataManager.Progress.koin++;
            koin++;
        }
    }
}
