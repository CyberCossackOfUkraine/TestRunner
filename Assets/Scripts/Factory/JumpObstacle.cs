using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObstacle : MonoBehaviour, IObstacle
{
    public void Spawn(Transform spawnPosition)
    {
        transform.position = new Vector3(spawnPosition.position.x, transform.position.y, spawnPosition.position.z);
        transform.SetParent(spawnPosition);
    }
}
