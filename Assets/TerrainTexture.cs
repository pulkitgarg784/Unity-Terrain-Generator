using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTexture
{
    public static Texture2D TextureColorMap(Color[] colormap, int width, int height, FilterMode filterMode)
    {
        Texture2D texture = new Texture2D(width,height);
        texture.filterMode = filterMode;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colormap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureHeightMap(float[,] heightmap,FilterMode filterMode)
    {
        int width = heightmap.GetLength(0);
        int height = heightmap.GetLength(1);
        Color[] colormap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colormap[y * width + x] = Color.Lerp(Color.black, Color.white, heightmap[x, y]);
            }
        }

        return TextureColorMap(colormap, width, height,filterMode);
    }
}
