using System.Collections.Generic;
using UnityEngine;

public class PropsSpawner : MonoBehaviour
{
    [Header("Spawn Zone")]
    [SerializeField] private PolygonCollider2D spawnZone; // drag SpawnZone here

    [Header("Props")]
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private GameObject[] stonePrefabs;
    [SerializeField] private GameObject[] vegetationPrefabs;

    [Header("Spawn Count")]
    [SerializeField] private int treeCount = 15;
    [SerializeField] private int stoneCount = 20;
    [SerializeField] private int vegCount = 10;

    [Header("Spacing")]
    [SerializeField] private float minDistanceBetweenProps = 1.5f;
    [SerializeField] private int maxAttempts = 30;

    [Header("House Exclusion")]
    [SerializeField] private Transform houseTransform;
    [SerializeField] private float houseRadius = 4f;

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        SpawnGroup(treePrefabs, treeCount);
        SpawnGroup(stonePrefabs, stoneCount);
        SpawnGroup(vegetationPrefabs, vegCount);
    }

    void SpawnGroup(GameObject[] prefabs, int count)
    {
        if (prefabs.Length == 0) return;

        int spawned = 0;
        int attempts = 0;

        while (spawned < count && attempts < count * maxAttempts)
        {
            attempts++;

            // get a random point inside the polygon bounds
            Vector3 randomPos = GetRandomPointInPolygon();

            // check it's actually inside the polygon shape
            if (!spawnZone.OverlapPoint(randomPos)) continue;

            // check spacing from other props
            if (!IsFarEnough(randomPos)) continue;

            // check house exclusion
            if (IsTooCloseToHouse(randomPos)) continue;

            // spawn it
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Instantiate(prefab, randomPos, Quaternion.identity, transform);
            spawnedPositions.Add(randomPos);
            spawned++;
        }
    }

    Vector3 GetRandomPointInPolygon()
    {
        // get the bounding box of the polygon
        Bounds bounds = spawnZone.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y, 0);
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

    bool IsTooCloseToHouse(Vector3 pos)
    {
        if (houseTransform == null) return false;
        return Vector3.Distance(pos, houseTransform.position) < houseRadius;
    }
}