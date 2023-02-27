using Sirenix.OdinInspector;
using System;

using UnityEngine;

public class FloatEventReceiver : MonoBehaviour
{
    public event Action<float> OnEvent;

    [Button]
    public void Call(float value)
    {
        OnEvent?.Invoke(value);
    }
}
