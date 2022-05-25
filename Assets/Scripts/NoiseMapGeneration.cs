using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGeneration : MonoBehaviour
{
    // Method that will return an array of Cartesian coordinates representing a noise map
    // Noise values are calculated using the Perlin Noise function
    // mapDepth: size along the z-axis of the map
    // mapWidth: size along the x-axis of the map
    public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, int scale)
    {
        // Initialise noiseMap object to contain each coordinate in the map
        float[,] noiseMap = new float[mapDepth, mapWidth];

        for (int z = 0; z < mapDepth; z++)
            for (int x = 0; x < mapWidth; x++)
            {
                // Calculate sample indices based on the coordinates and the scale
                float sampleX = x / scale;
                float sampleZ = z / scale;

                float noise = Mathf.PerlinNoise(sampleX, sampleZ);

                noiseMap[z, x] = noise;
            }

        return noiseMap;
    }
}
