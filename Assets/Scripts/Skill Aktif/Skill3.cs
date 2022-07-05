using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill3 : MonoBehaviour
{
    //audio script
    SoundManager sfx;
    //exp dan koin
    public bool lockskill;
    public GameObject skillTerkunci;
    public GameObject tombolUpgrade;
    public SkillManager skm;


    public float timeinvisibleDefault = 30f;
    private float timeinvisible;
    private float timeAktifInvisible = 0;

    public float speedpesawat=0.2f;

    public float durasispawnDefault = 90;
    private float durasispawn;

    public bool skillaktif;
    public bool invisible;
    public bool playskill;
    public Gamemanager gm;

    public Image maskSpawn;
    public Image maskSkillactive;

    public GameObject bintang;
    public Transform parrent;
    public int lvskill;
    private void Awake()
    {
        
        sfx = FindObjectOfType<SoundManager>();
        skm = FindObjectOfType<SkillManager>();
        lockskill = UserDataManager.Progress.lockskill[2];
        if (lockskill == false)
        {
            skillTerkunci.SetActive(false);
            //tombolUpgrade.SetActive(true);
        }
        if (lockskill == true)
        {
            tombolUpgrade.SetActive(false);
        }
        speedpesawat = speedpesawat+ UserDataManager.Progress.skill3;
        lvskill = UserDataManager.Progress.lvskill[2];
        for (int i = 0; i < lvskill; i++)
        {
            //Instantiate(bintang, parrent);
        }
        maskSpawn.enabled = false;
        maskSkillactive.enabled = false;
        gm = FindObjectOfType<Gamemanager>();
        if (gm != null)
        {
            tombolUpgrade.SetActive(false);
            maskSpawn.fillAmount = durasispawnDefault;
            skillaktif = true;
            timeinvisible = timeinvisibleDefault;
            durasispawn = durasispawnDefault;
            //frezee();
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sfx = FindObjectOfType<SoundManager>();

        if (invisible == true)
        {
            if (timeinvisible > 0.1f)
            {
                timeinvisible -= Time.deltaTime;
                timeAktifInvisible += Time.deltaTime;
            }
            else
            {
                maskSkillactive.enabled = false;
                invisible = false;
                timeinvisible = timeinvisibleDefault;
                maskSpawn.rectTransform.sizeDelta = new Vector2(79f, 67f);
                maskSpawn.enabled = true;
                timeAktifInvisible = 0;
            }
        }
        if (skillaktif == false)
        {
            if (durasispawn > 0.1f)
            {
                durasispawn -= Time.deltaTime;
            }
            else
            {
                maskSpawn.enabled = false;
                skillaktif = true;
                playskill = false;
                durasispawn = durasispawnDefault;
            }
        }

        maskSpawn.fillAmount = durasispawn / durasispawnDefault;
        maskSkillactive.fillAmount = timeAktifInvisible / timeinvisibleDefault;

    }
    public void Invisible()//method tombol untuk invisible
    {
        if (gm != null)
        {

            sfx.buttonclickMethod();

            if (skillaktif == true)
            {
                maskSkillactive.rectTransform.sizeDelta = new Vector2(79f, 67f);
                maskSkillactive.enabled = true;
                invisible = true;
                skillaktif = false;
            }
            else
            {
                print("proses");
            }
        }
        else
        {
            print("balm ada gm");
        }
    }
    void activeSkills()//mengaktifkan skill
    {
        invisible = false;
    }
    void activeButtonSkills()//mengaktifkan button skill
    {
        skillaktif = true;
    }


    public void unlockskillmethod()//unlock skill
    {
        if (skm != null)
        {

            if (skm.koin >= 1)
            {
                sfx.powerskillmethod(0);
                UserDataManager.Progress.lvskill[2]++;
                lvskill++;
                //Instantiate(bintang, parrent);

                UserDataManager.Progress.lockskill[2] = false;
                skillTerkunci.SetActive(false);
                lockskill = false;
                //tombolUpgrade.SetActive(true);
                skm.koin -= 1;
                UserDataManager.Progress.koin -= 1;
            }
            UserDataManager.Save();
        }
    }
    public void upgrade()
    {
        if (lockskill == false)
        {
            if (skm.koin >= 1)
            {
                if (lvskill < 3)
                {
                    sfx.powerskillmethod(1);

                    skm.koin -= 1;
                    UserDataManager.Progress.koin -= 1;

                    UserDataManager.Progress.lvskill[2]++;
                    lvskill++;
                    speedpesawat += 0.5f;
                    UserDataManager.Progress.skill3 += 0.5f;
                    Instantiate(bintang, parrent);
                    UserDataManager.Save();
                }
                else
                {
                    print("Lv penuh");
                }
            }
            else
            {
                print("Koin Kurang");
            }
        }

        
    }
}
