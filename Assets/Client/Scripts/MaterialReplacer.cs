using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReplacer : MonoBehaviour
{
    public void ChangeMaterial(GameObject gameObject, Material material)
    {
        Renderer objectRenderer = gameObject.GetComponent<Renderer>();
        if (objectRenderer)
        {
            objectRenderer.material = material;
        }

        foreach(Transform child in gameObject.transform)
        {
            ChangeMaterial(child.gameObject, material);
        }
        
    }
}
