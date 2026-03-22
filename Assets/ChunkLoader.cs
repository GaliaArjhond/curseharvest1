using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [Header("Chunk Settings")]
    [SerializeField] private float chunkSize = 16f;
    [SerializeField] private int loadRadius = 2;

    [Header("World")]
    [SerializeField] private Transform worldParent;

    private Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<Vector2Int, PropsSpawner> spawners = new Dictionary<Vector2Int, PropsSpawner>();
    private Vector2Int lastPlayerChunk;

    void Start()
    {
        foreach (Transform child in worldParent)
        {
            string[] parts = child.name.Split('_');
            if (parts.Length == 3 && parts[0] == "Chunk")
            {
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);
                Vector2Int key = new Vector2Int(x, y);

                chunks[key] = child.gameObject;

                // find PropsSpawner if this chunk has one
                PropsSpawner spawner = child.GetComponentInChildren<PropsSpawner>();
                if (spawner != null)
                    spawners[key] = spawner;

                child.gameObject.SetActive(false);
            }
        }

        UpdateChunks();
    }

    void Update()
    {
        Vector2Int currentChunk = GetChunkPosition(transform.position);

        if (currentChunk != lastPlayerChunk)
        {
            lastPlayerChunk = currentChunk;
            UpdateChunks();
        }
    }

    Vector2Int GetChunkPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / chunkSize);
        int y = Mathf.FloorToInt(worldPos.y / chunkSize);
        return new Vector2Int(x, y);
    }

    void UpdateChunks()
    {
        Debug.Log("Player chunk: " + lastPlayerChunk
                + " — loading " + (loadRadius * 2 + 1) + "x" + (loadRadius * 2 + 1) + " grid");

        foreach (var chunk in chunks)
            chunk.Value.SetActive(false);

        int loadedCount = 0;

        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2Int chunkPos = new Vector2Int(
                    lastPlayerChunk.x + x,
                    lastPlayerChunk.y + y
                );

                if (!chunks.ContainsKey(chunkPos)) continue;

                chunks[chunkPos].SetActive(true);
                loadedCount++;

                if (spawners.ContainsKey(chunkPos))
                {
                    if (!spawners[chunkPos].hasSpawned)
                        spawners[chunkPos].SpawnAll();
                    else
                        spawners[chunkPos].ShowAll();
                }
            }
        }

        Debug.Log("Chunks loaded: " + loadedCount + " / " + chunks.Count);
    }
}