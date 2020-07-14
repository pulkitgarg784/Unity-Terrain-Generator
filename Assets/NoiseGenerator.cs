using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static float[,] Generate(int seed, int width, int height, float scale, int octaves, float persistance,
        float lacuna, Vector2 offset)
    {
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float[,] noisemap = new float[width, height];
        System.Random noiseSeed = new System.Random(seed);

        Vector2[] octaveoffesets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = noiseSeed.Next(-10000, 10000) + offset.x;
            float offsetY = noiseSeed.Next(-10000, 10000) + offset.y;
            octaveoffesets[i] = new Vector2(offsetX, offsetY);
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amp = 1;
                float freq = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float perlin = Mathf.PerlinNoise((x - (width / 2f)) / scale * freq + octaveoffesets[i].x,
                        (y - (height / 2f)) / scale * freq + octaveoffesets[i].y) * 2 - 1;
                    noiseHeight += perlin * amp;

                    amp *= persistance;
                    freq *= lacuna;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noisemap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noisemap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noisemap[x, y]);
            }
        }

        return noisemap;
    }
}