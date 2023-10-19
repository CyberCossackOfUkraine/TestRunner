using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private ObjectPoolerScriptableObject _objectPoolerScriptableObject;

    private List<ZoneTile> _poolObjects;
    private int _poolAmount;
    private bool _willGrow;

    private List<ZoneTile> _poolList;
    private ObstacleFactory _obstacleFactory;

    private void Awake()
    {
        _obstacleFactory = GetComponent<ObstacleFactory>();
        _poolObjects = _objectPoolerScriptableObject.poolObjectVariants;
        _poolAmount = _objectPoolerScriptableObject.poolAmount;
        _willGrow = _objectPoolerScriptableObject.willGrow;

        CreatePool();
    }

    private void CreatePool()
    {
        _poolList = new List<ZoneTile>();

        int index = 0;
        for(int i = 0; i < _poolAmount; i++)
        {
            if (index >= _poolObjects.Count)
            {
                index = 0;
            }

            ZoneTile newZone = Instantiate(_poolObjects[index]);
            index++;

            newZone.transform.SetParent(transform, true);

            CreateObstacle(newZone);

            newZone.gameObject.SetActive(false);
            _poolList.Add(newZone);
        }
    }

    private void CreateObstacle(ZoneTile zone)
    {
        Transform[] obstacleSpawnPoints = zone.obstacleSpawnPoint;
        for(int i = 0; i < obstacleSpawnPoints.Length; i++)
        {
            float rand = Random.value;
            if (rand < .5f)
            {
                IObstacle obstacle = _obstacleFactory.CreateRandomObstacle();
                obstacle?.Spawn(obstacleSpawnPoints[i]);
            }
        }
    }

    public GameObject GetPoolObject()
    {
        var inactiveObject = _poolList.Where(obj => obj.gameObject.activeInHierarchy == false);

        if(inactiveObject.Count() > 0)
        {
            int index = Random.Range(0, inactiveObject.Count());
            return inactiveObject.ElementAt(index).gameObject;
        }

        return null;
    }
}
