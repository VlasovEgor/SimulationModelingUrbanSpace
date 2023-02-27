using Entities;
using System;
using UnityEngine;

public class Component_DoubleClickBuilderReceiver : MonoBehaviour, IComponent_DoubleClickBuilderReceiver
{
    [SerializeField] private ClickReceiver _clickReceiver;

    public event Action<UnityEntity> OnClickBuilding
    {
        add { _clickReceiver.DoubleClicked += value; }
        remove { _clickReceiver.DoubleClicked -= value; }
    }
}
