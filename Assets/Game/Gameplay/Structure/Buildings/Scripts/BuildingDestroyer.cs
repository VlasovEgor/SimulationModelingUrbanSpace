using System;
using Zenject;

public class BuildingDestroyer : IInitializable, IDisposable
{
    [Inject] private ManipulationInput manipulationInput;
    [Inject] private BuildingSelector _buildingSelector;

    public void Initialize()
    {
        manipulationInput.DeleteButtonClicked += DeleteBuilding;
    }

    public void Dispose()
    {
        manipulationInput.DeleteButtonClicked -= DeleteBuilding;
    }

    private void DeleteBuilding()
    {
        var currentBulding = _buildingSelector.GetCurrentBuilding();

        if (currentBulding == null)
        {
            return;
        }

        currentBulding.Get<IComponent_DestroyBuilding>().DestroyBuilding();
    }
}
