using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(BoardData),false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawl : Editor
{
    public TextMesh letter;

    private BoardData GameDataInstance => target as BoardData;
    private ReorderableList _dataList;
    
    private void OnEnable()
    {
        InitializeReordableList(ref _dataList, "searchingWords", "searching Words");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawColumnsRowsInputField();
        EditorGUILayout.Space();
        ConvertToUpperButton();

        if (GameDataInstance.Board != null && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
        DrawBoardTable();

        GUILayout.BeginHorizontal();

        clearBoardButton();
        fillUpWithRandomLettorButton();

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        _dataList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GameDataInstance);
        }
    }
    private void DrawColumnsRowsInputField() 
    {
        var columnsTemp = GameDataInstance.Columns;
        var rowTempt = GameDataInstance.Rows;

        GameDataInstance.Columns = EditorGUILayout.IntField("Columns", GameDataInstance.Columns);
        GameDataInstance.Rows = EditorGUILayout.IntField("Rows",GameDataInstance.Rows);
        if ((GameDataInstance.Columns!=columnsTemp||GameDataInstance.Rows!=rowTempt)
            &&GameDataInstance.Columns>0&&GameDataInstance.Rows>0)
        {
            GameDataInstance.CreateNewBoard();
        }
    }
    private void DrawBoardTable() 
    {
        var TableStyle = new GUIStyle("Box");
        TableStyle.padding = new RectOffset(10,10,10,10);
        TableStyle.margin.left = 32;
        
        var HeaderColumnStyle = new GUIStyle();
        HeaderColumnStyle.fixedWidth = 35;

        var columnStyle = new GUIStyle();
        columnStyle.fixedWidth = 50;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.fixedWidth = 40;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var textFIeldStyle = new GUIStyle();

        textFIeldStyle.normal.background = Texture2D.whiteTexture;
        textFIeldStyle.normal.textColor = Color.black;
        textFIeldStyle.fontStyle = FontStyle.Bold;
        textFIeldStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal(TableStyle);
        for (var x = 0; x < GameDataInstance.Columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? HeaderColumnStyle : columnStyle);
            for (var y = 0; y < GameDataInstance.Rows; y++)
            {
                if (x >= 0&& y>=0)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var character = (string)EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFIeldStyle);
                    if (GameDataInstance.Board[x].Row[y].Length>1)
                    {
                        character = GameDataInstance.Board[x].Row[y].Substring(0, 1);
                    }
                    GameDataInstance.Board[x].Row[y] = character;
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void InitializeReordableList(ref ReorderableList list, string PropertyName,string listLabel) 
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(PropertyName), true, true, true, true);
        list.drawHeaderCallback = (Rect rect) =>
         {
             EditorGUI.LabelField(rect, listLabel);
         };

        var l = list;

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isfocused) =>
          {
              var element=l.serializedProperty.GetArrayElementAtIndex(index);
              rect.y += 2;
              EditorGUI.PropertyField(
                  new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                  element.FindPropertyRelative("word"), GUIContent.none);
          };
    }
    //membuat tulisan huruf besar
    private void ConvertToUpperButton() 
    {
        if (GUILayout.Button("To Upper"))
        {
            for (var i = 0; i < GameDataInstance.Columns; i++)
            {
                for (var j = 0; j < GameDataInstance.Rows; j++)
                {
                    var errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-z]").Count;
                    if (errorCounter>0)
                    {
                        GameDataInstance.Board[i].Row[j] = GameDataInstance.Board[i].Row[j].ToUpper();
                    }
                }
            }
            foreach (var searchWord in GameDataInstance.searchingWords)
            {
                var erorCounter = Regex.Matches(searchWord.word, @"[a-z]").Count;
                if (erorCounter>0)
                {
                    searchWord.word = searchWord.word.ToUpper();
                }
            }
        }
    }
    private void clearBoardButton()
    {
        if (GUILayout.Button("Clear Board"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)
                {
                    GameDataInstance.Board[i].Row[j] = " ";
                }
            }
        } 
    }
    //membuat huruf random
    private void fillUpWithRandomLettorButton() 
    {
        if (GUILayout.Button("Fill Up Random"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)
                {
                    int erorcounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-zA-Z]").Count;
                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int index = UnityEngine.Random.Range(0, letters.Length);
                     
                    if (erorcounter==0)
                    {
                        GameDataInstance.Board[i].Row[j] = letters[index].ToString();
                    }
                }
            }
        }
    }
}
