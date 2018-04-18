using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalk : MonoBehaviour
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
	public float Seed;
	[Tooltip("Whether to use a random seed")]
	public bool randomSeed;

	int[,] MapArray; 
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
			Tilemap.ClearAllTiles();
			if(randomSeed)
			{
				Seed = Time.time;
			}
			//Generate Array
			MapArray = MapFunctions.GenerateArray(Width, Height, true);
			//Generate Random Walk
            MapArray = MapFunctions.RandomWalkTop(MapArray, Seed);
			//Render Map
            StartCoroutine(MapFunctions.RenderMapWithDelay(MapArray, Tilemap, Tile));
        }
    }
}
