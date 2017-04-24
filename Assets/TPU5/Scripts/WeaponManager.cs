using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

class WeaponManager : MonoBehaviour
{
    public Items parts = new Items();
    void Start()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name == "weapon-main") parts.setOn(t);
            else if (t.name == "weapon-dummy") parts.setOff(t);
        }
        parts.setDefaultOn(false);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WeaponManager))]
class WeaponManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WeaponManager wm = (WeaponManager)target;

        EditorGUILayout.LabelField(wm.parts.Active ? "Weapon is Enabled" : "Weapon is disabled");
        GUI.color = wm.parts.Active ? Color.red : Color.green;
        if(GUILayout.Button(wm.parts.Active.ToString().Replace("False", "Enable").Replace("True", "Disable"), GUILayout.Width(60)))
        {
            if (wm.parts.Active) wm.parts.TurnOff();
            else if (!wm.parts.Active) wm.parts.TurnOn();
        }
        GUI.color = Color.white;


        base.OnInspectorGUI();
    }
}
#endif