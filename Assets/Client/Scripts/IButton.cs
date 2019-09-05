using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton
{
    string Name { get; set; }
    HotkeyManager.GameAction Action { get; set; }

    void InitializeButton(string name, HotkeyManager.GameAction action);
}
