using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CustomEditor(typeof(TerrainGenerator))]
public class MapGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator terrainGenerator = (TerrainGenerator) target;
        if (DrawDefaultInspector())
        {
            if (terrainGenerator.autoReload)
            {
                terrainGenerator.GenerateMap();
                
            }
        }
        if (GUILayout.Button("Generate"))
        {
            terrainGenerator.GenerateMap();
        }
        if (GUILayout.Button("Random Seed"))
        {
            terrainGenerator.seed = Random.Range(-10000, 10000);
            terrainGenerator.GenerateMap();
        }
    }


}
