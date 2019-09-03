using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyManager : MonoBehaviour
{
    #region Attributes

    public enum GameAction {None, SelectBuilding0}

    [SerializeField]
    private KeyboardBinding[] _keyboardBindings;

    public KeyboardBindings keyboardBindings;

    #endregion

    #region Struct and Class

    [System.Serializable]
    private struct KeyboardBinding
    {
        public KeyCode key;
        public GameAction gameAction;
    }

    public class KeyboardBindings
    {
        private List<KeyCode> _keys;
        private List<GameAction> _gameActions;

        public KeyCode GetKeyCode(GameAction gameAction)
        {
            for (int i = 0; i < _gameActions.Count; i++)
            {
                if (_gameActions[i] == gameAction)
                    return _keys[i];
            }
            return KeyCode.None;
        }

        public GameAction GetGameAction(KeyCode keyCode)
        {
            for (int i = 0; i < _keys.Count; i++)
            {
                if (_keys[i] == keyCode)
                    return _gameActions[i];
            }
            return GameAction.None;
        }

        public void SetBind(KeyCode key, GameAction gameAction)
        {
            _keys.Add(key);
            _gameActions.Add(gameAction);
        }
    }

    #endregion

    #region Behaviour

    private void Start()
    {
        InitializeKeyboardBindings();
    }

    private void InitializeKeyboardBindings()
    {
        foreach(KeyboardBinding binding in _keyboardBindings)
            keyboardBindings.SetBind(binding.key, binding.gameAction);
    }
    #endregion
}
