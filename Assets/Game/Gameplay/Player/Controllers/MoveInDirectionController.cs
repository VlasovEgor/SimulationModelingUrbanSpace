using Entities;
using System;
using UnityEngine;
using Zenject;

public class MoveInDirectionController : IInitializable , IDisposable
{
    private IComponent_MoveInDirection _movingComponent;
    private ManipulationInput _manipulationInput;

    [Inject]
    private void Construct(ManipulationInput manipulationInput, IEntity character)
    {
        _manipulationInput=manipulationInput;
        _movingComponent = character.Get<IComponent_MoveInDirection>();
    }    

    void IInitializable.Initialize()
    {
        _manipulationInput.CameraMoved += OnDirectionMoved;
    }

    void IDisposable.Dispose()
    {
        _manipulationInput.CameraMoved -= OnDirectionMoved;
    }

    private void OnDirectionMoved(Vector3 screenDirection)
    {
        var worldDirection = new Vector3(screenDirection.x, 0.0f, screenDirection.z);
       _movingComponent.MoveInDirection(worldDirection);
    }

   
}
