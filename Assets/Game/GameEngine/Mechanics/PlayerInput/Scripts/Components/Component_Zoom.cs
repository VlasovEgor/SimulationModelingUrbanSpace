using UnityEngine;

public class Component_Zoom : MonoBehaviour, IComponent_Zoom
{
    [SerializeField] private FloatEventReceiver _zoomReceiver;

    public void Zoom(float zoom)
    {
        _zoomReceiver.Call(zoom);
    }
}
