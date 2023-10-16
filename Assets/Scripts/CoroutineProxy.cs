using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineProxy : MonoBehaviour
{
    public static CoroutineProxy instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
            Debug.Log("THIS");
        }
    }

    public Coroutine StartProxyCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
