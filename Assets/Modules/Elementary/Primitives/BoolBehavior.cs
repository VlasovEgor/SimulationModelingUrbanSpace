using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class BoolBehavior : MonoBehaviour
{
    public event Action<bool> OnValueChanged;

    public bool Value
    {
        get { return this.value; }
    }

    [SerializeField]
    private bool value;

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    public void Assign(bool value)
    {
        this.value = value;
        this.OnValueChanged?.Invoke(value);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void AssignTrue()
    {
        this.Assign(true);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void AssignFalse()
    {
        this.Assign(false);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        this.Assign(this.value);
    }
#endif
}
