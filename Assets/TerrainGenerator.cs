using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public enum DisplayMode
    {
        NoiseMap,
        ColorMap
    };

    public DisplayMode displayMode;
    public int seed;
    public int width;
    public int height;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)] public float persistance;
    public float lacunarity;
    public Vector2 offset;
    public bool autoReload;
    public Renderer texturerenderer;
    public FilterMode TextureFilterMode;
    public TerrainColor[] TerrainColors;

    public void GenerateMap()
    {
        if (width < 1)
        {
            width = 1;
        }

        if (height < 1)
        {
            height = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }

        float[,] noiseMap =
            NoiseGenerator.Generate(seed, width, height, noiseScale, octaves, persistance, lacunarity, offset);
        Color[] colormap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float PointHeight = noiseMap[x, y];
                for (int i = 0; i < TerrainColors.Length; i++)
                {
                    if (PointHeight <= TerrainColors[i].height)
                    {
                        colormap[y * width + x] = TerrainColors[i].Color;
                        break;
                    }
                }
            }
        }

        if (displayMode == DisplayMode.NoiseMap)
        {
            GenerateTerrainTexture(TerrainTexture.TextureHeightMap(noiseMap, TextureFilterMode));
        }
        else if (displayMode == DisplayMode.ColorMap)
        {
            GenerateTerrainTexture(TerrainTexture.TextureColorMap(colormap, width, height, TextureFilterMode));
        }
    }

    public void GenerateTerrainTexture(Texture2D texture)
    {
        texturerenderer.sharedMaterial.mainTexture = texture;
        texturerenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    [System.Serializable]
    public struct TerrainColor
    {
        public float height;
        public Color Color;
    }
}