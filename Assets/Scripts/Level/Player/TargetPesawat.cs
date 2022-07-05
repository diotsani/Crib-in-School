using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPesawat : MonoBehaviour
{
    public ProgressBarPlayer progresPlayer;
    public int _progresPlayer;

    private void Awake()
    {
        progresPlayer = GameObject.Find("gameplaymanager").GetComponent<ProgressBarPlayer>();
    }

    void Start()
    {
        _progresPlayer = progresPlayer.current;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score : " + _progresPlayer);
    }
}
