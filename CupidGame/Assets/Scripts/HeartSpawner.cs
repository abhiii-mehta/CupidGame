using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject[] heartPrefabs;
    public Transform player;
    public float spawnRadius = 10f;
    public float spawnHeight = 7f;
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnHeart), 1f, spawnInterval);
    }

    void SpawnHeart()
    {
        if (player == null || heartPrefabs.Length == 0) return;

        Vector2 circle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = player.position + new Vector3(circle.x, spawnHeight, circle.y);

        int randomIndex = Random.Range(0, heartPrefabs.Length);
        Instantiate(heartPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
