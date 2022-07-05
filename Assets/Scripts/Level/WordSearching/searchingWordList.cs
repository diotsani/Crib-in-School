using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchingWordList : MonoBehaviour
{
    public GameData currentGemedata;
    public GameObject serchingWordGameData;
    public float offset = 0.0f;
    public int maxcolumns = 5;
    public int maxrows = 4;

    private int _columns = 2;
    private int _row;
    private int _wordNumber;

    private List<GameObject> _words = new List<GameObject>();

    public GameObject target;

    void Start()
    {
        _wordNumber = currentGemedata.selectBoardData.searchingWords.Count;
        if (_wordNumber<_columns)
        {
            _row = 1;
        }
        else
        {
            hitungkolom();
        }
        createwordobjects();
        SetWordPosition();
    }
    private void hitungkolom() 
    {
        do
        {
            _columns++;
            _row = _wordNumber / _columns;
        } while (_row>=maxrows);
        if (_columns>maxcolumns)
        {
            _columns = maxcolumns;
            _row = _wordNumber / _columns;
        }
    }
    private bool tryincreaseeColumnNumber() 
    {
        _columns++;
        _row = _wordNumber / _columns;
        if (_columns>maxcolumns)
        {
            _columns = maxcolumns;
            _row = _wordNumber / _columns;
            return false;
        }
        if (_wordNumber%_columns>0)
        {
            _row++;
        }
        return true;
    }
    private void createwordobjects()
    {
        var squarescale = getsquarescale(new Vector3(0.1f, 0.1f, 0.1f));
        for (var i = 0; i < _wordNumber; i++)
        {
            _words.Add(Instantiate(serchingWordGameData) as GameObject);
            _words[i].transform.SetParent(this.transform);
            _words[i].transform.localScale = target.transform.localScale/4;
            _words[i].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            _words[i].GetComponent<SarchingWord>().setWord(currentGemedata.selectBoardData.searchingWords[i].word);
        }
    }
    private Vector3 getsquarescale(Vector3 defaultScale)
    {
        var finalScale =defaultScale;
        var adjustment = 1f;

        while (SHouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if (finalScale.x<=0||finalScale.y<=0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;

                return finalScale;
            }
        }
        return finalScale;
    }
    private bool SHouldScaleDown(Vector3 target) 
    {
        var squareRect = serchingWordGameData.GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();

        var squareSize = new Vector2(0, 0);
        
        squareSize.x = squareRect.rect.width * target.x + offset;
        squareSize.y = squareRect.rect.height * target.y + offset;

        var totalSquareHight = squareSize.y * _row;

        //memastikan seluruh kotak cocok dengan area parent rectagle
        if (totalSquareHight>parentRect.rect.height)
        {
            while (totalSquareHight>parentRect.rect.height)
            {
                if (tryincreaseeColumnNumber())
                {
                    totalSquareHight = squareSize.y * _row;
                }
                else
                {
                    return true;
                }
            }
        }
        var totalsquareWidh = squareSize.x * _columns;
        if (totalsquareWidh>parentRect.rect.width)
        {
            return true;
        }
        return false;
    }

    private void SetWordPosition() 
    {
        var squareRect = _words[0].GetComponent<RectTransform>();
        var wordOffset = new Vector2
        {
            x = squareRect.rect.width * squareRect.transform.localScale.x + offset,
            y = squareRect.rect.height * squareRect.transform.localScale.y + offset,
        };
        int columNumber = 0;
        int rowNumber = 0;
        var startPosition = getFirsstSquarePosition();
        foreach (var word in _words)
        {
            if (columNumber+1>_columns)
            {
                columNumber = 0;
                rowNumber += 1;
            }

            var positionx = startPosition.x + wordOffset.x * columNumber;
            var positiony = startPosition.y - wordOffset.y * rowNumber;

            word.GetComponent<RectTransform>().localPosition = new Vector2(positionx,positiony);
            columNumber++;
        }
    }

    private Vector2 getFirsstSquarePosition() 
    {
        var StartPosition= new Vector2(0, this.transform.position.y);
        var squareRect = _words[0].GetComponent<RectTransform>();
        var parentReact = this.GetComponent<RectTransform>();
        var squareSize = new Vector2(0, 0);

        squareSize.x = squareRect.rect.width * squareRect.transform.localScale.x + offset;
        squareSize.y = squareRect.rect.height * squareRect.transform.localScale.y + offset;

        // memastikan posisi ditengah
        var shiftBy = (parentReact.rect.width - (squareSize.x * _columns)) / 2;
        var shiftByY = (parentReact.rect.height - (squareSize.y * _row)) / 2;

        StartPosition.x = ((parentReact.rect.width - squareSize.x) / 2) * -1;
        StartPosition.x += shiftBy;
        StartPosition.y = (parentReact.rect.height - squareSize.y) / 2;
        StartPosition.y -= shiftByY;

        return StartPosition;
    }
}
