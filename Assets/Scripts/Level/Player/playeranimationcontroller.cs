using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimationcontroller : MonoBehaviour
{
    public Animator playeranim;
    public bool noleh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (noleh==true)
        {
            playeranim.SetBool("Noleh",true);
        }
        else
        {
            noleh = false;
        }
    }
}
