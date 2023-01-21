using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Map
{
    public struct GameCellData
    {
        public CellData CellData;
        public int Height;
    }
    public class NoiseGenerator : MonoBehaviour
    {
       [SerializeField] private Texture2D noiseTex;
       [SerializeField] private MapConfig _config;
       public int pixWidth;
       public int pixHeight;
       // The origin of the sampled area in the plane.
       public float xOrg;
       public float yOrg;
       
       public float scale = 1.0F;

       
       private Color[] pix;
       private GameCellData[,] cells;

       [SerializeField] private RawImage img;
       private void Start()
       {
           RegenerateTexture();
           img.texture = noiseTex;
       }

       [Button]
       private void RegenerateTexture()
       {
           noiseTex = new Texture2D(pixWidth, pixHeight);
           pix = new Color[noiseTex.width * noiseTex.height];
           cells = new GameCellData[noiseTex.width , noiseTex.height];
           noiseTex = new Texture2D(pixWidth, pixHeight);
           CalcNoise();
       }
       
       void CalcNoise()
       {
           // For each pixel in the texture...
           float y = 0.0F;

           while (y < noiseTex.height)
           {
               float x = 0.0F;
               while (x < noiseTex.width)
               {
                   float xCoord = xOrg + x / noiseTex.width * scale;
                   float yCoord = yOrg + y / noiseTex.height * scale;
                   float sample = Mathf.PerlinNoise(xCoord, yCoord);
                   cells[(int) x, (int) y] = GetCellByNoiseValue(sample);
                   pix[(int) y * noiseTex.width + (int) x] = cells[(int) x, (int) y].CellData.color;//new Color(sample, sample, sample);
                   x++;
               }
               y++;
           }

           // Copy the pixel data to the texture and load it into the GPU.
           noiseTex.SetPixels(pix);
           noiseTex.Apply();
       }

       public float GetNoiseValue(int x, int z)
       {
           float xCoord = xOrg + (float)x / pixWidth * scale;
           float yCoord = yOrg + (float)z / pixHeight * scale;
           var res = Mathf.PerlinNoise(xCoord, yCoord);
           return res;
       }

       private GameCellData GetCellByNoiseValue(float value)
       {
           var height = (int)(_config.cells.Length * value);
           return new GameCellData()
           {
               CellData = _config.cells[height],
               Height = height
           };
       }
    }
}