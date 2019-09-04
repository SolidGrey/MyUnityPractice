using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonProperties : MonoBehaviour, IButton
{
    public string name;
    public HotkeyManager.GameAction gameAction;
    private string Name => name;
    public HotkeyManager.GameAction GameAction => gameAction;

    public void InitializeButton()
    {

    }
}
