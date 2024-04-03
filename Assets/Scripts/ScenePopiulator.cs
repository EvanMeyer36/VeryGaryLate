using UnityEngine;

public class ScenePopulator : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public int numberOfInstances = 10;
    public float spawnRadius = 10f;
    public float fixedYPosition = 1f;

    void Start()
    {
        PopulateScene();
    }

    void PopulateScene()
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPosition = transform.position + randomOffset;
            spawnPosition.y = fixedYPosition; // Set y component to fixed value
            Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
        }
    }
}
