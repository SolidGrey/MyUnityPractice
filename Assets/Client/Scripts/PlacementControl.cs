using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementControl : MonoBehaviour
{
    public LevelManager levelManager;
    private GameObject handlingObject;

    private void Update()
    {
        if (handlingObject)
        {
            MoveHandlingObjectToMouse();
        }
    }

    public void HandleObject(GameObject pressedButton)
    {
        int index = pressedButton.transform.GetSiblingIndex();
        GameObject building = levelManager.availableBuildings[index].building;

        if (handlingObject == null)
        {
            handlingObject = Instantiate(building);
        }
        else
        {
            Destroy(handlingObject);
        }
    }

    private void MoveHandlingObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider.gameObject.tag == "FreeCell" )
        {
            handlingObject.transform.position = hitInfo.collider.gameObject.transform.position;
        }
    }


}
