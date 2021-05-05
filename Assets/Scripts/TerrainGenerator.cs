using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;

    public int width = 128;
    public int height = 128;

    public int seed = 21;
    public float scale = 25;
    public int octaves = 4;
    public float persistance = 0.5f;
    public float lucunarity = 1.2f;
    public Vector2 offset = new Vector2(10, 10);

    public float speed = 1;
    


    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();

        
        
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        //offset.y+=speed*Time.deltaTime;
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        //terrainData.SetHeights(0, 0, Noise.GenerateNoiseMap(width, height, seed,scale,octaves,persistance,lucunarity,offset));

        return terrainData;
    }
}
