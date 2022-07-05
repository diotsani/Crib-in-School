using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class alfabetData : ScriptableObject
{
    [System.Serializable]
    public class letterdata 
    {
        public Sprite image;
        public string letter;
    }

    public List<letterdata> alfabetplain = new List<letterdata>();
    public List<letterdata> alfabetnormal = new List<letterdata>();
    public List<letterdata> alfabetHighlighted = new List<letterdata>();
    public List<letterdata> alfabetwrong = new List<letterdata>();
}
