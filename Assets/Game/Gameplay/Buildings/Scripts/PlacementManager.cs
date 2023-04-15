using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager
{
    // private Dictionary<Vector3, BuildingConfig> _residentialBuildingsDictionary = new();
    // private Dictionary<Vector3, BuildingConfig> _commericalBuildingsDictionary = new();

    private List<BuildingConfig> _residentialBuildingsList = new();
    private List<BuildingConfig> _commericalBuildingsList = new();

    public void AddBuilding(BuildingConfig buildingConfig)
    {
        var position = buildingConfig.GetPosition();

        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            // _residentialBuildingsDictionary.Add(position, buildingConfig);
            // Debug.Log(_residentialBuildingsDictionary.Count);
            _residentialBuildingsList.Add(buildingConfig);
            Debug.Log(_residentialBuildingsList.Count);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            // _commericalBuildingsDictionary.Add(position, buildingConfig);
            //Debug.Log(_commericalBuildingsDictionary.Count);
            _commericalBuildingsList.Add(buildingConfig);
            Debug.Log(_commericalBuildingsList.Count);
            Debug.Log(buildingConfig.GetPosition());
        }
    }

    public void RemoveBuilding(BuildingConfig buildingConfig)
    {
        var position = buildingConfig.GetPosition();

        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            // _residentialBuildingsDictionary.Remove(position);
            _residentialBuildingsList.Remove(buildingConfig);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            // _commericalBuildingsDictionary.Remove(position);
            _commericalBuildingsList.Remove(buildingConfig);
        }
    }

    public BuildingConfig GetRandomBuildingCertainType(VertexType vertexType)
    {
        if (vertexType == VertexType.Residential_Building)
        {
            //List<BuildingConfig> buildingConfigs = Enumerable.ToList(_residentialBuildingsDictionary.Values);
            //var randmIndex = Random.Range(0, buildingConfigs.Count - 1);
            var randmIndex = Random.Range(0, _residentialBuildingsList.Count);
            return _residentialBuildingsList[randmIndex];


        }
        else if (vertexType == VertexType.Commercial_Building)
        {
            //  List<BuildingConfig> buildingConfigs = Enumerable.ToList(_commericalBuildingsDictionary.Values);
            // var randmIndex = Random.Range(0, buildingConfigs.Count - 1);
            // return buildingConfigs[randmIndex];

            var randmIndex = Random.Range(0, _commericalBuildingsList.Count);
            return _commericalBuildingsList[randmIndex];
        }

        throw new System.Exception("there are no buildings of this type");
    }

    public List<BuildingConfig> GetBuildingsListCertainType(VertexType vertexType)
    {
        if (vertexType == VertexType.Residential_Building)
        {
            return _residentialBuildingsList;


        }
        else if (vertexType == VertexType.Commercial_Building)
        {
            return _commericalBuildingsList;
        }

        throw new System.Exception("there are no buildings of this type");
    }

}
