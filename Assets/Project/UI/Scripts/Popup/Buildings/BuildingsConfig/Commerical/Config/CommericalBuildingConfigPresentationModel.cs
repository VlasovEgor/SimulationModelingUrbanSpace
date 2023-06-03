using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommericalBuildingConfigPresentationModel : ICommericalBuildingConfigPresentationModel
{   
    private CommericalBuildingConfig _buildingConfig;
    private CommericalBuildingConfigRedactor _configRedactor;
    private List<string> _buildingType;

    public CommericalBuildingConfigPresentationModel(CommericalBuildingConfig buildingConfig, CommericalBuildingConfigRedactor configRedactor)
    {
        _buildingConfig = buildingConfig;
        _configRedactor = configRedactor;
    }

    void ICommericalBuildingConfigPresentationModel.OnAcceptButtonClicked(CommericalBuildingConfig newBuildingConfig)
    {
        _configRedactor.ChangeValuesInConfig(_buildingConfig, newBuildingConfig);
    }

    public string GetName()
    {
        return _buildingConfig.GetName();
    }

    public List<string> ListCommericalBuildingType()
    {
        _buildingType =  Enum.GetValues(typeof(BuidingType)).
            Cast<BuidingType>().
            Select(v => v.ToString()).
            ToList();

        var typeList = new List<string>();
        for(int i=2; i< _buildingType.Count;i++)
        {
            typeList.Add(_buildingType[i]);
        }

        return typeList;
    }

    public int GetIndexBuildingType()
    {   
        for (int i = 2; i < _buildingType.Count; i++)
        {
            if (_buildingType[i] == _buildingConfig.GetBuidingType().ToString())
            {
                return i;
            }    
        }

        throw new Exception("there are no buildings of this type");
    }

    public string GetStringBuildingType()
    { 
        var buildingsType = ListCommericalBuildingType();

        for (int i = 0; i < buildingsType.Count; i++)
        {
            if (buildingsType[i] == _buildingConfig.GetBuidingType().ToString())
            {
                return buildingsType[i];
            }
        }

        throw new Exception("there are no buildings of this type");
    }

    public string GetMaximumNumberEmployeesWithHigherEducation()
    {
        return _buildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION).ToString();
    }

    public string GetMaximumNumberEmployeesWithSecondaryEducation()
    {
        return _buildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION).ToString();
    }

    public string GetMaximumNumberEmployeesWithoutEducation()
    {
        return _buildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION).ToString();
    }

    public string GetMaximumNumberVisitors()
    {
        return _buildingConfig.GetMaximumNumberVisitors().ToString();
    }

    public string GetAmountOfSatisfactionOfNeed()
    {
        return _buildingConfig.GetAmountOfSatisfactionOfNeed().ToString();
    }

    public string GetHourStartWork()
    {
        return _buildingConfig.GetStartWork().Hour.ToString();
    }

    public string GetMinuteStartWork()
    {
        return _buildingConfig.GetStartWork().Minute.ToString();
    }

    public string GetHourFinishWork()
    {
        return _buildingConfig.GetFinishWork().Hour.ToString();
    }

    public string GetMinuteFinishWork()
    {
        return _buildingConfig.GetFinishWork().Minute.ToString();
    }

    public string GetAverageTimeInBuilding()
    {
        return _buildingConfig.GetAverageTimeInBuilding().ToString();   
    }

    public string GetCurrentNumberEmployeesWithHigherEducation()
    {
        return _buildingConfig.GetCurrentNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION).ToString();
    }

    public string GetCurrentNumberEmployeesWithSecondaryEducation()
    {
        return _buildingConfig.GetCurrentNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION).ToString();
    }

    public string GetCurrentNumberEmployeesWithoutEducation()
    {
        return _buildingConfig.GetCurrentNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION).ToString();
    }
}