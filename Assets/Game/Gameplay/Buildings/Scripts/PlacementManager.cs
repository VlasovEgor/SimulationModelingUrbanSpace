using System.Collections.Generic;
using UnityEngine;

public class PlacementManager
{
    private List<BuildingConfig> _listResidentialBuildings = new List<BuildingConfig>();
    private List<BuildingConfig> _listCommericalBuildings = new List<BuildingConfig>();
    
    public void AddBuilding(BuildingConfig buildingConfig)
    {
        if(buildingConfig.GetVertexType() == VertexType.Residential_Building) 
        {
            _listResidentialBuildings.Add(buildingConfig);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            _listCommericalBuildings.Add(buildingConfig);
        }
    }

    public void RemoveBuilding(BuildingConfig buildingConfig)
    {
        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            _listResidentialBuildings.Remove(buildingConfig);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            _listCommericalBuildings.Remove(buildingConfig);
        }
    }

    public List<BuildingConfig> GetAllBuildingConfigurationCertainType(VertexType buidlingType)
    {
        if (buidlingType == VertexType.Residential_Building)
        {
          return  _listResidentialBuildings;
        }
        else if (buidlingType == VertexType.Commercial_Building)
        {
           return _listCommericalBuildings;
        }

        throw new System.Exception("there are no buildings of this type");
    }

    public BuildingConfig GetRandomBuildingCertainType(VertexType vertexType)
    {
        if(vertexType == VertexType.Residential_Building) 
        {
            var index = Random.Range(0, _listResidentialBuildings.Count-1);
            return _listResidentialBuildings[index];
        }
        else if(vertexType == VertexType.Commercial_Building)
        {
            var index = Random.Range(0, _listCommericalBuildings.Count - 1);
            return _listCommericalBuildings[index];
        }

        throw new System.Exception("there are no buildings of this type");
    }

}
