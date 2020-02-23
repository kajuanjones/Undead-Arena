using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectionEditor : EditorWindow
{


    public string text;

    [MenuItem("FunTools/Selection Editor")]
    public static void ShowWindow()
    {
        GetWindow<SelectionEditor>("Selection Editor");
    }


    private void OnGUI()
    {

        foreach (GameObject obj in Selection.gameObjects)
        {
            text = obj.transform.name;
        }

        GUILayout.Label("Selection Editor", EditorStyles.boldLabel);

        text = EditorGUILayout.TextField("Edit Name:", text);

        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.name = text;
        }
    }
}
