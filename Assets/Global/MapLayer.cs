using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

///<summary>
///This script makes the MapLayer class a scriptable object
///To create a new MapLayer, right click in the project view and select Map Layer Settings
///You can then use this script with the LevelGenerator to generate your level

public enum Algorithm
{
    Perlin, PerlinSmoothed, PerlinCave, RandomWalkTop, RandomWalkTopSmoothed, RandomWalkCave, RandomWalkCaveCustom, CellularAutomataVonNeuman, CellularAutomataMoore, DirectionalTunnel
}

[System.Serializable]
[CreateAssetMenu(fileName ="NewMapLayer", menuName = "Map Layer Settings", order = 0)]
public class MapLayer : ScriptableObject
{
    public Algorithm algorithm;    
    public bool randomSeed;	
    public string seed;	
    public int fillAmount;
    public int smoothAmount;
    public int clearAmount;
    public int interval;
    public int minPathWidth, maxPathWidth, maxPathChange, roughness, windyness;
    public bool edgesAreWalls;
    public float modifier;
}


//Custom UI for our class
[CustomEditor(typeof(MapLayer))]
public class MapLayer_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        MapLayer mapLayer = (MapLayer)target;
		mapLayer.algorithm = (Algorithm)EditorGUILayout.EnumPopup("Generation Method",mapLayer.algorithm);
		mapLayer.randomSeed = EditorGUILayout.Toggle("Random Seed", mapLayer.randomSeed);

		//Only appear if we have the random seed set to false
        if (!mapLayer.randomSeed)
        {
            mapLayer.seed = EditorGUILayout.TextField("Seed", mapLayer.seed);
        }

		//Shows different options depending on what algorithm is selected
        switch (mapLayer.algorithm)
        {
            case Algorithm.Perlin:
                //No additional Variables
                break;
            case Algorithm.PerlinSmoothed:
                mapLayer.interval = EditorGUILayout.IntSlider("Interval Of Points",mapLayer.interval, 1, 10);
                break;
            case Algorithm.PerlinCave:
                mapLayer.edgesAreWalls = EditorGUILayout.Toggle("Edges Are Walls", mapLayer.edgesAreWalls);
                mapLayer.modifier = EditorGUILayout.Slider("Modifier", mapLayer.modifier, 0.0001f, 1.0f);
                break;
            case Algorithm.RandomWalkTop:
                //No additional Variables
                break;
            case Algorithm.RandomWalkTopSmoothed:
                mapLayer.interval = EditorGUILayout.IntSlider("Minimum Section Length", mapLayer.interval, 1, 10);
                break;
            case Algorithm.RandomWalkCave:
                mapLayer.clearAmount = EditorGUILayout.IntSlider("Amount To Clear", mapLayer.clearAmount, 0, 100);
                break;
            case Algorithm.RandomWalkCaveCustom:
                mapLayer.clearAmount = EditorGUILayout.IntSlider("Amount To Clear", mapLayer.clearAmount, 0, 100);
                break;
            case Algorithm.CellularAutomataVonNeuman:
                mapLayer.edgesAreWalls = EditorGUILayout.Toggle("Edges Are Walls", mapLayer.edgesAreWalls);
                mapLayer.fillAmount = EditorGUILayout.IntSlider("Fill Percentage", mapLayer.fillAmount, 0, 100);
				mapLayer.smoothAmount = EditorGUILayout.IntSlider("Smooth Amount", mapLayer.smoothAmount, 0, 10);
				break;
            case Algorithm.CellularAutomataMoore:
                mapLayer.edgesAreWalls = EditorGUILayout.Toggle("Edges Are Walls", mapLayer.edgesAreWalls);
                mapLayer.fillAmount = EditorGUILayout.IntSlider("Fill Percentage", mapLayer.fillAmount, 0, 100);
                mapLayer.smoothAmount = EditorGUILayout.IntSlider("Smooth Amount", mapLayer.smoothAmount, 0, 10);
                break;
            case Algorithm.DirectionalTunnel:
                mapLayer.minPathWidth = EditorGUILayout.IntField("Minimum Path Width", mapLayer.minPathWidth);
                mapLayer.maxPathWidth = EditorGUILayout.IntField("Maximum Path Wdith", mapLayer.maxPathWidth);
                mapLayer.maxPathChange = EditorGUILayout.IntField("Maximum Path Change", mapLayer.maxPathChange);
                mapLayer.windyness = EditorGUILayout.IntSlider("Windyness", mapLayer.windyness, 0, 100);
                mapLayer.roughness = EditorGUILayout.IntSlider("Roughness", mapLayer.roughness, 0, 100);
                break;
        }
		AssetDatabase.SaveAssets();
    }
}
