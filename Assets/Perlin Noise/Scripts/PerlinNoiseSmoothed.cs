using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoiseSmoothed : MonoBehaviour
{
	[Tooltip("The tilemap we want to render onto.")]
	public Tilemap Tilemap;
	[Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
	public TileBase Tile;

	[Header("Map Settings")]
	[Tooltip("The height of the map")]
	public int Height;
	[Tooltip("The width of the map")]
	public int Width;
	[Tooltip("The seed to use for our perlin generation")]
	public float Seed;
	[Tooltip("Whether to use a random seed")]
	public bool randomSeed;

	[Tooltip("The interval at which we will record the perlin height into a List")]
	[Range(1,10)]
	public int Interval = 4;

	int[,] MapArray;
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Tilemap.ClearAllTiles();
			if(randomSeed)
			{
				Seed = Time.time;
			}
			//Generate our map
			MapArray = MapFunctions.GenerateArray(Width, Height, true);
			//Create the perlin noise
			MapArray = MapFunctions.PerlinNoiseSmooth(MapArray, Seed, Interval);
			//Render the map
			StartCoroutine(MapFunctions.RenderMapWithDelay(MapArray, Tilemap, Tile)); 
		}
	}
}
