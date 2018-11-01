using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class RTS_DialogEditor : Editor
{
    public RTS_Dialog dialog;

    void OnEnable()
    {
        dialog = (RTS_Dialog)target;
    }

    public override void OnInspectorGUI()
    {
        // header
        GUILayout.Label("Dialog Editor", EditorStyles.boldLabel);

        //dialog.scenario = (Sprite)EditorGUILayout.ObjectField("Background: ",dialog.scenario,typeof(Sprite),true);

        // show every sentences on the dialog
        foreach (RTS_Sentence sentence in dialog.sentences)
        {
            sentence.actor = (RTS_Actor)EditorGUILayout.ObjectField("Actor: ", sentence.actor, typeof(RTS_Actor), true);
            sentence.expression = (RTS_Actor.Expression)EditorGUILayout.EnumPopup("Expression: ",sentence.expression);
            GUILayout.Label("Text:", EditorStyles.boldLabel);
            sentence.text = EditorGUILayout.TextArea(sentence.text);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("remove", GUILayout.ExpandWidth(false)))
            {
                dialog.sentences.Remove(sentence);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(12);
        }

        // button to add a new sentence
        if (GUILayout.Button("Add sentence", GUILayout.Height(40)))
        {
            dialog.sentences.Add(new RTS_Sentence());
        }

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        dialog.next = (RTS_ICoversation)EditorGUILayout.ObjectField("to: ", dialog.next, typeof(RTS_ICoversation), true);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(dialog);
        }
    }

}
