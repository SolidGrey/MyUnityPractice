﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public GameObject pooledObject;
    public int pooledAmount = 10;
    public bool willGrow = true;

    List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        //Add objects to List
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    //Return object for another functions
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
            if (!pooledObjects[i].activeSelf)
                return pooledObjects[i];
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return pooledObjects[pooledObjects.Count - 1];
        }
        return null;
    }

}
