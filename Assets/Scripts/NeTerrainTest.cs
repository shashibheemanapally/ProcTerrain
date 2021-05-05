using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeTerrainTest : MonoBehaviour
{
    Terrain terrain;
    public GameObject terrainPref;

    public int depth = 20;

    public int width = 1000;
    public int height = 1000;

    public int seed = 21;
    public float scale = 25;
    public int octaves = 4;
    public float persistance = 0.5f;
    public float lucunarity = 1.2f;
    public Vector2 offset = new Vector2(10, 10);

    private void Start()
    {
        terrain = GetComponent<Terrain>();
        GameObject terrainGO = Instantiate(terrainPref,new Vector3(1000,0,0),new Quaternion(0,0,0,0));
        terrain.SetNeighbors(null, null, terrainGO.GetComponent<Terrain>(), null);

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        terrainGO.GetComponent<Terrain>().terrainData= GenerateTerrain(terrainGO.GetComponent<Terrain>().terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        //terrainData.SetHeights(0, 0, Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lucunarity, offset));

        return terrainData;
    }


}
