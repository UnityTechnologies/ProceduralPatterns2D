using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalkSmooth : MonoBehaviour
{

	[Tooltip("The tilemap we want to render onto.")]
	public Tilemap Tilemap;
	[Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
	public TileBase Tile;

	[Header("Map Settings")]
	[Tooltip("The height of our map.")]
	public int Height;
	[Tooltip("The width of our map.")]
	public int Width;
	[Tooltip("The seed to use for our perlin generation")]
	public float seed;
	[Tooltip("Whether to use a random seed")]
	public bool randomSeed;
	[Tooltip("The minimum width we want each height section to be")]
	[Range(0,100)]
	public int minSectionWidth;

    int[,] MapArray;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (randomSeed)
            {
                seed = Time.time;
            }
			Tilemap.ClearAllTiles(); 
            MapArray = MapFunctions.GenerateArray(Width, Height, true);
            MapArray = MapFunctions.RandomWalkTopSmoothed(MapArray, seed.GetHashCode(), minSectionWidth);
            StartCoroutine(MapFunctions.RenderMapWithDelay(MapArray, Tilemap, Tile));
        }
    }
    
}
