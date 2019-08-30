using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public ObjectPooler pooler;
    public float coolDown;

    private void Fire(GameObject target)
    {
        GameObject bullet = pooler.GetPooledObject();
        bullet.transform.localPosition = new Vector3(0, 0, 0);
        BulletController launchParam = bullet.GetComponent<BulletController>();
        launchParam.Launch(target);
    }
}
