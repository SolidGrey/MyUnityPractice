using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonProperties : MonoBehaviour, IButton
{
    [SerializeField]
    private GameObject buttonName;
    [SerializeField]
    private GameObject buttonHotkey;

    private string _name;
    private HotkeyManager.GameAction _action;
    private HotkeyManager hotkeyManager;

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public HotkeyManager.GameAction Action
    {
        get => _action;
        set => _action = value;
    }

    public void InitializeButton(string name, HotkeyManager.GameAction action)
    {
        hotkeyManager = GameObject.FindGameObjectWithTag("HotkeyManager").GetComponent<HotkeyManager>();
        Name = name;
        Action = action;
        buttonName.GetComponent<Text>().text = Name;
        buttonHotkey.GetComponent<Text>().text = hotkeyManager.keyboardBindings.GetKeyCode(Action).ToString();
    }
}
