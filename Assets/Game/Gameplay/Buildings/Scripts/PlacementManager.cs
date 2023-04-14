using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager
{
    private Dictionary<Vector3, BuildingConfig> _residentialBuildingsDictionary = new();
    private Dictionary<Vector3, BuildingConfig> _commericalBuildingsDictionary = new();

    public void AddBuilding(BuildingConfig buildingConfig)
    {
        var position = buildingConfig.GetPosition();

        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            _residentialBuildingsDictionary.Add(position, buildingConfig);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            _commericalBuildingsDictionary.Add(position, buildingConfig);
        }
    }

    public void RemoveBuilding(BuildingConfig buildingConfig)
    {
        var position = buildingConfig.GetPosition();

        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            _residentialBuildingsDictionary.Remove(position);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            _commericalBuildingsDictionary.Remove(position);
        }
    }

    public BuildingConfig GetRandomBuildingCertainType(VertexType vertexType)
    {
        if (vertexType == VertexType.Residential_Building)
        {
            List<BuildingConfig> buildingConfigs = Enumerable.ToList(_residentialBuildingsDictionary.Values);
            var randmIndex = Random.Range(0, buildingConfigs.Count);
            return buildingConfigs[randmIndex];


        }
        else if (vertexType == VertexType.Commercial_Building)
        {
            List<BuildingConfig> buildingConfigs = Enumerable.ToList(_commericalBuildingsDictionary.Values);
            var randmIndex = Random.Range(0, buildingConfigs.Count);
            return buildingConfigs[randmIndex];
        }

        throw new System.Exception("there are no buildings of this type");
    }

}
