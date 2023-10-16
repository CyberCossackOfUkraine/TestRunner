using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    public IObstacle CreateRandomObstacle()
    {
        GameObject prefab = _obstacles[Random.Range(0, _obstacles.Length)];
        GameObject obj = Instantiate(prefab);
        return obj.GetComponent<IObstacle>();
    }
}
