using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour, IObstacle
{
    private PlayerStateController _playerStateController;
    private void Awake()
    {
        _playerStateController = PlayerStateController.instance;
    }
    public void Spawn(Transform spawnPosition)
    {
        transform.position = new Vector3(spawnPosition.position.x, transform.position.y, spawnPosition.position.z);
        transform.SetParent(spawnPosition);
    }
    
    private void OnTriggerStay(Collider other)
    {
        _playerStateController.SetStateDead();
    }
}
