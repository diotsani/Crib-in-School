using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SarchingWord : MonoBehaviour
{
    public Text displayedText;
    public Image crossLine;

    private string _word;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        GameEvent.OnCorrectWOrd+= CorrectWord;
    }
    private void OnDisable()
    {
        GameEvent.OnCorrectWOrd-= CorrectWord;
    }
    public void setWord(string word) 
    {
        _word = word;
        displayedText.text = word;
    }
    private void CorrectWord(string word, List<int> squareIndexes) 
    {
        if (word==_word)
        {
            crossLine.gameObject.SetActive(true);
        }
    }
}
