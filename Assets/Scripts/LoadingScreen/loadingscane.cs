using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingscane : MonoBehaviour
{
    public levelmanager level;
    public Image loadingFill;

    private void Awake()
    {
        level = FindObjectOfType<levelmanager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        print((level.nomorlv + level.urutanNumber).ToString());
        loadingFill.fillAmount = 0;
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Level "+ (level.nomorlv + level.urutanNumber).ToString());
        while (!loading.isDone)
        {
            loadingFill.fillAmount = loading.progress/0.9f;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
