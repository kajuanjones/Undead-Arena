using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class InputNode : BaseInputNode
{
    private InputType inputType;

    public enum InputType
    {
        Number, Randomization
    }

    private string randomFrom = "";
    private string randomTo = "";

    private string inputValue = "";

    public InputNode()
    {
        windowTitle = "Input Node";
    }

    public override void DrawWindow()
    {
        base.DrawWindow();

        // Add more to the window
        // assign the value of our stored variable = to the following editor display
        inputType = (InputType)EditorGUILayout.EnumPopup("Input type :", inputType);

        // input cases

        if (inputType == InputType.Number)
        {
            inputValue = EditorGUILayout.TextField("Value", inputValue);
        } else if (inputType == InputType.Randomization)
        {
            //set randomFrom = to the shown textfield
            randomFrom = EditorGUILayout.TextField("From", randomFrom);
            randomTo = EditorGUILayout.TextField("To", randomTo);

            if (GUILayout.Button("Calculate Random"))
            {
                calculateRandom();
            }
        }
    }

    public override void DrawCurves()
    {
       

    }

    private void calculateRandom()
    {
        float rFrom = 0;
        float rTo = 0;
        
        // Try to get a value from randomFrom, and send the result to rFrom
        float.TryParse(randomFrom, out rFrom);
        float.TryParse(randomTo, out rTo);

        int randFrom = (int)(rFrom * 10);
        int randTo = (int)(rTo * 10);

        int selected = UnityEngine.Random.Range(randFrom, randTo + 1);

        float selectedValue = selected / 10;

        inputValue = selectedValue.ToString();


    }

    public override string getResult()
    {
        return inputValue.ToString();
    }
}
