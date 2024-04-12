using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChunkShuffle))]
public class ShuffleEditor : Editor
{
    ChunkShuffle shuffler;
    private void OnEnable()
    {
        shuffler = (ChunkShuffle)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();


        EditorGUILayout.LabelField("Configure chunk");


        if (GUILayout.Button("Append new chunk configuration"))
        {
            shuffler.AppendNewConfiguration();
        }

        if (GUILayout.Button("Switch to next configuration"))
        {
            shuffler.RearrangeChunks();
        }

    }
        
}
