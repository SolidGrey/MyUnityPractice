using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;
    public int pooledAmount = 10;
    public bool willGrow = true;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Start()
    {
        //Add objects to List
        for (int i = 0; i < pooledAmount; i++)
        {
            AddObject();
        }
    }

    //Return disabled object
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
            if (!pooledObjects[i].activeSelf)
            {
                return pooledObjects[i];
            }
                

        if (willGrow)
        {
            AddObject();
            return pooledObjects[pooledObjects.Count - 1];
        }

        return null;
    }

    private void AddObject()
    {
        GameObject obj = Instantiate(pooledObject);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pooledObjects.Add(obj);
    }

}
