using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer tileRenderer;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshCollider meshCollider;

    [SerializeField]
    private float mapScale;

    [SerializeField]
    NoiseMapGeneration noiseMapGeneration;

    private Texture2D BuildTexture(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Color[] colourMap = new Color[tileDepth * tileWidth];
        for (int z = 0; z < tileDepth; z++)
            for (int x = 0; x < tileWidth; x++)
            {
                // Get the 2D coordinate from the 1D array
                int colourIndex = z * tileWidth + x;
                float height = heightMap[z, x];

                // Assign a shade of grey based on the height value
                colourMap[colourIndex] = Color.Lerp(Color.black, Color.white, height);
            }

        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colourMap);
        tileTexture.Apply();

        return tileTexture;
    }

    void Start()
    {
        GenerateTile();
    }

    void GenerateTile()
    {
        // Calculate tile depth and width
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        // Calculate offset
        float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, (int)this.mapScale);

        // Create texture for each tile
        Texture2D tileTexture = BuildTexture(heightMap);
        this.tileRenderer.material.mainTexture = tileTexture;
    }
}
