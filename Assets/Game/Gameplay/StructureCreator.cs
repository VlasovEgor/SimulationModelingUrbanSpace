using Zenject;

public class StructureCreator
{
    [Inject] private BuildingCreator _buildingCreator;
    [Inject] private RoadSystem _roadSystem;

    public void Create(Building building)
    {   
        if (building.Type == VertexType.Commercial_Building || building.Type == VertexType.Residential_Building)
        {
            _buildingCreator.Create(building.BuildingPrefab);
        }
        else if (building.Type == VertexType.Road)
        {
            _roadSystem.SetPavedRoad(building.BuildingPrefab);
            _roadSystem.EnableAbilityBuildRoad();
        }
    }
}