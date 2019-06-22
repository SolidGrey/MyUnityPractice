using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class spawnEnemy
    {
        public ObjectPooler pooler;
        public float cooldown;
    }

    public Player player;
    public LevelManager map;
    public int waves = 3;
    public spawnEnemy[] enemy;

    int counter;
 
    //Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < waves; i++)
            for (int j = 0; j < enemy.Length; j++)
            {
                //InvokeRepeating("SpawnEnemy", enemy[j].cooldown, enemy[j].cooldown);
            }
            
    }

    void SpawnEnemy()
    {
        if (player.health <= 0f)
            return;
        //Debug.Log("Counter " + counter + " enemy.count " + enemy.Length);
        GameObject enemyObj = this.enemy[counter].pooler.GetPooledObject();
        Enemy enemyParam = enemyObj.GetComponent<Enemy>();
        if (enemyParam != null)
        {
            int route = Random.Range(0, map.routes.Count - 1);
            enemyParam.path = map.routes[route].ToArray();
            Debug.Log("map.routes[route] " + map.routes[route].Count);
        }
        enemyObj.SetActive(true);
    }


}
