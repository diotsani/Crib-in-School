using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class munculskill : MonoBehaviour
{
    public LoadSkill ls;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ls.skillPrefabs[0], this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
