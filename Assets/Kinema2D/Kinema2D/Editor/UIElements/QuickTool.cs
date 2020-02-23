using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using System;

public class QuickTool : EditorWindow
{

    private VisualTreeAsset _VisualTree;


    private void OnEnable()
    {

        var root = rootVisualElement;

        _VisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/2DMoveAndCollide Core/Editor/Resources/QuickToolTemplate.uxml");

        _VisualTree.CloneTree(root);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/2DMoveAndCollide Core/Editor/Resources/QuickToolStyles.uss");
        root.styleSheets.Add(styleSheet);

        UQueryBuilder<VisualElement> builder = root.Query(classes: new string[] { "spawn-button" });
        builder.ForEach(AddButtonFunctionality);

    }

    public void AddButtonFunctionality(VisualElement button)
    {

        button.hierarchy.Add(new Label(button.name));


        button.RegisterCallback<MouseDownEvent>(evnt =>
       {
           
           SpawnPrefab(button.name);
       });
    }


    [MenuItem("QuickTool/Open _%#T")]
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it’s already open.
        var window = GetWindow<QuickTool>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("QuickTool");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(250, 50);

        
    }

    [MenuItem("QuickTool/Close")]
    public static void CloseWindow()
    {
        var window = GetWindow<QuickTool>();
        window.Close();
    }

    public void SpawnPrefab(string name)
    {
        Debug.Log(name);
        string prefabPath = "Assets/2DMoveAndCollide Core/Prefabs/" + name + ".prefab";
        GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath));
    }

}



