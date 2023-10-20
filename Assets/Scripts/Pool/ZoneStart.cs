using UnityEngine;

public class ZoneStart : MonoBehaviour
{
    [SerializeField] private Transform _nextSpawnPosition;
    [SerializeField] private ObjectPoolerScriptableObject _objectPoolerScriptableObject;

    private void Start()
    {
        Singleton.Instance.ZoneMover.SetNextSpawnPoint(_nextSpawnPosition);
        Singleton.Instance.ZoneMover.ActivateZones(_objectPoolerScriptableObject.poolAmount);
    }

    private void OnTriggerExit(Collider other)
    {
        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
