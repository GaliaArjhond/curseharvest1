using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PropsSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private GameObject[] stonePrefabs;
    [SerializeField] private GameObject[] vegetationPrefabs;
    [SerializeField] private int treeCount = 15;
    [SerializeField] private int stoneCount = 20;
    [SerializeField] private int vegCount = 10;
    [SerializeField] private float minDistanceBetweenProps = 1.5f;

    [Header("House Exclusion Zone")]
    [SerializeField] private Transform houseTransform; // drag your house here
    [SerializeField] private float houseRadius = 4f;   // adjust to match house size

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        List<Vector3> positions = GetValidTilePositions();
        Shuffle(positions);
        SpawnGroup(treePrefabs, treeCount, positions);
        SpawnGroup(stonePrefabs, stoneCount, positions);
        SpawnGroup(vegetationPrefabs, vegCount, positions);
    }

    List<Vector3> GetValidTilePositions()
    {
        List<Vector3> positions = new List<Vector3>();
        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int cell in bounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(cell))
            {
                Vector3 worldPos = groundTilemap.CellToWorld(cell)
                                 + new Vector3(0.5f, 0.5f, 0);
                positions.Add(worldPos);
            }
        }
        return positions;
    }

    void SpawnGroup(GameObject[] prefabs, int count, List<Vector3> positions)
    {
        if (prefabs.Length == 0) return;

        int spawned = 0;
        foreach (Vector3 pos in positions)
        {
            if (spawned >= count) break;
            if (!IsFarEnough(pos)) continue;
            if (IsTooCloseToHouse(pos)) continue; // ← house check

            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Instantiate(prefab, pos, Quaternion.identity, transform);
            spawnedPositions.Add(pos);
            spawned++;
        }
    }

    bool IsFarEnough(Vector3 pos)
    {
        foreach (Vector3 existing in spawnedPositions)
        {
            if (Vector3.Distance(pos, existing) < minDistanceBetweenProps)
                return false;
        }
        return true;
    }

    // ── new method — skip positions too close to the house ──
    bool IsTooCloseToHouse(Vector3 pos)
    {
        if (houseTransform == null) return false;
        return Vector3.Distance(pos, houseTransform.position) < houseRadius;
    }

    void Shuffle(List<Vector3> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}