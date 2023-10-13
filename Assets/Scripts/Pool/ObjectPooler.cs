using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private List<GameObject> _poolObjects;
    private int _poolAmount;
    private bool _willGrow;

    private List<GameObject> _poolList;

    [SerializeField] private ObjectPoolerScriptableObject _objectPoolerScriptableObject;

    private void Awake()
    {
        _poolObjects = _objectPoolerScriptableObject.poolObjectVariants;
        _poolAmount = _objectPoolerScriptableObject.poolAmount;
        _willGrow = _objectPoolerScriptableObject.willGrow;

        CreatePool();
    }

    private void CreatePool()
    {
        _poolList = new List<GameObject>();

        int index = 0;
        for(int i = 0; i < _poolAmount; i++)
        {
            if (index >= _poolObjects.Count)
            {
                index = 0;
            }

            GameObject newObject = Instantiate(_poolObjects[index]);
            index++;

            newObject.transform.SetParent(transform, true);
            newObject.SetActive(false);
            _poolList.Add(newObject);
        }
    }

    public GameObject GetPoolObject()
    {
        var inactiveObject = _poolList.Where(obj => obj.activeInHierarchy == false);

        if(inactiveObject.Count() > 0)
        {
            int index = UnityEngine.Random.Range(0, inactiveObject.Count());
            return inactiveObject.ElementAt(index);
        }

        return null;
    }
}
