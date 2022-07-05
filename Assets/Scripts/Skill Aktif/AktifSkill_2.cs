using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class AktifSkill_2 : MonoBehaviour
{
    PuzzleManager pzm;
    //audio sfx
    SoundManager sfx;

    public static Action<Vector3> OnDistraction;

    //public NavMeshAgent agentGuru;
    public Gamemanager gm;
    public Transform transformBatu;
    public GameObject plane;

    public Image darkMaskCooldown;
    public Image activedMaskSkill;

    public Text textCooldownActived; //text cooldown saat skill telah aktif
    public Text textCooldownSpawn; //text cooldown durasi skill
    public Text skill2Text;

    public float timeDistractDefault = 10f;
    private float timeDistract;
    private float timeAktifDistract = 0;

    public float durasispawnDefault = 90;
    private float durasispawn;
    public float speedguru;//update speed

    public GameObject batu;

    public bool skillAktif;
    public bool distractSkill;

    private bool _distracted;
    public bool spawnObject = false;
    public GameObject spawnGameObject;

    public Skill1 skill1;
    public GameObject bintang;
    public Transform parrent;
    public int lvskill;
    public bool gotobatu;


    //exp dan koin
    public bool lockskill;
    public GameObject skillTerkunci;
    public GameObject tombolUpgrade;
    public SkillManager skm;

    public bool click;

    public void Awake()
    {
        sfx = FindObjectOfType<SoundManager>();
        skm = FindObjectOfType<SkillManager>();
        lockskill = UserDataManager.Progress.lockskill[1];
        if (lockskill == false)
        {
            skillTerkunci.SetActive(false);
            //tombolUpgrade.SetActive(true);
        }

        if (lockskill == true)
        {
            tombolUpgrade.SetActive(false);
        }
        speedguru = speedguru + UserDataManager.Progress.skill2;
        lvskill = UserDataManager.Progress.lvskill[1];
        for (int i = 0; i < lvskill; i++)
        {
            //Instantiate(bintang, parrent);
        }
        //agentGuru = GameObject.FindGameObjectWithTag("Guru(Clone)").GetComponent<NavMeshAgent>();
        gm = FindObjectOfType<Gamemanager>();
    }

    public void Start()
    {
        textCooldownActived.enabled = false;
        if (gm != null)
        {
            transformBatu = FindInActiveObjectByTag("Penghapus").GetComponent<Transform>();
            pzm = FindObjectOfType<PuzzleManager>();
            skillAktif = true;
            timeDistract = timeDistractDefault;
            durasispawn = durasispawnDefault;
            textCooldownActived.enabled = false;
            tombolUpgrade.SetActive(false);
            skill2Text = FindInActiveObjectByName("Skill 2 Text").GetComponent<Text>();
        }
        //OnDistraction += ThrowCoin;
        OnDistraction += ThrowCoin;
        AktifSkill_2.OnDistraction += GetDistracted;
    }

    public void Update()
    {
        //AktifSkill_2.OnDistraction += GetDistracted;

        skill1 = FindObjectOfType<Skill1>();
        gm = FindObjectOfType<Gamemanager>();
        pzm = FindObjectOfType<PuzzleManager>();
        sfx = FindObjectOfType<SoundManager>();


        if (distractSkill == true)
        {
            //textCooldownSpawn.gameObject.SetActive(false);
            darkMaskCooldown.gameObject.SetActive(false);
            skill1 = FindInActiveObjectByName("Skill1").GetComponent<Skill1>();
            //agentGuru = FindInActiveObjectByName("Guru(Clone)").GetComponent<NavMeshAgent>();

            if (timeDistract > 0.1f)
            {
                //skill2Text.gameObject.SetActive(true);
                timeDistract -= Time.deltaTime;
                timeAktifDistract += Time.deltaTime;
                float roundedCd = Mathf.Round(timeDistract);
                //textCooldownActived.text = roundedCd.ToString();
                //skill1.maskSkillactive.enabled = true;

                activedMaskSkill.gameObject.SetActive(true);
                activedMaskSkill.enabled = true;
                activedMaskSkill.fillAmount = (timeAktifDistract / timeDistractDefault);

                

                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {

                        if (hitInfo.collider.tag == "Plane")
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                if (click==true)
                                {
                                    transformBatu = FindInActiveObjectByTag("Penghapus").GetComponent<Transform>();
                                    skill2Text.gameObject.SetActive(false);
                                    sfx.powerskillmethod(1);
                                    gotobatu = true;
                                    click = false;
                                    if (!(OnDistraction is null))
                                        OnDistraction(hitInfo.point);
                                }
                                
                            }
                        }

                    }
                }
            }
            else
            {
                skill2Text.gameObject.SetActive(false);
                distractSkill = false;
                timeDistract = timeDistractDefault;
                timeAktifDistract = 0;
                //spawnGameObject = FindInActiveObjectByTag("Batu");
                //Destroy(spawnGameObject, 5);
                //textCooldownActived.enabled = false;
                //textCooldownSpawn.gameObject.SetActive(true);

                activedMaskSkill.gameObject.SetActive(false);
                activedMaskSkill.rectTransform.sizeDelta = new Vector2(79f, 67f);

                darkMaskCooldown.gameObject.SetActive(true);
                darkMaskCooldown.rectTransform.sizeDelta = new Vector2(79f, 67f);
            }
        }

        if (skillAktif == false)
        {
            if (durasispawn > 0.1f)
            {
                //skill1.lockimg.enabled = true;
                durasispawn -= Time.deltaTime;

                //textCooldownSpawn.enabled = true;
                float roundedCd = Mathf.Round(durasispawn);
                //textCooldownSpawn.text = roundedCd.ToString();

                darkMaskCooldown.enabled = true;
                darkMaskCooldown.fillAmount = (durasispawn / durasispawnDefault);
            }
            else
            {
                skillAktif = true;
                darkMaskCooldown.enabled = false;
                durasispawn = durasispawnDefault;
                spawnObject = false;
            }
        }
    }

    private void FixedUpdate()
    {

    }

    

    public void Distract()
    {

        if (gm != null)
        {
            skill2Text.gameObject.SetActive(true);

            tombolUpgrade.SetActive(false);

            sfx.buttonclickMethod();

            if (skillAktif == true)
            {
                distractSkill = true;
                click = true;
                skillAktif = false;
                //textCooldownActived.enabled = true;
            }
            else
            {
                print("proses");
            }
            if (pzm.player.munculPuzzle == true)
            {
                pzm.keluar();
            }
        }
        else
        {
            print("balm ada gm");
        }
    }

    void activeSkills()
    {
        distractSkill = false;
    }

    void activeButtonSkills()
    {
        skillAktif = true;
    }

    public void ThrowCoin(Vector3 pos)
    {
        if (spawnObject == false)
        {
            spawnGameObject = Instantiate(batu, pos, Quaternion.identity);
            spawnObject = true;
            //return;
        }

    }

    public void OnDestroy()
    {
        AktifSkill_2.OnDistraction -= GetDistracted;
        OnDistraction -= ThrowCoin;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_distracted)
            return;
    }

    public void GetDistracted(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) <= 15f)
        {
            StopAllCoroutines();
            _distracted = true;
            //agentGuru.SetDestination(pos);
            StartCoroutine(FollowDistraction(pos));
        }
    }

    public void unlockskillmethod()
    {
        if (skm != null)
        {
            if (skm.koin >= 1)
            {
                sfx.powerskillmethod(0);

                UserDataManager.Progress.lvskill[1]++;
                lvskill++;
                //Instantiate(bintang, parrent);

                UserDataManager.Progress.lockskill[1] = false;
                skillTerkunci.SetActive(false);
                //tombolUpgrade.SetActive(true);
                lockskill = false;
                skm.koin -= 1;
                UserDataManager.Progress.koin -= 1;
            }
            UserDataManager.Save();
        }
    }

    public void upgradeskill()//method upgrade skill dan simpan progress skill
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

                    UserDataManager.Progress.lvskill[1]++;
                    lvskill++;
                    speedguru += 0.2f;
                    UserDataManager.Progress.skill2 += 0.2f;
                    Instantiate(bintang, parrent);
                    UserDataManager.Save();
                }
                else
                {
                    print("skill penuh");
                }
            }
            else
            {
                print("Koin Kurang");
            }
        }

    }
    public IEnumerator FollowDistraction(Vector3 pos)
    {
        while (Vector3.Distance(transform.position, pos) > 2f)
            yield return null;
        //agentGuru.SetDestination(transform.position);
    }

    GameObject FindInActiveObjectByTag(string tag) //fungsi mencari object yang tidak aktif menggunakan tag
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {

                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    GameObject FindInActiveObjectByName(string name) //fungsi mencari object yang tidak aktif menggunakan nama
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
