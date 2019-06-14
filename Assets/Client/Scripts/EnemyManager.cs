using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject spawner;
    public Player player;
    public float spawnTime = 5f;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Spawn()
    {
        if (player.health <= 0f)
            return;

    }
}
