using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    public float time;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time>0.1f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            UserDataManager.Load();
            SceneManager.LoadScene(scene);
        }
    }
    
}
