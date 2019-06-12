using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButton : MonoBehaviour
{

    public void SetParam(Text playerParam)
    {
        int countIndex;
        int number;

        countIndex = playerParam.text.LastIndexOf(' '); // Search for last space in string
        number = Convert.ToInt32(playerParam.text.Substring(countIndex + 1)); //current number of something in string
        number++;
        playerParam.text = playerParam.text.Substring(0, countIndex + 1) + number;
    }

}
