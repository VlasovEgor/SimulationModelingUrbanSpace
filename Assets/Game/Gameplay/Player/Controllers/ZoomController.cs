using Entities;
using System;
using UnityEngine;
using Zenject;

public class ZoomController : IInitializable, IDisposable
{
    private IComponent_Zoom _zoomComponent;
    private ManipulationInput _manipulationInput;

    [Inject]
    private void Construct(ManipulationInput manipulationInput, IEntity character)
    {
        _manipulationInput = manipulationInput;
        _zoomComponent = character.Get<IComponent_Zoom>();
    }

    void IInitializable.Initialize()
    {
        _manipulationInput.OnCameraZoom += Zoom;
    }

    void IDisposable.Dispose()
    {
        _manipulationInput.OnCameraZoom -= Zoom;
    }

    private void Zoom(float zoom)
    {
        _zoomComponent.Zoom(zoom);
    }
}
