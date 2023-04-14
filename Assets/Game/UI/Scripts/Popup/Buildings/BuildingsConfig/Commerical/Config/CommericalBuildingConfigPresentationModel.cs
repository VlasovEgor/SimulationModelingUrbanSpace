using System;
using System.Collections.Generic;
using System.Linq;

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
        _buildingType =  Enum.GetValues(typeof(CommericalBuidingType)).
            Cast<CommericalBuidingType>().
            Select(v => v.ToString()).
            ToList();

        return _buildingType;
    }

    public int GetIndexBuildingType()
    {
        for (int i = 0; i < _buildingType.Count; i++)
        {
            if (_buildingType[i] == _buildingConfig.GetCommericalBuidingType().ToString())
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
            if (buildingsType[i] == _buildingConfig.GetCommericalBuidingType().ToString())
            {
                return buildingsType[i];
            }
        }

        throw new Exception("there are no buildings of this type");
    }

    public string GetNumberEmployeesWithHigherEducation()
    {
        return _buildingConfig.GetMaximumNumberEmployeesWithHigherEducation().ToString();
    }

    public string GetNumberEmployeesWithSecondaryEducation()
    {
        return _buildingConfig.GetMaximumNumberEmployeesWithSecondaryEducation().ToString();
    }

    public string GetNumberEmployeesWithoutEducation()
    {
        return _buildingConfig.GetMaximumNumberEmployeesWithoutEducation().ToString();
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
}