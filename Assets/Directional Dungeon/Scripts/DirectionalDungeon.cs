using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DirectionalDungeon : MonoBehaviour
{
    [Tooltip("The tilemap we want to render onto.")]
    public Tilemap Map; 
    [Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
    public TileBase Tile;

    [Header("Map Settings")]
    [Tooltip("The height of our map.")]
    public int Height;
    [Tooltip("The width of our map.")]
    public int Width;

    [Tooltip("Checked against to see if we are adjusting the path of the tunnel")]
    [Range(0, 100)]
    public int Windyness; //Used to determine how often we will adjust the path of the tunnel

    [Tooltip("Checked against to see if we are adjusting the width of the tunnel")]
    [Range(0, 100)]
    public int Roughness; //Used to determine how often we will change the width of the tunnel

    [Tooltip("The minimum width our path can be. (Bear in mind that the amount will count for zero count, i.e. 1 will result in a total width of 3 (-1,0,1))")]
    public int MinPathWidth = 1;
    [Tooltip("The maximum amount our width can adjust by")]
    public int MaxPathWidth = 3; //The width of the path, this will be multiplied by 2 and have 1 added onto it to account for 0. i.e. 3 will result in the max width being 7
    [Tooltip("the maximum amount our path can adjust by")]
    public int MaxPathChange = 2; //The amount that the path can change, will only happen when the windyness is being changed
    

    int[,] MapArray; //The array that stores the map

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) //Generate a new map on button click
        {
            MapArray = MapFunctions.GenerateArray(Width, Height, false); //Create our map
            MapArray = MapFunctions.DirectionalTunnel(MapArray, MinPathWidth, MaxPathWidth, MaxPathChange, Roughness, Windyness); //Produce a tunnel
            MapFunctions.RenderMap(MapArray, Map, Tile); //Render the result
        }
    }
} 
