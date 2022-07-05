using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(alfabetData))]
[CanEditMultipleObjects]
[System.Serializable]
public class AlfabetDataDrawl : Editor
{
    private ReorderableList alfabetPlanList;
    private ReorderableList alfabetNormalList;
    private ReorderableList alfabetHighlightedList;
    private ReorderableList alfabetWrongList;

    private void OnEnable()
    {
        intializeReodableList(ref alfabetPlanList, "alfabetplain", "Alphabet Plain");
        intializeReodableList(ref alfabetNormalList, "alfabetnormal", "Alphabet Normal");
        intializeReodableList(ref alfabetHighlightedList, "alfabetHighlighted", "Alphabet Highlight");
        intializeReodableList(ref alfabetWrongList, "alfabetwrong", "Alphabet Wrong");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        alfabetPlanList.DoLayoutList();
        alfabetNormalList.DoLayoutList();
        alfabetHighlightedList.DoLayoutList();
        alfabetWrongList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        
    }
    private void intializeReodableList(ref ReorderableList list, string propertyName, string ListLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
            true, true, true, true);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect ,ListLabel);
        };

        var l = list;

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
          {
             var element = l.serializedProperty.GetArrayElementAtIndex(index);
              rect.y += 2;

             EditorGUI.PropertyField(
                 new Rect(rect.x, rect.y,60, EditorGUIUtility.singleLineHeight),
                 element.FindPropertyRelative("letter"),GUIContent.none);  
              EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
              element.FindPropertyRelative("image"), GUIContent.none);
          };
    }
}
