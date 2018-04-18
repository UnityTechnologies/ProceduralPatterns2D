using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoise: MonoBehaviour
{
	[Tooltip("The tilemap we want to render onto.")]
	public Tilemap Tilemap;
	[Tooltip("The tile we want to draw. (For the best result use a rule tile)")]
	public TileBase Tile;

	[Header("Map Settings")]
	[Tooltip("Height of our map")]
	public int Height; 
	[Tooltip("Width of our map")]
	public int Width;
	[Tooltip("The seed to use with the perlin")]
	public float Seed;
	[Tooltip("Whether to use a random seed")]
	public bool randomSeed;


	int[,] MapArray;
    

	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Tilemap.ClearAllTiles();
			if (randomSeed)
			{
				Seed = Time.time;
			}
			MapArray = MapFunctions.GenerateArray(Width, Height, true); //Generate the array
			MapArray = MapFunctions.PerlinNoise(MapArray, Seed); //Create our map
			StartCoroutine(MapFunctions.RenderMapWithDelay(MapArray, Tilemap, Tile)); //Render the map
		}
	}
}
