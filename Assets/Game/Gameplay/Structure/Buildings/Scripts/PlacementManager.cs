using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlacementManager : IInitializable, IDisposable
{
    public event Action<CommericalBuildingConfig, Education> OnEmpolyeeAdded;
    public event Action<CommericalBuildingConfig, Education> OnEmpolyeeRemoved;
    public event Action OnCommericalBuildingAdded;

    [Inject] private CommericalBuildingConfigRedactor _configRedactor;

    private List<ResidentialBuildingConfig> _residentialBuildingsList = new();
    private List<CommericalBuildingConfig> _commericalBuildingsList = new();

    private Dictionary<BuidingType, List<BuildingConfig>> _dictionaryBuildingsVariousTypes = new();

    public void Initialize()
    {
        _configRedactor.BuildingRedactedForPlacmentManager += OnBuildingRedacted;

        var values = Enum.GetValues(typeof(BuidingType)).Cast<BuidingType>();
        

        foreach (var type in values)
        {
            List<BuildingConfig> buildings = new();
            _dictionaryBuildingsVariousTypes.Add(type, buildings);
        }
    }

    public void Dispose()
    {
        _configRedactor.BuildingRedactedForPlacmentManager -= OnBuildingRedacted;
    }

    public void AddBuilding(BuildingConfig buildingConfig)
    {
        _dictionaryBuildingsVariousTypes[buildingConfig.GetBuidingType()].Add(buildingConfig);

        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            ResidentialBuildingConfig residentialBuildingConfig = (ResidentialBuildingConfig)buildingConfig;
            _residentialBuildingsList.Add(residentialBuildingConfig);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            CommericalBuildingConfig commericalBuildingConfig = (CommericalBuildingConfig)buildingConfig;
            _commericalBuildingsList.Add(commericalBuildingConfig);
            OnCommericalBuildingAdded?.Invoke();
        }

    }

    public void RemoveBuilding(BuildingConfig buildingConfig)
    {

        var buildingList = _dictionaryBuildingsVariousTypes[buildingConfig.GetBuidingType()];
        buildingList.Remove(buildingConfig);

        if (buildingConfig.GetVertexType() == VertexType.Residential_Building)
        {
            ResidentialBuildingConfig residentialBuildingConfig = (ResidentialBuildingConfig)buildingConfig;
            _residentialBuildingsList.Remove(residentialBuildingConfig);
        }
        else if (buildingConfig.GetVertexType() == VertexType.Commercial_Building)
        {
            CommericalBuildingConfig commericalBuildingConfig = (CommericalBuildingConfig)buildingConfig;
            _commericalBuildingsList.Remove(commericalBuildingConfig);
        }

    }

    public bool CheckingWhetherThereBuildingsOfType(BuidingType buildingType)
    {
        var buildingList = _dictionaryBuildingsVariousTypes[buildingType];

        if (buildingList.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public CommericalBuildingConfig GetBuildingNearestBuildingToHouseCertainBuildingsType(BuidingType buildingType, Vector3 residentialPosition)
    {
        float distance = float.MaxValue;
        CommericalBuildingConfig nearestBuilding = null;
        var BuildingList = _dictionaryBuildingsVariousTypes[buildingType];

        foreach (var building in BuildingList)
        {   
            if (Vector3.Distance(residentialPosition, building.GetPosition()) < distance)
            {
                nearestBuilding = (CommericalBuildingConfig)building;
                distance = Vector3.Distance(residentialPosition, building.GetPosition());
            }
        }

        return nearestBuilding;
    }

    public List<CommericalBuildingConfig> GetCommericalBuildingConfigList()
    {
        return _commericalBuildingsList;
    }

    public bool TryGetRandomBuildingPositionWithFreeWorkplace(Education education)
    {
        foreach (var buidling in _commericalBuildingsList)
        {
            if (buidling.GetCurrentNumberEmployeesOfCertainEducation(education) < buidling.GetMaximumNumberEmployeesOfCertainEducation(education))
            {
                return true;
            }
        }

        return false;
    }

    public CommericalBuildingConfig GetBuildingWithFreeWorkplace(Education education)
    {

        foreach (var buidling in _commericalBuildingsList)
        {
            if (buidling.GetCurrentNumberEmployeesOfCertainEducation(education) < buidling.GetMaximumNumberEmployeesOfCertainEducation(education))
            {
                OnEmpolyeeAdded?.Invoke(buidling, education);
                return buidling;
            }
        }

        throw new Exception("there are no vacant jobs");
    }

    public BuildingConfig GetBuildingInCertainPosition(BuidingType buidingType, Vector3 position)
    {

        var buildingList = _dictionaryBuildingsVariousTypes[buidingType];

        foreach (var building in buildingList)
        {
            if (building.GetPosition() == position)
            {
                return building;
            }
        }

        throw new Exception("there is no building in this position");
    }

    private void OnBuildingRedacted(CommericalBuildingConfig buildingConfig)
    {
        foreach (var keyValuePair in _dictionaryBuildingsVariousTypes)
        {
            var buildingList = keyValuePair.Value;
            if(buildingList.Contains(buildingConfig) == true)
            {
                buildingList.Remove(buildingConfig);
            }
        }
        _dictionaryBuildingsVariousTypes[buildingConfig.GetBuidingType()].Add(buildingConfig);
    }

    public int GetNumberBuildingsOfCertainType(BuidingType buildingsType)
    {
        return _dictionaryBuildingsVariousTypes[buildingsType].Count;
    }

    public int GetTotalNumberOfVacancies()
    {
        int numberOfVacancies = 0;

        foreach (var buildingConfig in _commericalBuildingsList)
        {
            numberOfVacancies += buildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION) - buildingConfig.GetCurrentNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION);
            numberOfVacancies += buildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION) - buildingConfig.GetCurrentNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION);
            numberOfVacancies += buildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION) - buildingConfig.GetCurrentNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION);
        }

        return numberOfVacancies;
    }
}
