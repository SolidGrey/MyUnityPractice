using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEnemyParam
    {
        public ObjectPooler pooler;
        public float cooldown; //Пока не используется
    }
    public Player player;
    public LevelManager map;
    public int waves = 3;
    public float pauseBetweenWaves = 5f;
    public float pauseBetweenSpawns = 0.3f;
    public SpawnEnemyParam[] enemy;
 

    //Start is called before the first frame update
    void Start()
    {
         StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        Debug.Log("Hey");
        int enemyMult = 1;
        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < enemy.Length; j++)
            {
                for (int k = 0; k < enemyMult; k++)
                {
                    SpawnEnemy(j);
                    yield return new WaitForSeconds(pauseBetweenSpawns);
                }
                enemyMult++;
            }
            yield return new WaitForSeconds(pauseBetweenWaves);
        }
        
    }

    void SpawnEnemy(int index)
    {
        if (player.health <= 0f)
            return;
        GameObject enemyObj = enemy[index].pooler.GetPooledObject();
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
