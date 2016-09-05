﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MapGenerator))]
public class MapGenEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapGenerator script = (MapGenerator)target;
        if(GUILayout.Button("Generate Map"))
        {
            script.GenerateMap();
        }
    }
}
