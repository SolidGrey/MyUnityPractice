using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private GameObject tower;

    [SerializeField]
    private GameObject triggerZone;

    [SerializeField]
    private GameObject ammo;

    public float coolDown = 3f;

    private ObjectPooler ammoPooler;

    private bool isReadyToFire = true;

    private TargetTracker targetTracker;

    #endregion

    #region Behavior

    private void Start()
    {
        targetTracker = triggerZone.GetComponent<TargetTracker>();
        InitializeObjectPooler();
    }

    private void Update()
    {
        Tracking();
        StartCoroutine("Firing");
    }

    private void Tracking()
    {
        if (targetTracker.target)
            tower.transform.LookAt(targetTracker.target.transform);
    }

    private void InitializeObjectPooler()
    {
        GameObject objectPoolers = GameObject.FindWithTag("ObjectPoolers");
        foreach (Transform child in objectPoolers.transform)
        {
            ammoPooler = child.GetComponent<ObjectPooler>();
            if (ammoPooler.pooledObject == ammo)
            {
                return;
            }
        }
        ammoPooler = null;
        Debug.LogError("Object pooler not found.");
        gameObject.SetActive(false);
    }

    private IEnumerator Firing()
    {
        if (targetTracker.target && isReadyToFire)
        {
            GameObject bullet = ammoPooler.GetPooledObject();
            bullet.transform.position = firePoint.transform.position;
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.target = targetTracker.target;
            bullet.SetActive(true);
            isReadyToFire = false;
            yield return new WaitForSeconds(coolDown);
            isReadyToFire = true;
        }
        else
            yield return null;

    }

    #endregion

}
