using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static float[,] NoisemapGenerator(int width, int height,float scale)
    {
        if (scale<=0)
        {
            scale = 0.0001f;
        }
        float[,] noisemap = new float[width,height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float perlin = Mathf.PerlinNoise(x / scale, y / scale);
                noisemap[x, y] = perlin;
            }
        }

        return noisemap;
    }
}
