using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


public class LevelGenerator : MonoBehaviour {
	[Tooltip("The Tilemap to draw onto")]
	public Tilemap tilemap;
	[Tooltip("The Tile to draw (use a RuleTile for best results)")]
	public TileBase tile;

	[Tooltip("Width of our map")]
	public int width;
	[Tooltip("Height of our map")]
	public int height;
	
	[Tooltip("The settings of our map")]
	public MapSettings mapSetting;

	void Start()
	{
		GenerateMap();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			ClearMap();
			GenerateMap();
		}
	}

	[ExecuteInEditMode]
	public virtual void GenerateMap()
	{
		ClearMap();
		int[,] map = new int[width, height];
		float seed;
		if (mapSetting.randomSeed)
		{
			seed = Time.time;
		}
		else
		{
			seed = mapSetting.seed;
		}

		//Generate the map depending omapSen the algorithm selected
		switch (mapSetting.algorithm)
		{
			case Algorithm.Perlin:
				map = MapFunctions.GenerateArray(width, height, true);
				map = MapFunctions.PerlinNoise(map, seed);
				break;
			case Algorithm.PerlinSmoothed:
				map = MapFunctions.GenerateArray(width, height, true);
				map = MapFunctions.PerlinNoiseSmooth(map, seed, mapSetting.interval);
				break;
			case Algorithm.PerlinCave:
				map = MapFunctions.GenerateArray(width, height, true);
				map = MapFunctions.PerlinNoiseCave(map, mapSetting.modifier, mapSetting.edgesAreWalls);
				break;
			case Algorithm.RandomWalkTop:
				map = MapFunctions.GenerateArray(width, height, true);
				map = MapFunctions.RandomWalkTop(map, seed);
				break;
			case Algorithm.RandomWalkTopSmoothed:
				map = MapFunctions.GenerateArray(width, height, true);
				map = MapFunctions.RandomWalkTopSmoothed(map, seed, mapSetting.interval);
				break;
			case Algorithm.RandomWalkCave:
				map = MapFunctions.GenerateArray(width, height, false);
				map = MapFunctions.RandomWalkCave(map, seed, mapSetting.clearAmount);
				break;
			case Algorithm.RandomWalkCaveCustom:
				map = MapFunctions.GenerateArray(width, height, false);
				map = MapFunctions.RandomWalkCaveCustom(map, seed, mapSetting.clearAmount);
				break;
			case Algorithm.CellularAutomataVonNeuman:
				map = MapFunctions.GenerateCellularAutomata(width, height, seed, mapSetting.fillAmount, mapSetting.edgesAreWalls);
				map = MapFunctions.SmoothVNCellularAutomata(map, mapSetting.edgesAreWalls, mapSetting.smoothAmount);
				break;
			case Algorithm.CellularAutomataMoore:
				map = MapFunctions.GenerateCellularAutomata(width, height, seed, mapSetting.fillAmount, mapSetting.edgesAreWalls);
				map = MapFunctions.SmoothMooreCellularAutomata(map, mapSetting.edgesAreWalls, mapSetting.smoothAmount);
				break;
			case Algorithm.DirectionalTunnel:
				map = MapFunctions.GenerateArray(width, height, false);
				map = MapFunctions.DirectionalTunnel(map, mapSetting.minPathWidth, mapSetting.maxPathWidth, mapSetting.maxPathChange, mapSetting.roughness, mapSetting.windyness);
				break;
		}

		MapFunctions.RenderMap(map, tilemap, tile);
		
	}

	public void ClearMap()
	{
		tilemap.ClearAllTiles();
	}
}

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		//Reference to our script
		LevelGenerator levelGen = (LevelGenerator)target;
		
		//Only show the mapsettings UI if we have a reference set up in the editor
		if (levelGen.mapSetting != null)
		{
			Editor mapSettingEditor = CreateEditor(levelGen.mapSetting);
			mapSettingEditor.OnInspectorGUI();

			if (GUILayout.Button("Generate"))
			{
				levelGen.GenerateMap();
			}

			if (GUILayout.Button("Clear"))
			{
				levelGen.ClearMap();
			}
		}
	}
}
