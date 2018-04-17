using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UIPlayerHUD))]
public class UIPlayerHUDEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        UIPlayerHUD myScript = (UIPlayerHUD)target;
        if(GUILayout.Button("Update Player HUD"))
        {
            myScript.UpdateHud();
        }
    }
}
