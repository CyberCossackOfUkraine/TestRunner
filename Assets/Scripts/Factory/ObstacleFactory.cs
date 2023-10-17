using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    private int sameObstacleCounter;
    private int oldRand;
    public IObstacle CreateRandomObstacle()
    {
        GameObject prefab = _obstacles[GetRand()];
        GameObject obj = Instantiate(prefab);
        return obj.GetComponent<IObstacle>();
    }

    private int GetRand() {
        int rand = Random.Range(0, _obstacles.Length);

        if (oldRand == rand)
            sameObstacleCounter++;
        else
            sameObstacleCounter = 0;

        if (sameObstacleCounter >= 2)
            GetRand();

        oldRand = rand;

        return rand;
    }
}
