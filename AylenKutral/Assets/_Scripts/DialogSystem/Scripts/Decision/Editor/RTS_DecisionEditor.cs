using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RTS_Decision))]
public class RTS_DecisionEditor : Editor
{
    public RTS_Decision decision;

    void OnEnable()
    {
        decision = (RTS_Decision)target;
    }

    public override void OnInspectorGUI()
    {
        // header
        GUILayout.Label("Dialog Editor", EditorStyles.boldLabel);

        //decision.scenario = (Sprite)EditorGUILayout.ObjectField("Background: ", decision.scenario, typeof(Sprite), true);

        decision.actor = (RTS_Actor)EditorGUILayout.ObjectField("Actor: ",decision.actor,typeof(RTS_Actor), true);
        decision.headerText = EditorGUILayout.TextArea(decision.headerText);

        foreach ( RTS_Option opt in decision.options)
        {
            
            GUILayout.Label("Text:", EditorStyles.boldLabel);
            opt.text = EditorGUILayout.TextArea(opt.text);
            opt.next = (RTS_ICoversation)EditorGUILayout.ObjectField("To: ", opt.next, typeof(RTS_ICoversation), true);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("remove", GUILayout.ExpandWidth(false)))
            {
                decision.options.Remove(opt);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(12);
            
        }

        // button to add a new sentence
        //max decisions are 4, modify count to increase or decrease
        if (decision.options.Count < 4 && GUILayout.Button("Add Option", GUILayout.Height(40)))
        {
            decision.options.Add(new RTS_Option());
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(decision);
        }
    }
}
