using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildLevel : MonoBehaviour
{
    public GameObject map;
    public GameObject field;
    public GameObject road;
    public GameObject spawner;
    public GameObject despawner;

    void Start()
    {

        string[] gridPattern = LoadGridPattern(1);
        CreateLevel(gridPattern);
    }
    
    // Instantiate field and road cells on scene
    void CreateLevel(string[] gridPattern)
    {
        float xPosition = 0f;
        float zPosition = 0f;
        float xStartPosition = xPosition; //Set up start position
        Vector3 position;
        
        foreach (string line in gridPattern)
        {
            for (int i = 0; i < line.Length; i++)
            {
                position = new Vector3(xPosition, 0, zPosition);
                if (line[i] == '0')
                    Instantiate(field, position, Quaternion.identity, map.transform);
                else
                {
                    Instantiate(road, position, Quaternion.identity, map.transform);

                    // Set spawn & despawn position
                    switch (line[i])
                    {
                        case 'I':
                            spawner.transform.position = position;
                            break;
                        case 'O':
                            despawner.transform.position = position;
                            break;
                    }

                }    
                xPosition += 5f;
            }
            zPosition -= 5f;
            xPosition = xStartPosition;
        }
            
    }

    // Import level from resources. Input parameter requires level number
    string[] LoadGridPattern(int i)
    {
        TextAsset tmpText = Resources.Load<TextAsset>("Level" + i); //Import level matrix from .txt file
        string tmpGrid = tmpText.text.Replace(Environment.NewLine, ";"); //Replace all line break characters with ';' character
        if (tmpGrid != null)
            return tmpGrid.Split(';'); //Return array with level matrix
        else
        {
            Debug.Log("Level not found");
            return null;
        }
    }
}