using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 3f;
    public int bounty = 1;
    public int damage = 1;
    public float speed = 2f;
    public Transform[] path;
    int waypointIndex = 0;

    void OnEnable()
    {
        Debug.Log("path.Length " + path.Length);
        gameObject.transform.position = path[path.Length - 1].position;
        waypointIndex = path.Length - 2;
    }

    void Update()
    {
        Vector3 direction = path[waypointIndex].position - gameObject.transform.position;
        gameObject.transform.LookAt(path[waypointIndex].position);
        gameObject.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(gameObject.transform.position, path[waypointIndex].position) <= 0.5f)
            GetNextWaypoint();
    }

    void GetNextWaypoint()
    {
        if (waypointIndex == 0)
            gameObject.SetActive(false);

        waypointIndex--;

    }
}
