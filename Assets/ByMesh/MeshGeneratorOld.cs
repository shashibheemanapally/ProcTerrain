using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGeneratorOld : MonoBehaviour
{
    public int xOffset = 0;
    public int zOffset = 0;

    public Material fen;

    Mesh mesh;
    int xSize;
    int zSize;
    Vector3[] vertices;
    int[] triangles;


    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //createShape();
        //updateMesh();
    }

    public void setGridSize(int x,int z)
    {
        xSize = x;
        zSize = z;
    }
    public void createShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for(int i=0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise((x+xOffset)*0.3f, (z+zOffset)*0.3f) * 5f;
                vertices[i] = new Vector3(x+xOffset, y, z+zOffset);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize+1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize+1;
                triangles[tris + 5] = vert + xSize+2;

                vert++;
                tris+=6;
            }
            vert++;
        }



    }
    public void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        GetComponent<MeshRenderer>().material = fen;
    }
}
