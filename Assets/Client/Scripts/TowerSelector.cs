using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public Vector3 localButtonPosition = new Vector3(0, 0, 0);

    public void PlaceButton(GameObject button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        button.transform.SetParent(gameObject.transform, false);
        button.transform.localPosition = localButtonPosition;

        localButtonPosition.x += rectTransform.sizeDelta.x;

        transform.Translate((rectTransform.sizeDelta.x / -2f), 0, 0);
    }
}
