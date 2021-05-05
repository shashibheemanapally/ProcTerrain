using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapGridPref;

    NoiseSettings noiseSettings;

    float curX;
    float curZ;
    float prevX;
    float prevZ;

    Dictionary<String, GameObject> hm;

    GameObject c  ;
    GameObject n  ;
    GameObject ne ;
    GameObject e  ;
    GameObject se ;
    GameObject s  ;
    GameObject sw ;
    GameObject w  ;
    GameObject nw;
    private void Start()
    {
        
        noiseSettings = GetComponent<NoiseSettings>();
        //mapGridPref.GetComponent<MeshGenerator>().setGridSize(noiseSettings.gridSize, noiseSettings.gridSize);

        curX = (float)(Math.Floor(transform.position.x/ noiseSettings.gridSize));
        curZ = (float)(Math.Floor(transform.position.z/ noiseSettings.gridSize));
        prevX = curX;
        prevZ = curZ;

        createInitialBoundaries();

    }

    private void Update()
    {
        curX = (float)(Math.Floor(transform.position.x/ noiseSettings.gridSize) );
        curZ = (float)(Math.Floor(transform.position.z/ noiseSettings.gridSize) );

        if(curX!=prevX || curZ != prevZ)
        {
            //Debug.Log("curZ pos changed");
            updateBoundaries((int)(curX - prevX), (int)(curZ - prevZ));
        }
        prevX = curX;
        prevZ = curZ;
    }
    /// <summary>
    /// creates initial 9 map grids and adds these grids to the hashmap.
    /// </summary>
    void createInitialBoundaries()
    {
        hm = new Dictionary<string, GameObject>();
        hm["c"] = createMapGrid(0, 0);
        hm["n"] = createMapGrid(0, noiseSettings.gridSize);
        hm["e"] = createMapGrid(noiseSettings.gridSize, 0);
        hm["s"] = createMapGrid(0,-noiseSettings.gridSize);
        hm["w"] = createMapGrid(-noiseSettings.gridSize, 0);
        hm["ne"] = createMapGrid(noiseSettings.gridSize, noiseSettings.gridSize);
        hm["se"] = createMapGrid(noiseSettings.gridSize, -noiseSettings.gridSize);
        hm["sw"] = createMapGrid(-noiseSettings.gridSize, -noiseSettings.gridSize);
        hm["nw"] = createMapGrid(-noiseSettings.gridSize, noiseSettings.gridSize);

    }
    /// <summary>
    /// updates the map grids when provided the delta x and delta z.
    /// </summary>
    /// <param name="xDiff"></param>
    /// <param name="zDiff"></param>
    void updateBoundaries(int xDiff,int zDiff)
    {
        c = hm["c"];
        n = hm["n"];
        ne = hm["ne"];
        e = hm["e"];
        se = hm["se"];
        s = hm["s"];
        sw = hm["sw"];
        w = hm["w"];
        nw = hm["nw"];
        
        //format-->destroy,rename,instantiate

        //went N
        if (zDiff == 1)
        {
            Destroy(se);
            Destroy(s);
            Destroy(sw);
            hm["e"] = ne;
            hm["se"] = e;
            hm["s"] = c;
            hm["sw"] = w;
            hm["w"] = nw;
            hm["c"] = n;
            hm["n"] = createMapGrid((int)curX* noiseSettings.gridSize, (int)curZ* noiseSettings.gridSize + noiseSettings.gridSize);
            hm["ne"]= createMapGrid((int)curX * noiseSettings.gridSize + noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize + noiseSettings.gridSize);
            hm["nw"]= createMapGrid((int)curX * noiseSettings.gridSize - noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize + noiseSettings.gridSize);
            return;
        }
        //went E
        if (xDiff == 1)
        {
            Destroy(sw);
            Destroy(w);
            Destroy(nw);
            hm["n"] = ne;
            hm["s"] = se;
            hm["sw"] = s;
            hm["w"] = c;
            hm["nw"] = n;
            hm["c"] = e;
            hm["ne"] = createMapGrid((int)curX * noiseSettings.gridSize + noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize + noiseSettings.gridSize);
            hm["e"] = createMapGrid((int)curX * noiseSettings.gridSize + noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize);
            hm["se"] = createMapGrid((int)curX * noiseSettings.gridSize + noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize - noiseSettings.gridSize);
            return;
        }
        //went W
        if (xDiff == -1 )
        {
            Destroy(ne);
            Destroy(e);
            Destroy(se);
            hm["n"] = nw;
            hm["ne"] = n;
            hm["e"] = c;
            hm["se"] = s;
            hm["s"] = sw;
            hm["c"] = w;
            hm["nw"] = createMapGrid((int)curX * noiseSettings.gridSize - noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize + noiseSettings.gridSize);
            hm["w"] = createMapGrid((int)curX * noiseSettings.gridSize - noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize);
            hm["sw"] = createMapGrid((int)curX * noiseSettings.gridSize - noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize - noiseSettings.gridSize);
            return;
        }
        //went S
        if (zDiff == -1)
        {
            Destroy(ne);
            Destroy(n);
            Destroy(nw);
            hm["n"] = c;
            hm["ne"] = e;
            hm["e"] = se;
            hm["w"] = sw;
            hm["nw"] = w;
            hm["c"] = s;
            hm["se"] = createMapGrid((int)curX * noiseSettings.gridSize + noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize - noiseSettings.gridSize);
            hm["s"] = createMapGrid((int)curX * noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize - noiseSettings.gridSize);
            hm["sw"] = createMapGrid((int)curX * noiseSettings.gridSize - noiseSettings.gridSize, (int)curZ * noiseSettings.gridSize - noiseSettings.gridSize);
            return;
        }
        
        
    }
    /// <summary>
    /// returns gameobject of mapGrid with position at origin but mesh rendered at offset.
    /// </summary>
    /// <param name="offsetX"></param>
    /// <param name="offsetZ"></param>
    /// <returns></returns>
    GameObject createMapGrid(int offsetX,int offsetZ)
    {
        GameObject mapGrid = Instantiate(mapGridPref);
        
        MeshGenerator meshGenerator = mapGrid.GetComponent<MeshGenerator>();
        meshGenerator.setGridSize(noiseSettings.gridSize, noiseSettings.gridSize);
        meshGenerator.xOffset = offsetX;
        meshGenerator.zOffset = offsetZ;

        //Debug.Log("createshape started at "+Environment.TickCount);
        meshGenerator.createShape(noiseSettings);
        //Debug.Log("createshape ended at " + Environment.TickCount);

        //Debug.Log("update mesh started at " + Environment.TickCount);
        meshGenerator.updateMesh();
        //Debug.Log("cupdate mesh ended at " + Environment.TickCount);

        return mapGrid;
    }

    




}
