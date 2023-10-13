using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTile : MonoBehaviour
{
    [SerializeField] private Transform _nextSpawnPoint;

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
        ZoneMover.instance.SpawnNextZone(1);
        gameObject.SetActive(false);
    }
}
