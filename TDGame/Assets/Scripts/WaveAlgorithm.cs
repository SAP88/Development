using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class WaveAlgorithm
    {
        public static List<Vector2> FindShortestPath(int[,] Map, int[] walls, int startX, int startY, int targetX, int targetY)
        {
            int MapWidth = Map.GetLength(1);
            int MapHeight = Map.GetLength(0);

            bool isFullmapParsed = true;
            int[,] cMap = new int[MapHeight, MapWidth];

            int step = 0;
            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth; x++)
                {
                    if (walls.Contains(Map[y, x]))
                        cMap[y, x] = -2;//индикатор стены
                    else
                        cMap[y, x] = -1;//индикатор еще не ступали сюда
                }

            cMap[targetY, targetX] = 0;//Начинаем с финиша

            while (isFullmapParsed)
            {
                isFullmapParsed = false;
                for (int y = 0; y < MapHeight; y++)
                    for (int x = 0; x < MapWidth; x++)
                    {
                        if (cMap[y, x] == step)
                        {
                            //Ставим значение шага+1 в соседние ячейки (если они проходимы)
                            if (x - 1 >= 0 && cMap[y, x - 1] != -2 && cMap[y, x - 1] == -1)
                                cMap[y, x - 1] = step + 1;
                            if (y - 1 >= 0 && cMap[y - 1, x] != -2 && cMap[y - 1, x] == -1)
                                cMap[y - 1, x] = step + 1;
                            if (x + 1 < MapWidth && cMap[y, x + 1] != -2 && cMap[y, x + 1] == -1)
                                cMap[y, x + 1] = step + 1;
                            if (y + 1 < MapHeight && cMap[y + 1, x] != -2 && cMap[y + 1, x] == -1)
                                cMap[y + 1, x] = step + 1;
                        }
                    }
                step++;
                isFullmapParsed = true;
                if (cMap[startY, startX] != -1)//решение найдено
                    isFullmapParsed = false;
                if (step > MapWidth * MapHeight)//решение не найдено
                    isFullmapParsed = false;
            }

            recursion_result = new List<Vector2>();
            ReversePath(cMap, startY, startX);
            recursion_result.Add(new Vector2(targetX, targetY));
            return recursion_result.ToList();
        }

        private static List<Vector2> recursion_result = null;
        private static void ReversePath(int[,] cMap, int currentI, int currentJ)
        {
            //[ ][ ][ ]
            //[ ][ ][ ]
            //[ ][X][ ]
            if (CheckBound(cMap, currentI + 1, currentJ) && cMap[currentI + 1, currentJ] == cMap[currentI, currentJ] - 1)
            {
                recursion_result.Add(new Vector2 { y = currentI, x = currentJ });
                ReversePath(cMap, currentI + 1, currentJ);
                return;
            }

            //[ ][X][ ]
            //[ ][ ][ ]
            //[ ][ ][ ]
            if (CheckBound(cMap, currentI, currentJ) && cMap[currentI - 1, currentJ] == cMap[currentI, currentJ] - 1)
            {
                recursion_result.Add(new Vector2 { y = currentI - 1, x = currentJ });
                ReversePath(cMap, currentI - 1, currentJ);
                return;
            }

            //[ ][ ][ ]
            //[ ][ ][X]
            //[ ][ ][ ]
            if (CheckBound(cMap, currentI, currentJ + 1) && cMap[currentI, currentJ + 1] == cMap[currentI, currentJ] - 1)
            {
                recursion_result.Add(new Vector2 { y = currentI, x = currentJ });
                ReversePath(cMap, currentI, currentJ + 1);
                return;
            }

            //[ ][ ][ ]
            //[X][ ][ ]
            //[ ][ ][ ]
            if (CheckBound(cMap, currentI, currentJ - 1) && cMap[currentI, currentJ - 1] == cMap[currentI, currentJ] - 1)
            {
                recursion_result.Add(new Vector2 { y = currentI, x = currentJ });
                ReversePath(cMap, currentI, currentJ - 1);
                return;
            }
        }

        private static bool CheckBound(int[,] array, int i, int j)
        {
            return 0 <= j && j < array.GetLength(1) &&
                0 <= i && i < array.GetLength(0);
        }
    }
}
