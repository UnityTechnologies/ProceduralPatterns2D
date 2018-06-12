using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelGeneratorStack : MonoBehaviour
{
	public Tilemap tilemap;
	public TileBase tile;

	[Tooltip("The width of each layer of the stack")]
	public int width;
	[Tooltip("The height of each layer of the stack")]
	public int height;

	[SerializeField]
    public List<MapSettings> mapSettings = new List<MapSettings>();

    List<int[,]> mapList = new List<int[,]>();
	
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            tilemap.GetComponent<Tilemap>().ClearAllTiles();
			GenerateMap();
        }
    }
   
	[ExecuteInEditMode]
    public void GenerateMap()
    {
		ClearMap();
		mapList = new List<int[,]>();

		//Work through the List of mapSettings
		for (int i = 0; i < mapSettings.Count; i++)
        {
            int[,] map = new int[width,height];
            float seed;
            if(mapSettings[i].randomSeed)
            {
                seed = Time.time.GetHashCode();
            }
            else
            {
                seed = mapSettings[i].seed.GetHashCode();
            }

			//Generate the map depending on the algorithm selected
            switch(mapSettings[i].algorithm)
            {
                case Algorithm.Perlin:
					//First generate our array
                    map = MapFunctions.GenerateArray(width, height, true);
					//Next generate the perlin noise onto the array
                    map = MapFunctions.PerlinNoise(map, seed);
                    break;
                case Algorithm.PerlinSmoothed:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, true);
					//Next generate the perlin noise onto the array
					map = MapFunctions.PerlinNoiseSmooth(map, seed, mapSettings[i].interval);
                    break;
                case Algorithm.PerlinCave:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, true);
					//Next generate the perlin noise onto the array
					map = MapFunctions.PerlinNoiseCave(map, mapSettings[i].modifier, mapSettings[i].edgesAreWalls);
                    break; 
                case Algorithm.RandomWalkTop:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, true);
					//Next generater the random top
					map = MapFunctions.RandomWalkTop(map, seed);
                    break;
                case Algorithm.RandomWalkTopSmoothed:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, true);
					//Next generate the smoothed random top
					map = MapFunctions.RandomWalkTopSmoothed(map, seed, mapSettings[i].interval);
                    break;
                case Algorithm.RandomWalkCave:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, false);
					//Next generate the random walk cave
					map = MapFunctions.RandomWalkCave(map, seed, mapSettings[i].clearAmount);
                    break;
                case Algorithm.RandomWalkCaveCustom:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, false);
					//Next generate the custom random walk cave
                    map = MapFunctions.RandomWalkCaveCustom(map, seed, mapSettings[i].clearAmount);
                    break;
                case Algorithm.CellularAutomataVonNeuman:
					//First generate the cellular automata array
					map = MapFunctions.GenerateCellularAutomata(width, height, seed, mapSettings[i].fillAmount, mapSettings[i].edgesAreWalls);
					//Next smooth out the array using the von neumann rules
					map = MapFunctions.SmoothVNCellularAutomata(map, mapSettings[i].edgesAreWalls, mapSettings[i].smoothAmount);
                    break;
                case Algorithm.CellularAutomataMoore:
					//First generate the cellular automata array
					map = MapFunctions.GenerateCellularAutomata(width, height, seed, mapSettings[i].fillAmount, mapSettings[i].edgesAreWalls);
					//Next smooth out the array using the Moore rules
					map = MapFunctions.SmoothMooreCellularAutomata(map, mapSettings[i].edgesAreWalls, mapSettings[i].smoothAmount);
                    break;
                case Algorithm.DirectionalTunnel:
					//First generate our array
					map = MapFunctions.GenerateArray(width, height, false);
					//Next generate the tunnel through the array
					map = MapFunctions.DirectionalTunnel(map, mapSettings[i].minPathWidth, mapSettings[i].maxPathWidth, mapSettings[i].maxPathChange, mapSettings[i].roughness, mapSettings[i].windyness);
                    break;
            }
			//Add the map to the list
            mapList.Add(map);

        }


		//Allows for all of the maps to be on the same tilemap without overlaying
		Vector2Int offset = new Vector2Int(-width / 2, (-height / 2) - 1);

		//Work through the list to generate all maps
        foreach(int[,] map in mapList)
        {
            MapFunctions.RenderMapWithOffset(map, tilemap, tile, offset);
            offset.y += -height + 1;
        }
    }

	public void ClearMap()
	{
		tilemap.ClearAllTiles();
	}
}

[CustomEditor(typeof(LevelGeneratorStack))]
public class LevelGeneratorStackEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		//Create a reference to our script
		LevelGeneratorStack levelGen = (LevelGeneratorStack)target;

		//List of editors to only show if we have elements in the map settings list
		List <Editor> mapEditors  = new List<Editor>();

		for(int i = 0; i < levelGen.mapSettings.Count; i++)
		{
			if (levelGen.mapSettings[i] != null)
			{
				Editor mapLayerEditor = CreateEditor(levelGen.mapSettings[i]);
				mapEditors.Add(mapLayerEditor);
			}
		}
		//If we have more than one editor in our editor list, draw them out. Also draw the buttons
		if (mapEditors.Count > 0)
		{
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			for (int i = 0; i < mapEditors.Count; i++)
			{
				mapEditors[i].OnInspectorGUI();
			}

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