using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class LevelGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;

    public int layerWidth;
    public int layerHeight;

    [SerializeField]
    public List<MapLayer> mapLayers = new List<MapLayer>();

    List<int[,]> mapList = new List<int[,]>();

    void Start()
    {
        GenerateMaps();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            tilemap.GetComponent<Tilemap>().ClearAllTiles();
            mapList = new List<int[,]>();
            GenerateMaps();
        }
    }
   

    void GenerateMaps()
    {
        //Work through the List of maplayers
        for(int i = 0; i < mapLayers.Count; i++)
        {
            int[,] map = new int[layerWidth,layerHeight];
            float seed;
            if(mapLayers[i].randomSeed)
            {
                seed = Time.time.GetHashCode();
            }
            else
            {
                seed = mapLayers[i].seed.GetHashCode();
            }

			//Generate the map depending on the algorithm selected
            switch(mapLayers[i].algorithm)
            {
                case Algorithm.Perlin:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, true);
                    map = MapFunctions.PerlinNoise(map, seed);
                    break;
                case Algorithm.PerlinSmoothed:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, true);
                    map = MapFunctions.PerlinNoiseSmooth(map, seed, mapLayers[i].interval);
                    break;
                case Algorithm.PerlinCave:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, true);
                    map = MapFunctions.PerlinNoiseCave(map, mapLayers[i].modifier, mapLayers[i].edgesAreWalls);
                    break; 
                case Algorithm.RandomWalkTop:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, true);
                    map = MapFunctions.RandomWalkTop(map, seed);
                    break;
                case Algorithm.RandomWalkTopSmoothed:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, true);
                    map = MapFunctions.RandomWalkTopSmoothed(map, seed, mapLayers[i].interval);
                    break;
                case Algorithm.RandomWalkCave:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, false);
                    map = MapFunctions.RandomWalkCave(map, seed, mapLayers[i].clearAmount);
                    break;
                case Algorithm.RandomWalkCaveCustom:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, false);
                    map = MapFunctions.RandomWalkCaveCustom(map, seed, mapLayers[i].clearAmount);
                    break;
                case Algorithm.CellularAutomataVonNeuman:
                    map = MapFunctions.GenerateCellularAutomata(layerWidth, layerHeight, seed, mapLayers[i].fillAmount, mapLayers[i].edgesAreWalls);
                        map = MapFunctions.SmoothVNCellularAutomata(map, mapLayers[i].edgesAreWalls, mapLayers[i].smoothAmount);
                    break;
                case Algorithm.CellularAutomataMoore:
                    map = MapFunctions.GenerateCellularAutomata(layerWidth, layerHeight, seed, mapLayers[i].fillAmount, mapLayers[i].edgesAreWalls);
					map = MapFunctions.SmoothMooreCellularAutomata(map, mapLayers[i].edgesAreWalls, mapLayers[i].smoothAmount);
                    break;
                case Algorithm.DirectionalTunnel:
                    map = MapFunctions.GenerateArray(layerWidth, layerHeight, false);
                    map = MapFunctions.DirectionalTunnel(map, mapLayers[i].minPathWidth, mapLayers[i].maxPathWidth, mapLayers[i].maxPathChange, mapLayers[i].roughness, mapLayers[i].windyness);
                    break;
            }
            mapList.Add(map);

        }


		//Allows for all of the maps to be on the same tilemap without overlaying
		Vector2Int offset = new Vector2Int(-layerWidth / 2, (-layerHeight / 2) - 1);

        foreach(int[,] map in mapList)
        {
            MapFunctions.RenderMapWithOffset(map, tilemap, tile, offset);
            offset.y += -layerHeight + 1;
        }
    }

}