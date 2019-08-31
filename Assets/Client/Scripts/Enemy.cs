using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float health = 3f;
    public float bounty = 1;
    public float damage = 1;
    public float speed = 2f;

    [HideInInspector]
    public float currentHealth;

    [HideInInspector]
    public Transform[] path;
    int waypointIndex = 0;


    void OnEnable()
    {
        currentHealth = health;
        gameObject.transform.position = path[path.Length - 1].position;
        waypointIndex = path.Length - 2;
    }

    void Update()
    {
        Moving();
        ChekingWounds();
    }

    private void Moving()
    {
        Vector3 direction = path[waypointIndex].position - gameObject.transform.position;
        gameObject.transform.LookAt(path[waypointIndex].position);
        gameObject.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(gameObject.transform.position, path[waypointIndex].position) <= 0.5f)
            GetNextWaypoint();
    }

    private void ChekingWounds()
    {
        if (currentHealth <= 0)
            gameObject.SetActive(false);
    }

    void GetNextWaypoint()
    {
        if (waypointIndex == 0)
            gameObject.SetActive(false);

        waypointIndex--;

    }
}
