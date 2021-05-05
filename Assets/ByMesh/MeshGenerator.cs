using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshGenerator : MonoBehaviour
{
    public int xOffset = 0;
    public int zOffset = 0;

    public Material fen;

     

    int xSize ;
    int zSize;

    Mesh mesh;
    
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    float[,] gridHeights;

    //for noise variables
    //public GameObject NSGO;
    //NoiseSettings noiseSettings;

    public AnimationCurve mapHeightCurve;

    MeshCollider meshCollider;


    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }
    
    public void setGridSize(int x, int z)
    {
        xSize = x;
        zSize = z;
    }
    public void createShape(NoiseSettings noiseSettings)
    {
        

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        triangles = new int[xSize * zSize * 6];
        uvs = new Vector2[vertices.Length];

        //Debug.Log("generate perlin started at " + Environment.TickCount);
        gridHeights = Noise.GenerateNoiseMap(noiseSettings.gridSize,noiseSettings.gridSize,noiseSettings.scale,noiseSettings.octaves,
                                             noiseSettings.persistance,noiseSettings.lacunarity,xOffset,zOffset);
        //Debug.Log("generate perlin ended at " + Environment.TickCount);


        //Debug.Log("vertex setting started at " + Environment.TickCount);


        
        for (int i=0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {

                
                vertices[i] = new Vector3(x+xOffset, mapHeightCurve.Evaluate(gridHeights[x, z]) * noiseSettings.amplitude, z+zOffset);
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
                
            }
            
                
        }


        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        //Debug.Log("vertex setting ended at " + Environment.TickCount);




    }
    public void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();



        mesh.RecalculateBounds();

        meshCollider.sharedMesh = mesh;

        //GetComponent<MeshRenderer>().material = fen;
    }
    
}
