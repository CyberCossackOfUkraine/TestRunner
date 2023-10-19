using UnityEngine;

public class ZoneTile : MonoBehaviour
{
    [SerializeField] private Transform _nextSpawnPoint;
    public Transform[] obstacleSpawnPoint;

    private void OnEnable()
    {
        Singleton.Instance.ZoneMover.SetNextSpawnPoint(_nextSpawnPoint);
    }

    private void OnTriggerExit(Collider other)
    {
        DisableZone();
    }

    private void DisableZone()
    {
        gameObject.SetActive(false);
        Singleton.Instance.ZoneMover.SpawnNextZone(1);
    }
}
