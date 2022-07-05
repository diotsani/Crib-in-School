using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] wayPoint;
    int wayPoitIndex;
    Vector3 target;
    bool step;
    public float defaultspeed = 1f;
    //audio manager
    private SoundManager audiomanager;
    private float volume;
    bool mendekat;
    //player manager
    private player playermanager;
    //animasi
    public Animator animasiGuru;

    //skill setting;
    // random
    public int random;//selain angka 0guru akan diam
    public float time;//durasi guru diam
    public float Defaulttime = 3f;//durasi guru diam
    public Skill1 skill1;
    public GameObject pusing3D;
    public bool pusing;
    public GameObject fov;

    public AktifSkill_2 skill_2;
    public Transform targetDistract;


    public float defauldspeed = 0.3f;
    void Start()
    {
        animasiGuru.SetFloat("speed", 1);

        animasiGuru.SetBool("isWalking", true);

        time = Defaulttime;
        skill1 = FindObjectOfType<Skill1>();

        skill_2 = FindObjectOfType<AktifSkill_2>();

        volume = 0.3f;
        step = true;
        audiomanager = FindObjectOfType<SoundManager>();
        playermanager = FindObjectOfType<player>();

        agent = GetComponent<NavMeshAgent>();
        random = 0;
        agent.speed = defauldspeed;

        UpdateDestination();

        pusing = false;
        fov.SetActive(true);
        pusing3D.SetActive(false);
    }

    void Update()
    {
        if (step == true)
        {
            Invoke("stepSound", 2.3f - agent.speed);
            step = false;
        }
        else
        {
            audiomanager.guruWalk.Stop();
        }
        if (playermanager.munculPuzzle == true)
        {
            if (mendekat == true && volume < 1)
            {
                playermanager.peringatanObj.SetActive(true);
                volume += 1;
            }
            else if (mendekat == false && volume > 0.3f)
            {
                playermanager.peringatanObj.SetActive(false);
                volume -= Time.deltaTime;
            }
        }
        else
        {
            volume = 0.5f;
            if (mendekat == true && playermanager._transfer == false)
            {
                playermanager.peringatanObj.SetActive(true);
            }
            else if (mendekat == false && playermanager._transfer == false)
            {
                playermanager.peringatanObj.SetActive(false);
            }
            else
            {
                mendekat = false;
            }

        }
        //==========================================================================
        //bagianskill
        if (skill1 != null)
        {
            bool skilaktif = false;
            if (skill1.frezeer == true)
            {
                //anim.SetBool("jalan", false);
                animasiGuru.SetBool("isWalking", false);
                animasiGuru.SetBool("skill1", true);
                agent.speed = 0;
                agent.Stop();
                step = false;
                skilaktif = true;
                audiomanager.guruWalk.Stop();

                pusing = true;
                fov.SetActive(false);
                pusing3D.SetActive(true);

            }
            else
            {
                agent.Resume();
                agent.speed = defauldspeed;
                if (skilaktif == true)
                {
                    stepSound();
                    skilaktif = false;
                }
                //anim.SetBool("jalan", true);
                animasiGuru.SetBool("isWalking", true);
                animasiGuru.SetBool("skill1", false);


                pusing = false;
                fov.SetActive(true);
                pusing3D.SetActive(false);
                //jalan();
            }
        }

        if (skill_2 != null)
        {
            if (skill_2.distractSkill == true)
            {
                if (skill_2.spawnGameObject == true)
                {
                    if (skill_2.gotobatu == true)
                    {
                        agent.Resume();
                        agent.speed = agent.speed + skill_2.speedguru;
                        animasiGuru.SetBool("isWalking", true);
                        animasiGuru.SetFloat("speed", animasiGuru.speed + skill_2.speedguru);
                        skill_2.gotobatu = false;
                        targetDistract = skill_2.spawnGameObject.GetComponent<Transform>();
                        agent.SetDestination(targetDistract.transform.position);
                    }


                    ////Destroy(skill_2.spawnGameObject, 5);
                    if (!skill_2.spawnGameObject.activeSelf == true)
                    {
                        Debug.Log("BatuDes");
                    }

                }

            }

        }

        if (Vector3.Distance(transform.position, target) < 1)
        {
            if (skill1 != null)
            {
                if (skill1.frezeer == false)
                {
                    if (random != 0)
                    {
                        agent.Stop();
                        animasiGuru.SetBool("isWalking", false);

                        agent.speed = 0;
                        step = false;
                        Invoke("jalan", time);
                    }
                    if (random == 0)
                    {
                        agent.Resume();
                        IterateWaypoint();
                        UpdateDestination();
                    }
                }
            }
            else if (skill_2 != null)
            {
                if (skill_2.distractSkill == false)
                {
                    animasiGuru.SetFloat("speed", 1);
                    if (random != 0)
                    {
                        agent.Stop();
                        animasiGuru.SetBool("isWalking", false);
                        agent.speed = 0;
                        step = false;
                        Invoke("jalan", time);
                    }
                    if (random == 0)
                    {
                        agent.Resume();

                        IterateWaypoint();
                        UpdateDestination();
                    }
                }
                else
                {
                    if (skill_2.spawnGameObject == true)
                    {
                        if (skill_2.gotobatu == true)
                        {
                            agent.Resume();
                            agent.speed = agent.speed + skill_2.speedguru;
                            animasiGuru.SetBool("isWalking", true);
                            animasiGuru.SetFloat("speed", animasiGuru.speed + skill_2.speedguru);
                            skill_2.gotobatu = false;
                            targetDistract = skill_2.spawnGameObject.GetComponent<Transform>();
                            agent.SetDestination(targetDistract.transform.position);
                        }


                        ////Destroy(skill_2.spawnGameObject, 5);
                        if (!skill_2.spawnGameObject.activeSelf == true)
                        {
                            Debug.Log("BatuDes");
                        }

                    }
                    else if (skill_2.gotobatu == false)
                    {
                        if (random != 0)
                        {
                            animasiGuru.SetBool("isWalking", false);
                            agent.Stop();
                            agent.speed = 0;
                            step = false;
                            Invoke("jalan", time);
                        }
                        if (random == 0)
                        {
                            agent.Resume();

                            IterateWaypoint();
                            UpdateDestination();
                        }
                    }
                    
                }
            }

            else
            {
                if (random != 0)
                {
                    animasiGuru.SetBool("isWalking", false);

                    agent.speed = 0;
                    agent.Stop();
                    step = false;
                    Invoke("jalan", time);
                }
                if (random == 0)
                {
                    animasiGuru.SetBool("isWalking", true);

                    agent.Resume();
                    IterateWaypoint();
                    UpdateDestination();
                }
            }

        }
    }
    void stepSound()
    {
        audiomanager.guruWalkMethod(volume);
        step = true;
    }
    void UpdateDestination()
    {
        if (skill_2 != null)
        {
            animasiGuru.SetBool("isWalking", true);
        }
        target = wayPoint[wayPoitIndex].position;
        agent.SetDestination(target);
    }
    void jalan()
    {
        animasiGuru.SetBool("isWalking", true);
        agent.Resume();
        random = 0;
        agent.speed = defauldspeed;
    }
    void IterateWaypoint()
    {
        wayPoitIndex++;
        if (wayPoitIndex == wayPoint.Length)
        {
            wayPoitIndex = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (skill_2 != null)
        {
            if (other.transform.tag == "Penghapus")
            {
                agent.speed = defauldspeed;
                animasiGuru.SetFloat("speed", 1);

                step = false;
                animasiGuru.SetBool("isWalking", false);
                Invoke("UpdateDestination", 5);
                //Invoke("stepSound", 5);
                audiomanager.guruWalk.Stop();
                Destroy(skill_2.spawnGameObject, 5);
                //Destroy(GameObject.Find("Batu(Clone)"),5);
                //skill_2.gotobatu = false;

            }
        }

        if (other.transform.tag == "deteksiguru")
        {
            mendekat = true;
        }
        if (other.transform.tag == "point")
        {
            random = Random.Range(0, 2);
            if (skill_2 == null)
            {
                if (random != 0)
                {
                    agent.Stop();
                    step = false;
                    animasiGuru.SetBool("isWalking", false);

                    Invoke("jalan", time);
                }
            }
            else if (skill_2 != null && skill_2.distractSkill == false)
            {
                //if (random != 0)
                //{
                //    agent.Stop();
                //    step = false;
                //    animasiGuru.SetBool("isWalking", false);

                //    Invoke("jalan", time);
                //}
            }
            else if (skill1 != null)
            {
                animasiGuru.SetBool("isWalking", false);
                agent.Stop();
                step = false;

                Invoke("jalan", time);
            }
            if (random == 0)
            {
            }
            //anim.SetBool("jalan", false);
            //animasiGuru.SetBool("diam", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "deteksiguru")
        {
            mendekat = false;
        }
        if (other.transform.tag == "point")
        {
            //anim.SetBool("jalan", true);
            //animasiGuru.SetBool("diam", false);
        }
    }
}