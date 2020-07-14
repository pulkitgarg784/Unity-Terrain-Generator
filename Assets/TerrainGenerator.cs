using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public float noiseScale;

    public bool autoReload;
    public Renderer texturerenderer;

    public void GenerateMap()
    {
        float[,] noiseMap = NoiseGenerator.Generate(width, height, noiseScale);
        GenerateTerrain(noiseMap);
    }

    public void GenerateTerrain(float[,] noisemap)
    {
        int width = noisemap.GetLength(0);
        int height = noisemap.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        Color[] colormap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colormap[y * width + x] = Color.Lerp(Color.black, Color.white, noisemap[x, y]);
            }
        }

        texture.SetPixels(colormap);
        texture.Apply();
        texturerenderer.sharedMaterial.mainTexture = texture;
        texturerenderer.transform.localScale = new Vector3(width, 1, height);
    }
}