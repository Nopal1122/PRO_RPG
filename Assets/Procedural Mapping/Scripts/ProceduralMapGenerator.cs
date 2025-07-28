using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProceduralMapGenerator : MonoBehaviour
{
    [Header("Tilemap & Tiles")]
    public Tilemap grassTilemap;
    public Tile[] grassTiles; // 0: center, 1: top, 2: bottom, 3: left, 4: right, 5: top-left, 6: top-right, 7: bottom-left, 8: bottom-right
    public GameObject treePrefab;

    [Header("Map Settings")]
    public int width = 20;
    public int height = 20;
    public float treeSpawnChance = 0.05f;

    [Header("Noise Settings")]
    public float noiseScale = 0.1f;
    public float islandCurve = 2.5f;
    public int seed = -1;

    private bool[,] map;
    private List<GameObject> activeTrees = new List<GameObject>();

    public void GenerateMap()
    {
        ClearMap();

        if (seed <= 0)
        {
            seed = Random.Range(1, int.MaxValue);
        }
        Random.InitState(seed);

        map = new bool[width, height];
        Vector2 center = new Vector2(width / 2f, height / 2f);
        float maxDistance = Vector2.Distance(Vector2.zero, center);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 tilePos = new Vector2(x, y);
                float noise = Mathf.PerlinNoise((x + seed) * noiseScale, (y + seed) * noiseScale);
                float distance = Vector2.Distance(tilePos, center) / maxDistance;
                float mask = 1 - Mathf.Pow(distance, islandCurve);
                float edgeWeight = Mathf.Clamp01(distance * 1.5f);
                float blended = Mathf.Lerp(1f, noise, edgeWeight);

                map[x, y] = blended * mask > 0.4f;
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y])
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    Tile chosenTile = GetGrassTile(x, y);
                    grassTilemap.SetTile(pos, chosenTile);

                    if (Random.value < treeSpawnChance && IsNotEdge(x, y))
                    {
                        SpawnTreeAt(pos);
                    }
                }
            }
        }

        Debug.Log("Generated with seed: " + seed);
    }

    void SpawnTreeAt(Vector3Int cellPosition)
    {
        Vector3 worldPos = grassTilemap.GetCellCenterWorld(cellPosition);
        GameObject tree = Instantiate(treePrefab, worldPos, Quaternion.identity);
        activeTrees.Add(tree);
    }

    public void ClearMap()
    {
        grassTilemap.ClearAllTiles();

        foreach (GameObject tree in activeTrees)
        {
            if (tree != null)
            {
                DestroyImmediate(tree);
            }
        }

        activeTrees.Clear();
        Debug.Log("Map cleared.");
    }

    Tile GetGrassTile(int x, int y)
    {
        bool hasTop = y + 1 < height && map[x, y + 1];
        bool hasBottom = y - 1 >= 0 && map[x, y - 1];
        bool hasLeft = x - 1 >= 0 && map[x - 1, y];
        bool hasRight = x + 1 < width && map[x + 1, y];

        if (!hasTop && !hasLeft) return grassTiles[5];
        if (!hasTop && !hasRight) return grassTiles[6];
        if (!hasBottom && !hasLeft) return grassTiles[7];
        if (!hasBottom && !hasRight) return grassTiles[8];

        if (!hasTop) return grassTiles[1];
        if (!hasBottom) return grassTiles[2];
        if (!hasLeft) return grassTiles[3];
        if (!hasRight) return grassTiles[4];

        return grassTiles[0];
    }

    bool IsNotEdge(int x, int y)
    {
        return x > 1 && y > 1 && x < width - 2 && y < height - 2;
    }
}
