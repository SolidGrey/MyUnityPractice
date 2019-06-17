using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Player player;
    public ObjectPooler objectPooler;
    public float spawnTime = 5f;

    int wave = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wave; i++)
            InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (player.health <= 0f)
            return;
        objectPooler.GetPooledObject();
    }


}
