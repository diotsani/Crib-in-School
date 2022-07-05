using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    [System.Serializable]
    public class SearchingWord 
    {
        public string word;
    }

    [System.Serializable]
    public class BoardRow 
    {
        public int Size;
        public string[] Row;

        public BoardRow() { }

        public BoardRow(int size) 
        {
            CreateRow(size);
        }
        public void CreateRow(int size) 
        {
            Size = size;
            Row = new string[size];
            ClearRow();
        }
        public void ClearRow() 
        {
            for (int i = 0; i < Size; i++)
            {
                Row[i] = " ";
            }
        }
    }

    public float Time;
    public int Columns = 0;
    public int Rows = 0;
    public TextMesh letter;


    public BoardRow[] Board;
    public List<SearchingWord> searchingWords = new List<SearchingWord>();
    public void ClearEmtyString() 
    {
        for (int i = 0; i < Columns; i++)
        {
            Board[i].ClearRow();
        }
    }
    public void CreateNewBoard()
    {
        Board = new BoardRow[Columns];
        for (int i = 0; i < Columns; i++)
        {
            Board[i] = new BoardRow(Rows);
        }
    }
}
