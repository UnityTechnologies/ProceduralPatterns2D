# Procedural Patterns to use with Tilemaps

![Procedural](https://i.imgur.com/hOk30GL.png)

## Description

This project shows a number of different patterns to use with Tilemaps to create custom maps. 
All the main functions are within the script [MapFunctions.cs](https://github.com/UnityTechnologies/ProceduralPatterns2D/blob/master/Assets/Global%20Scripts/MapFunctions.cs).

### How to get started:

Within this project there are scenes showing examples of the following algorithms:
1. [Perlin Noise](https://github.com/UnityTechnologies/ProceduralPatterns2D/tree/master/Assets/Generation%20Examples/Perlin%20Noise)
	1. Basic Generation (for top layer)
	2. Smoothed Generation (for top layer)
	3. Cave Generation
2. [Random Walk](https://github.com/UnityTechnologies/ProceduralPatterns2D/tree/master/Assets/Generation%20Examples/Random%20Walk)
	1. Basic Generation (for top layer)
	2. Smoothed Generation (for top layer)
	3. Cave Generation (4 directional movement)
	4. Custom Cave Generation (8 directional movement)
3. [Cellular Automata](https://github.com/UnityTechnologies/ProceduralPatterns2D/tree/master/Assets/Generation%20Examples/Cellular%20Automata)
	1. von Neumann Neighbourhood
	2. Moore Neighbourhood
4. [Directional Dungeon](https://github.com/UnityTechnologies/ProceduralPatterns2D/tree/master/Assets/Generation%20Examples/Directional%20Tunnel)

There is also an example of using multiple types of generation for one tilemap. 
This can be seen in the Multi-Generation Example subfolder.

### Layout of each scene

Within each scene there will be an object named LevelGenerator. This object holds the [LevelGenerator.cs](https://github.com/UnityTechnologies/ProceduralPatterns2D/blob/master/Assets/Global%20Scripts/LevelGenerator.cs) Script, the exception to this rule is the multi-generation example scene. 
The object named LevelGenerator in this scene holds a customised version of the [LevelGenerator.cs](https://github.com/UnityTechnologies/ProceduralPatterns2D/blob/master/Assets/Multi-Generation%20Example/Scripts/LevelGeneratorStack.cs) script which allows for multiple types of Map Settings

![LevelGenerator](https://i.imgur.com/tGOCyZu.png)

On this game object, you can generate and clear the level in edit mode using the buttons provided

### How to create Map Settings

To create a new map setting object, you need to right click in the project view then go Create->Map Settings.

![ProjectView](https://i.imgur.com/3mnSX93.png)

*or*

You can got to Assets->Create->Map Settings from the toolbar

![Toolbar](https://i.imgur.com/DshzBGv.png)

### How to call functions from MapFunctions.cs

The good thing about the MapFunctions.cs script is that you can take it out of this project and put it into any other project.
If you want to call functions from the script you would just need to do the following:

```csharp
int[,] map;
map = MapFunctions.GenerateArray(width, height, empty);
map = MapFunctions.PerlinNoise(map, seed); 
MapFunctions.RenderMap(map, tilemap, tile);
```

What this code would do is:
1. Create an int array for a map
2. Generate the array into the map
3. Generate perlin noise within the map
4. Render the map to a tilemap

**Software Requirements**

Required: Unity 2017.3, or later version

**Hardware Requirements**

Required: Any Computer (Windows or Mac)

**Owner and Responsible Devs**

Owners: Ethan Bruins (ethanbruins@unity3d.com)
