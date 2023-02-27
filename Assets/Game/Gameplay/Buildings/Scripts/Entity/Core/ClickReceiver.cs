using Entities;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour , IPointerClickHandler
{
    public event Action<UnityEntity> DoubleClicked;

    [SerializeField] private UnityEntity _entity;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("AMOGUS");
        if(eventData.clickCount == 2)
        {
            Debug.Log("Double click" + eventData.pointerPressRaycast.gameObject.name);
            DoubleClicked.Invoke(_entity);
        }
    }
}
