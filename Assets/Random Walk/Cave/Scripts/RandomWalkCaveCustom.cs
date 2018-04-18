using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalkCaveCustom : MonoBehaviour {

    [Tooltip("The tilemap we want to render onto.")]
    public Tilemap Map;
    [Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
    public TileBase Tile;

    [Header("Map Settings")]
    [Tooltip("The height of our map.")]
    public int Height;
    [Tooltip("The width of our map.")]
    public int Width;

    [Tooltip("The amount of the dungeon we want to be cleared")]
    [Range(0, 100)]
    public int RequiredFloorPercent;

    int[,] MapArray;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            MapArray = MapFunctions.GenerateArray(Width, Height, false);
            MapArray = MapFunctions.RandomWalkCaveCustom(MapArray, Time.time, RequiredFloorPercent);
            StartCoroutine(MapFunctions.RenderMapWithDelay(MapArray, Map, Tile));
        }
    }
}
