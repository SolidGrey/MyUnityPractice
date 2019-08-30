using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementControler : MonoBehaviour
{
    public LevelManager levelManager;
    public Material canPlace;
    public Material canNotPlace;

    private MaterialReplacer _MaterialReplacer;
    private string activeMaterial = "CanNotPlace";
    private GameObject handlingObject;
    private GameObject building;
    private Transform activeCell;

    private void Update()
    {
        MoveHandlingObjectToMouse();
        ReleaseIfClicked();
    }

    public void HandleObject(GameObject pressedButton)
    {
        this.enabled = true;
        _MaterialReplacer = new MaterialReplacer();
        int index = pressedButton.transform.GetSiblingIndex();
        building = levelManager.availableBuildings[index].building;

        handlingObject = Instantiate(building);
        handlingObject.transform.position = new Vector3(0, -10f, 0);
        _MaterialReplacer.ChangeMaterial(handlingObject, canNotPlace);
        activeMaterial = "CanNotPlace";
    }

    private void MoveHandlingObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        if (hitInfo.collider)
        {
            handlingObject.SetActive(true);
            handlingObject.transform.position = hitInfo.collider.gameObject.transform.position;
        }  
        else
            handlingObject.SetActive(false);

        GameObject hitObject = hitInfo.collider.gameObject;
        activeCell = hitObject.transform;

        if (hitObject.tag != "FreeCell" && activeMaterial != "CanNotPlace")
        {
            _MaterialReplacer.ChangeMaterial(handlingObject, canNotPlace);
            activeMaterial = "CanNotPlace";
        }

        if (hitObject.tag == "FreeCell" && activeMaterial != "CanPlace")
        {
            _MaterialReplacer.ChangeMaterial(handlingObject, canPlace);
            activeMaterial = "CanPlace";
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && activeCell.tag == "FreeCell")
        {
            GameObject buildingClone = Instantiate(building);
            buildingClone.transform.position = handlingObject.transform.position;
            activeCell.transform.tag = "OccupiedCell";

            Destroy(handlingObject);
            this.enabled = false;
        }
    }
}
