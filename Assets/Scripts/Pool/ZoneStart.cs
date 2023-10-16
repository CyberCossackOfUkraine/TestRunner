using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneStart : MonoBehaviour
{
    [SerializeField] private Transform _nextSpawnPosition;
    [SerializeField] private ObjectPoolerScriptableObject _objectPoolerScriptableObject;

    private void OnTriggerEnter(Collider other)
    {
        ZoneMover.instance.SetNextSpawnPoint(_nextSpawnPosition);
        ZoneMover.instance.ActivateZones(_objectPoolerScriptableObject.poolAmount);
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
