using System;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class FloatBehaviour : MonoBehaviour
{
    public event Action<float> OnValueChanged;

    public float Value
    {
        get { return _value; }
    }

    [SerializeField]
    private float _value;

    public void Assign(float value)
    {
        _value = value;
        OnValueChanged?.Invoke(value);
    }

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    public void Plus(float range)
    {
        var newValue = _value + range;
        Assign(newValue);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Minus(float range)
    {
        var newValue = _value - range;
        Assign(newValue);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Multiply(float multiplier)
    {
        var newValue = _value * multiplier;
        Assign(newValue);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Divide(float divider)
    {
        var newValue = _value / divider;
        Assign(newValue);
    }
}
