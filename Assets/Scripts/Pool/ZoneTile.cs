using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTile : MonoBehaviour
{
    [SerializeField] private Transform _nextSpawnPoint;
    public Transform[] obstacleSpawnPoint;


    private void OnEnable()
    {
        ZoneMover.instance.SetNextSpawnPoint(_nextSpawnPoint);
    }

    private void OnTriggerExit(Collider other)
    {
        DisableZone();
    }

    private void DisableZone()
    {
        gameObject.SetActive(false);
        ZoneMover.instance.SpawnNextZone(1);
    }
}
