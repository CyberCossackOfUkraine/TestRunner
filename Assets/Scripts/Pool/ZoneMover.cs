using UnityEngine;

public class ZoneMover : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int startPoolAmount;
    [SerializeField] private ObjectPooler _objectPooler;
    [SerializeField] private ObjectPoolerScriptableObject _objectPoolerScriptableObject;
    
    private Transform _nextSpawnPoint;

    public void SetNextSpawnPoint(Transform spawnPoint)
    {
       _nextSpawnPoint = spawnPoint;
    }

    public void ActivateZones(int amount)
    {
        SpawnNextZone(amount);
    }

    public void SpawnNextZone(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var zoneTile = _objectPooler.GetPoolObject();
            zoneTile.transform.position = _nextSpawnPoint.position;
            zoneTile.SetActive(true);
        }
    }
}
