using Entities;
using System;
using UnityEngine;
using Zenject;

public class RotateController : IInitializable, IDisposable
{   
     private IComponent_Rotate _rotateComponent;
     private ManipulationInput _manipulationInput;

    [Inject]
    private void Construct(ManipulationInput manipulationInput, IEntity character)
    {
        _manipulationInput = manipulationInput;
        _rotateComponent = character.Get<IComponent_Rotate>();
    }

    void IInitializable.Initialize()
    {
        _manipulationInput.OnCameraRotate += Rotate;
    }

    void IDisposable.Dispose()
    {
        _manipulationInput.OnCameraRotate -= Rotate;
    }

     private void Rotate(Vector2 rotate)
     {
         _rotateComponent.Rotate(rotate);
     }
}