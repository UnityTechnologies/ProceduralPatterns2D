using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoiseCave : MonoBehaviour {

    [Tooltip("The tilemap we want to render onto.")]
    public Tilemap tilemap;
    [Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
    public TileBase tile;

    [Header("Map Settings")]
    [Tooltip("Height of our map")]
    public int Height;
    [Tooltip("Width of our map")]
    public int Width;

    [Range(0.001f, 1f)]
    public float modifier;

    public bool edgesAreWalls;

    int[,] mapArray;

	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.N))
        {
            mapArray = MapFunctions.GenerateArray(Width, Height, true);
            mapArray = MapFunctions.PerlinNoiseCave(mapArray, modifier, edgesAreWalls);
            MapFunctions.RenderMap(mapArray, tilemap, tile);
        }
	}
}
