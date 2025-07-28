using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject treePrefab;

    public int width = 20;
    public int height = 20;
    [Range(0f, 1f)] public float treeSpawnChance = 0.2f;

    void Start()
    {
        GenerateForest();
    }

    void GenerateForest()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = new Vector2(x, y);

                // Spawn grass
                Instantiate(grassPrefab, position, Quaternion.identity);

                // Randomly spawn trees
                if (Random.value < treeSpawnChance)
                {
                    Instantiate(treePrefab, position, Quaternion.identity);
                }
            }
        }
    }
}