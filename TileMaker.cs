using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class TileMaker : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject>[] tiles;
    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<Vector3> existTiles = new List<Vector3>();
    Check check;
    public class TileInfo
    {
        public int Index { get; set; }
        public Vector3 Position { get; set; }

        public TileInfo(int index, Vector3 position)
        {
            Index = index;
            Position = position;
        }
    }
    public int tilesNumber;
    private List<int> impossibleIndex = new List<int>();
    private int randomTileNumber;
    private int randomSpawnIndex;
    private List<TileInfo> tileInfoList = new List<TileInfo>();

    private void Awake()
    {
        tiles = new List<GameObject>[tilePrefabs.Length];

        for (int index = 0; index < tiles.Length; index++)
        {
            tiles[index] = new List<GameObject>();
        }

        check = FindObjectOfType<Check>();


    }

    private void Start()
    {
        for (int index3 = 0; index3 < 50; index3++)
        {
            Debug.Log("타일 만들기를 시도했어요!");
            MakeTiles();
            Debug.Log(existTiles.Count);
            if (existTiles.Count == tilesNumber)
            {
                break;
            }
            else if (index3 == 49)
            {
                Debug.Log("타일 만들기에 실패했어요!");
                return;
            }
        }

        foreach (TileInfo tileInfo in tileInfoList)
        {
            int index = tileInfo.Index;
            Vector3 position = tileInfo.Position;

            GameObject newTile = Instantiate(tilePrefabs[index], position, Quaternion.identity);

            SpriteRenderer spriteRenderer = newTile.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = Mathf.RoundToInt((-3.5f - tileInfo.Position.x)/(-0.9f) + (-tileInfo.Position.y)/(0.9f));
            }
        }
    }

    private void MakeTiles()
    {
        List<int> attributesNumber = new List<int>(7);
        for (int i = 0; i < 7; i++)
        {
            attributesNumber.Add(0);
        }
        spawnPositions.Clear();
        existTiles.Clear();


        for (int length = 0; length < 8; length++)
        {
            for (int width = 0; width < 8; width++)
            {
                spawnPositions.Add(new Vector3(-3.5f + width * 0.9f, 0f - length * 0.9f, 0f));
            }
        }
        for (int index = 0; index < (tilesNumber / 2); index++)
        {
            randomTileNumber = UnityEngine.Random.Range(0, tilePrefabs.Length);
            attributesNumber[randomTileNumber] += 1;

        }

        for (int index = 0; index < tilePrefabs.Length; index++)
        {

            for (int repeat = 0; repeat < (attributesNumber[index] * 2); repeat++)
            {
                impossibleIndex.Clear();

                for (int index2=0; index2 < spawnPositions.Count; index2++)
                {
                    randomSpawnIndex = UnityEngine.Random.Range(0, spawnPositions.Count);

                    while (impossibleIndex.Contains(randomSpawnIndex))
                    {
                        randomSpawnIndex = UnityEngine.Random.Range(0, spawnPositions.Count);
                    }
                    if (repeat % 2 == 0)
                    {
                        break;
                    }
                    else if (check.checkham(spawnPositions, randomSpawnIndex, existTiles))
                    {
                        break;
                    }
                    else if (index2 == 49)
                    {
                        return;
                    }
                    else
                    {
                        impossibleIndex.Add(randomSpawnIndex);

                    }
                }

                TileInfo tileInfo = new TileInfo(index, spawnPositions[randomSpawnIndex]);
                tileInfoList.Add(tileInfo);

                existTiles.Add(spawnPositions[randomSpawnIndex]);
                spawnPositions.RemoveAt(randomSpawnIndex);
            }
        }
    }



}