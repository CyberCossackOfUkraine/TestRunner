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
        int rand;

        do
        {
            rand = Random.Range(0, _obstacles.Length);
        } while (rand == oldRand & ++sameObstacleCounter >= 2);

        if (oldRand == rand)
            sameObstacleCounter++;
        else
            sameObstacleCounter = 0;

        oldRand = rand;
        return rand;
    }
}
