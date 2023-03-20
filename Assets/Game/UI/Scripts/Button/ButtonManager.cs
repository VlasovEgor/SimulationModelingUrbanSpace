using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour, ButtonUI.ICallback
{
    [SerializeField] private ButtonHolder[] _allButton;

    private readonly Dictionary<ButtonName, ButtonUI> _activeButton = new();

    private void Awake()
    {
        foreach (var buttonHolder in _allButton)
        {
            ShowButton(buttonHolder.Name);
        }
    }

    [Button]
    public void ShowButton(ButtonName name, object args = null)
    {
        if (IsButtonActive(name) == true)
        {
            return;
        }

        var button = FindButton(name);
        button.gameObject.SetActive(true);
        button.Show(args: args, callback: this);
        _activeButton.Add(name, button);
    }

    [Button]
    public void HideButton(ButtonName name)
    {
        if (IsButtonActive(name) == false)
        {
            return;
        }

        var popup = _activeButton[name];
        popup.Hide();
        popup.gameObject.SetActive(false);
        _activeButton.Remove(name);
    }

    [Button]
    public bool IsButtonActive(ButtonName name)
    {
        return _activeButton.ContainsKey(name);
    }

    void ButtonUI.ICallback.OnClose(ButtonUI button)
    {
        var name = FindName(button);
        HideButton(name);
    }

    private ButtonName FindName(ButtonUI button)
    {
        foreach (var holder in _allButton)
        {
            if (ReferenceEquals(holder.Button, button))
            {
                return holder.Name;
            }
        }

        throw new Exception($"Name of button {button.name} is not found!");
    }

    private ButtonUI FindButton(ButtonName name)
    {
        foreach (var holder in _allButton)
        {
            if (holder.Name == name)
            {
                return holder.Button;
            }
        }

        throw new Exception($"Button with name {name} is not found!");
    }

    [Serializable]
    private struct ButtonHolder
    {
        [SerializeField]
        public ButtonName Name;

        [SerializeField]
        public ButtonUI Button;
    }
}
