using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public int squareIndex { get; set; }

    private alfabetData.letterdata _normalLetterData;
    private alfabetData.letterdata _selectedLetterData;
    private alfabetData.letterdata _currectLetterData;

    private SpriteRenderer _displayImage;

    private bool _selected;
    private bool _clicked;
    private int _index = -1;
    private bool _correct;

    //mengambil fungsi time untuk mengurangi waktu
    public timeManager timer;

    public void setIndex(int index) 
    {
        _index = index;
    }
    public int GetIndex() { return _index; }

    void Start()
    {
        _selected = false;
        _clicked = false;
        _correct = false;
        _displayImage = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GameEvent.OnEnableSquareSelecrion += OnEnableSquareSelection;
        GameEvent.OndisableSquarSelection += OnDisableSquareSelection;
        GameEvent.OnSelectSquare += selectsquare;
        GameEvent.OnCorrectWOrd += CorrectWord;
    }
    private void OnDisable()
    {
        GameEvent.OnEnableSquareSelecrion -= OnEnableSquareSelection;
        GameEvent.OndisableSquarSelection -= OnDisableSquareSelection;
        GameEvent.OnSelectSquare -= selectsquare;
        GameEvent.OnCorrectWOrd -= CorrectWord;
    }
    private void CorrectWord(string word,List<int>squareIndexes) 
    {
        if (_selected&&squareIndexes.Contains(_index))
        {
            _correct = true;
            _displayImage.sprite = _currectLetterData.image;
        }
        _selected = false;
        _clicked = false;
    }
    public void OnEnableSquareSelection() 
    {
        _clicked = true;
        _selected = false;
    }
    public void OnDisableSquareSelection() 
    {
        _selected = false;
        _clicked = false;
        if (_correct==true)
        {
            _displayImage.sprite = _currectLetterData.image;
        }
        else
        {
            _displayImage.sprite = _normalLetterData.image;
        }
    }
    private void selectsquare(Vector3 position) 
    {
        if (this.gameObject.transform.position==position)
        {
            _displayImage.sprite = _selectedLetterData.image;
        }
    }
    public void setSprites(alfabetData.letterdata normalLetterData, alfabetData.letterdata SelectedLetterData,alfabetData.letterdata correctLetterData) 
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = SelectedLetterData;
        _currectLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;
    }

    private void OnMouseDown()
    {
        OnEnableSquareSelection();
        GameEvent.enableSquareSelectionMethod();
        CheckSquare();
        _displayImage.sprite = _selectedLetterData.image;
    }
    private void OnMouseEnter()
    {
        CheckSquare();
    }
    private void OnMouseUp()
    {
        GameEvent.ClearSelectMethod();
        GameEvent.disbleSquareSelectionNethod();
       
    }
    public void CheckSquare() 
    {
        if (_selected==false&&_clicked==true)
        {
            _selected = true;
            GameEvent.ChecksquareMethod(_normalLetterData.letter, gameObject.transform.position, _index);
        }
    }
    
}
