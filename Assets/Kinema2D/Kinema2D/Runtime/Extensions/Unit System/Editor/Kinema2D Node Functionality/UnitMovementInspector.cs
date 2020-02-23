using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class UnitMovementInspector : EditorWindow
{

    List<UnitMovement> selections = new List<UnitMovement>();

    UnitMovement selectedUnit;

    [MenuItem("Kinema2D/UnitMovementInspector")]
    public static void ShowWindow()
    {
        GetWindow<UnitMovementInspector>("UnitMovement Inspector");
    }

    private void OnGUI()
    {

        selections.Clear();
        

        GameObject[] gos = Selection.gameObjects;
       


        foreach (GameObject go in gos)
        {
            UnitMovement cur = go.GetComponent<UnitMovement>();

            if (cur != null)
            {
                selections.Add(cur);
            }
        }

        string labelText = "";

        if (selections.Count <= 0)
        {
            labelText = "No UnitMovement Selected";
        } else
        {

            if (selections.Count > 1)
            {
                labelText = ("Multiple Units Selected");
            }
            else labelText = ("Selected Unit: " + "'" + selections[0].transform.name + "'");
            
        }

        GUILayout.Label(labelText);






        EditorGUI.BeginDisabledGroup(selections.Count <= 0);

        if (GUILayout.Button("Set Patrol"))
        {
            foreach(UnitMovement mov in selections)
            {
                mov.SetMoveState(new PatrolState(mov, true));
            }
        }

        EditorGUI.EndDisabledGroup();

    }
       
}
