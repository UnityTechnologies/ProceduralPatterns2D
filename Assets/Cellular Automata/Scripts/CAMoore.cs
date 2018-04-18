using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CAMoore : MonoBehaviour
{
    [Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
    public TileBase Tile; // Tile that we will draw onto our map

    public Tilemap Tilemap; // The Tilemap we will draw on
    int[,] MapArray; // A 2d array to store our generation into

    [Header("Map Settings")]
    [Tooltip("The height of our map")]
    public int Height;
    [Tooltip("The width of our map")]
    public int Width;
    [Tooltip("The seed we will use to generate the map")]
    public string Seed;
    [Tooltip("The amount we will fill in the map")]
    [Range(0, 100)]
    public int FillPercent;
    [Tooltip("Create a random seed using the time")]
    public bool RandomSeed;

    [Tooltip("Smoothing Iterations")]
    public int SmoothCount;

    [Tooltip("Whether the edges of the map should be walls")]
    public bool EdgesAreWalls;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {

            if (RandomSeed)
            {
                Seed = Time.time.ToString();
            }

            MapArray = MapFunctions.GenerateCellularAutomata(Width, Height, Seed.GetHashCode(), FillPercent, EdgesAreWalls);

			MapArray = MapFunctions.SmoothMooreCellularAutomata(MapArray, EdgesAreWalls, SmoothCount);
            
            MapFunctions.RenderMap(MapArray, Tilemap, Tile);
        }
    }
    
}

