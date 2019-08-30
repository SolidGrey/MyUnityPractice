using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private GameObject tower;

    [SerializeField]
    private GameObject triggerZone;

    public float fireRange = 0f;

    private GameObject target;
    private TargetTracker _TargetTracker;

    private void Start()
    {
        
    }

    void Update()
    {
        Tracking();
    }

    

    private void Tracking()
    {
        if (target)
            tower.transform.LookAt(target.transform);
    }

    private void TargetSearch()
    {
        if (!target)
        {

        }
    }

}
