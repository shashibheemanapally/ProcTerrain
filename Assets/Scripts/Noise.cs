using UnityEngine;
using System.Collections;

public static class Noise
{
	/// <summary>
	/// returns perlin noise grid with float values(can be negative as well)
	/// </summary>
	/// <param name="mapWidth"></param>
	/// <param name="mapHeight"></param>
	/// <param name="scale"></param>
	/// <param name="octaves"></param>
	/// <param name="persistance"></param>
	/// <param name="lacunarity"></param>
	/// <param name="offset"></param>
	/// <returns></returns>
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity, float offsetX,float offsetY)
	{
		float[,] noiseMap = new float[mapWidth+1, mapHeight+1];

		

		if (scale <= 0)
		{
			scale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		


		for (int y = 0; y <= mapHeight; y++)
		{
			for (int x = 0; x <= mapWidth; x++)
			{

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x+offsetX ) / scale * frequency ;
					float sampleY = (y+offsetY ) / scale * frequency ;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)  ;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight)
				{
					maxNoiseHeight = noiseHeight;
				}
				else if (noiseHeight < minNoiseHeight)
				{
					minNoiseHeight = noiseHeight;
				}
				noiseMap[x, y] = noiseHeight;
			}
		}

		//for (int y = 0; y <= mapHeight; y++)
		//{
		//	for (int x = 0; x <= mapWidth; x++)
		//	{

		//		noiseMap[x, y] -= 1;
		//	}
		//}



		//Debug.Log(maxNoiseHeight + "  " + minNoiseHeight);
		
		return noiseMap;
	}

}