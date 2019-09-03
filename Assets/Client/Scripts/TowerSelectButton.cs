using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectButton : MonoBehaviour
{
    private PlacementControler placementControler;

    private void Start()
    {
        placementControler = GameObject.FindGameObjectWithTag("PlacementController").GetComponent<PlacementControler>();

        if (!placementControler)
            Debug.LogError("Placement Controller not found!");
    }

    public void OnButtonDown()
    {
        if (!placementControler)
            return;

        placementControler.HandleObject(gameObject);
    }
}
