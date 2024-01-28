using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Check : MonoBehaviour
{

    private Vector3 FindTileWithCondition(float a, float b, float c, float d, List<Vector3> existTiles) // a = 목표타일 b = 현재타일 c = 목표타일 d = 현재타일
    {
        float minX = Math.Min(a, b);
        float maxX = Math.Max(a, b);
        float minY = Math.Min(c, d);
        float maxY = Math.Max(c, d);
        Vector3 smallestTile = new Vector3(a, c, 100);

        foreach (Vector3 vector in existTiles)
        {
            float x = vector.x;
            float y = vector.y;

            if (x >= minX && x <= maxX && y >= minY && y <= maxY)
            {
                if (x < smallestTile.x || (x == smallestTile.x && y < smallestTile.y))
                {
                    smallestTile = vector;
                }
            }
        }

        return smallestTile;
    }
    public bool checkham(List<Vector3> spawnPositions, int randomSpawnIndex, List<Vector3> existTiles)
    {
        Vector3 nowPosition = spawnPositions[randomSpawnIndex];
        bool[] trying = { true, true, true, true };
        bool tryingX = false;
        bool tryingY = false;
        if (nowPosition.x == existTiles[existTiles.Count - 1].x)
        {
            FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
            if (nowPosition.z == 100)
            {
                return true;
            }
            trying = new bool[] { false, true, false, false };
        }
        if (nowPosition.y == existTiles[existTiles.Count - 1].y)
        {
            FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
            if (nowPosition.z == 100)
            {
                return true;
            }
            trying = new bool[] { false, true, false, false };
        }



        for (int i = 0; i < 4; i++)
        {
            nowPosition = spawnPositions[randomSpawnIndex];
            if (!trying[i])
            {
                continue;
            }
            if (i == 0)
            {
                FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                if (nowPosition.z != 100)
                {
                    if (nowPosition.x < existTiles[existTiles.Count - 1].x)
                    {
                        nowPosition.x -= 1;
                    }
                    else
                    {
                        nowPosition.x += 1;
                    }
                    tryingX = true;
                }
                nowPosition.z = 0;
                FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                if (!tryingX && nowPosition.y == existTiles[existTiles.Count - 1].y - 1)
                {
                    return true;
                }
                else
                {
                    if (tryingX)
                    {
                        FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                        if (nowPosition.x == existTiles[existTiles.Count - 1].x - 1)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (i == 1)
            {
                FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                if (nowPosition.z != 100)
                {
                    if (nowPosition.y < existTiles[existTiles.Count - 1].y)
                    {
                        nowPosition.y -= 1;
                    }
                    else
                    {
                        nowPosition.y += 1;
                    }
                    tryingY = true;
                }
                nowPosition.z = 0;
                FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                if (!tryingY && nowPosition.x == existTiles[existTiles.Count - 1].x - 0.9f)
                {
                    return true;
                }
                else
                {
                    if (tryingY)
                    {
                        FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                        if (nowPosition.y == existTiles[existTiles.Count - 1].y - 0.9f)
                        {
                            return true;
                        }
                    }
                }
            }
            if (i == 2)
            {
                if (nowPosition.x < existTiles[existTiles.Count - 1].x)
                {
                    Vector3 maxPosition = FindTileWithCondition(-4.4f, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                    for (int j = 1; j < (maxPosition.x - nowPosition.x) / 0.9f + 1; j++)
                    {
                        nowPosition.x += j * 0.9f;
                        nowPosition = FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                        if (nowPosition.z == 100)
                        {
                            nowPosition = FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                            if (nowPosition.z == 100)
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Vector3 maxPosition = FindTileWithCondition(4.6f, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                    for (int j = 1; j < (maxPosition.x - nowPosition.x) / 0.9f + 1; j++)
                    {
                        nowPosition.x += j * 0.9f;
                        nowPosition = FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                        if (nowPosition.z == 100)
                        {
                            nowPosition = FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                            if (nowPosition.z == 100)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (i == 3)
            {
                if (nowPosition.y < existTiles[existTiles.Count - 1].y)
                {
                    Vector3 maxPosition = FindTileWithCondition(nowPosition.x, nowPosition.x, -0.9f, nowPosition.y, existTiles);
                    for (int j = 1; j < (maxPosition.y - nowPosition.y) / 0.9f + 1; j++)
                    {
                        nowPosition.y += j * 0.9f;
                        nowPosition = FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                        if (nowPosition.z == 100)
                        {
                            nowPosition = FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                            if (nowPosition.z == 100)
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Vector3 maxPosition = FindTileWithCondition(nowPosition.x, nowPosition.x, 8.1f, nowPosition.y, existTiles);
                    for (int j = 1; j < (maxPosition.y - nowPosition.y) / 0.9f + 1; j++)
                    {
                        nowPosition.y += j * 0.9f;
                        nowPosition = FindTileWithCondition(existTiles[existTiles.Count - 1].x, nowPosition.x, nowPosition.y, nowPosition.y, existTiles);
                        if (nowPosition.z == 100)
                        {
                            nowPosition = FindTileWithCondition(nowPosition.x, nowPosition.x, existTiles[existTiles.Count - 1].y, nowPosition.y, existTiles);
                            if (nowPosition.z == 100)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
}
