using Entities;
using System;
using UnityEngine;
using Zenject;

public class BoostController : IInitializable, IDisposable
{
    private IComponent_Boost _boostComponent;
    private ManipulationInput _manipulationInput;

    public IComponent_Boost IComponent_Boost
    {
        get => default;
        set
        {
        }
    }

    [Inject]
    private void Construct(ManipulationInput manipulationInput, IEntity character)
    {
        _manipulationInput = manipulationInput;
        _boostComponent = character.Get<IComponent_Boost>();
    }

    void IInitializable.Initialize()
    {
        _manipulationInput.CameraBoosted += OnBoost;
    }

    void IDisposable.Dispose()
    {
        _manipulationInput.CameraBoosted -= OnBoost;
    }

    private void OnBoost(bool isBoost)
    {
        _boostComponent.CanBoost(isBoost);
    }
}
