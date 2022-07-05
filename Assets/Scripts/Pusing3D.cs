using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusing3D : MonoBehaviour
{
    public TeacherAI guru;

    public float rotateAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (guru.pusing == true)
        {
            transform.Rotate(Vector3.up * rotateAmount);
        }
    }
}
