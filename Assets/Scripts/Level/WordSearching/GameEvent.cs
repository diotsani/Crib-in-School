using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent 
{
    public delegate void enableSquareSelection();
    public static event enableSquareSelection OnEnableSquareSelecrion;

    public static void enableSquareSelectionMethod() 
    {
        if (OnEnableSquareSelecrion!=null)
        {
            OnEnableSquareSelecrion();
        }
    }
    //==========================================================================
    public delegate void disbleSquareSelection();
    public static event disbleSquareSelection OndisableSquarSelection;

    public static void disbleSquareSelectionNethod() 
    {
        if (OndisableSquarSelection!=null)
        {
            OndisableSquarSelection();
        }
    }
    //==========================================================================
    public delegate void selectSquare(Vector3 position);
    public static event selectSquare OnSelectSquare;

    public static void selectSquareMethod(Vector3 position)
    {
        if (OnSelectSquare != null)
        {
            OnSelectSquare(position);
        }
    }
    //==========================================================================
    public delegate void Checksquare(string Letter,Vector3 squareposition,int squareIndex);
    public static event Checksquare OnChecksquare;

    public static void ChecksquareMethod(string Letter, Vector3 squareposition, int squareIndex)
    {
        if (OnChecksquare != null)
        {
            OnChecksquare(Letter,squareposition,squareIndex);
        }
    }
 //==========================================================================
    public delegate void ClearSelect();
    public static event ClearSelect OnClearSelect;

    public static void ClearSelectMethod()
    {
        if (OnClearSelect != null)
        {
            OnClearSelect();
        }
    }
    //==========================================================================

    public delegate void correctWord(string word, List<int> squareIndexes);

    public static event correctWord OnCorrectWOrd;

    public static void correctWordMethod(string word, List<int> squareIndexes) 
    {
        if (OnCorrectWOrd!=null)
        {
            OnCorrectWOrd(word, squareIndexes);
        }
    }
    //==========================================================================

}
