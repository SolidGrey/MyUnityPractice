using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    public float speed;
    public GameObject target;

    private Enemy targetInfo;

    private void OnEnable()
    {
        targetInfo = target.GetComponent<Enemy>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (targetInfo.currentHealth <= 0 || !target.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            targetInfo.currentHealth -= damage;
            gameObject.SetActive(false);
        }
    }
}
