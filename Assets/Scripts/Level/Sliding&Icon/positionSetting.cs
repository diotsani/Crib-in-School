using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionSetting : MonoBehaviour
{
    public Transform progresPuzzle;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        progresPuzzle.position = transform.position;
        progresPuzzle.transform.localScale = this.transform.localScale;
    }
    
   
}
