using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour
{

    public GameObject target;

    private void Update()
    {
        if (target && !target.activeSelf)
        {
            target = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!target && other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target == other.gameObject)
        {
            target = null;
        }
            
    }
}
