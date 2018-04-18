using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CellularAutomata : MonoBehaviour {

    [Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
    public TileBase tile; 
    [Tooltip("The tilemap we want to render onto.")]
    public Tilemap tilemap; 

    int[,] MapArray; // A 2d array to store our generation into

    [Header("Map Settings")]
    [Tooltip("The height of our map.")]
    public int Height;
    [Tooltip("The width of our map.")]
    public int Width;
    [Tooltip("The seed we will use to generate the map.")]
    public string seed;
    [Tooltip("The amount we will fill in the map.")]
    [Range(0,100)]
    public int fillPercent;
    [Tooltip("Create a random seed using the time.")]
    public bool randomSeed;

    [Tooltip("Whether we want the edges to be walls.")]
    public bool edgesAreWalls;
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            tilemap.ClearAllTiles();
            if(randomSeed)
            {
                seed = Time.time.ToString();
            }
            MapArray = MapFunctions.GenerateCellularAutomata(Width, Height, seed.GetHashCode(), fillPercent, edgesAreWalls);
            MapFunctions.RenderMap(MapArray, tilemap, tile);
        }
    }
}
