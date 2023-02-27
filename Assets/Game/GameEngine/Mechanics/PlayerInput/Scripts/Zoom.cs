using System;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private FloatBehaviour _zoomSercetivity;
    [SerializeField] private FloatEventReceiver _zoomReceiver;
    [SerializeField] private Camera _camera;

    private void OnEnable()
    {
        _zoomReceiver.OnEvent += OnZooming;
    }

    private void OnDisable()
    {
        _zoomReceiver.OnEvent -= OnZooming;
    }

    private void OnZooming(float zoom)
    {
        _camera.fieldOfView += zoom * _zoomSercetivity.Value;
    }
}
