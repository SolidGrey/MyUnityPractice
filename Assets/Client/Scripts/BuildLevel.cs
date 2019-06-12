using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildLevel : MonoBehaviour
{
    public Transform field;
    public Transform road;

    void Start()
    {
        string[] gridPattern = LoadGridPattern(1);
        CreateLevel(gridPattern);
    }
    
    void CreateLevel(string[] gridPattern)
    {
        float xPosition = 0.5f;
        float zPosition = -0.5f;
        float xStartPosition = xPosition; //Set up start position
        
        foreach (string line in gridPattern)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '0')
                    Instantiate(field, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                else
                    Instantiate(road, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                xPosition++;
            }
            zPosition--;
            xPosition = xStartPosition;
        }
            
    }

    string[] LoadGridPattern(int i)
    {
        TextAsset tmpText = Resources.Load<TextAsset>("Level" + i); //Import level matrix from .txt file
        string tmpGrid = tmpText.text.Replace(Environment.NewLine, ";"); //Replace all line break characters with ';' character

        return tmpGrid.Split(';'); //Return array with level matrix
    }
}