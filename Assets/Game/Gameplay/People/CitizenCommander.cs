using System;
using System.Collections.Generic;

public class CitizenCommander
{
    private Dictionary<Needs, int> _citizenNeeds;
    private Dictionary<BuidingType, BuildingConfig> _placesActivity;

    private bool _isWorth = true;
    private bool _isActive = false;

    private HourMinute _releaseTime;
    private DateTime _currentTime;
    private CommericalBuildingConfig _currnetCommericalBuildng;
    private Agent _currentAgent;

    private HourMinute _activeTimeStart;
    private HourMinute _activeTimeEnd;

    private List<BuildingConfig> _planForDay = new();

    public int _buildingCounter =0;
    private AgentPath _agentPath;

    public CitizenCommander(AgentPath agentPath)
    {
        _agentPath = agentPath;
    }

    public void GetData(Citizen citizen)
    {
        _citizenNeeds = citizen.GetDictionaryNeeds();
        _placesActivity = citizen.GetDictionaryPlacesActivity();

        _activeTimeStart = citizen.GetActiveTimeStart();
        _activeTimeEnd= citizen.GetActiveTimeEnd();
    }

    public HourMinute GetActiveTimeStart()
    {
        return _activeTimeStart;
    }

    public HourMinute GetActiveTimeEnd()
    {
        return _activeTimeEnd;
    }

    public bool TryGetPlaceActivity(BuidingType placesActivity)
    {
        return _placesActivity.TryGetValue(placesActivity, out var place);
    }

    public BuildingConfig GetPlaceActivity(BuidingType placesActivity)
    {
        return _placesActivity[placesActivity];
    }

    public Dictionary<BuidingType, BuildingConfig> GetDictionaryPlacesActivity()
    {
        return _placesActivity;
    }

    public Dictionary<Needs, int> GetDictionaryNeeds()
    {
        return _citizenNeeds;
    }

    public void ChangeActiveTimeStart(int value)
    {
        _activeTimeStart.Hour += value / 60;
        _activeTimeStart.Minute += value % 60;
    }

    public void SetWorth(bool value)
    {
        _isWorth = value;

        if (_isWorth == true)
        {
            if (_currnetCommericalBuildng != null)
            {
                SetReleaseTime();
            }
        }

    }

    public void SetAgent(Agent agent)
    {
        _currentAgent = agent;
        _currentAgent.ArrivedAtDestination += SetWorth;
    }

    public bool CheckingIfBusy()
    {   
        var currentTimeInMinute = _currentTime.Hour * 60 + _currentTime.Minute;
        var releaseTimeInMinute = _releaseTime.Hour * 60 + _releaseTime.Minute;

        if (releaseTimeInMinute > currentTimeInMinute)
        {
            return true;
        }
        else
        {
            _releaseTime.Hour = 0;
            _releaseTime.Minute = 0;
            
            return false;
        }
    }

    public void SetReleaseTime()
    {
        _releaseTime.Hour = _currentTime.Hour;
        _releaseTime.Minute = _currentTime.Minute;

        if(_currnetCommericalBuildng == _placesActivity[BuidingType.WORK])
        {
            _releaseTime.Hour += _currnetCommericalBuildng.GetwWorkingHoursOfEmployeesInMinute() / 60;
            _releaseTime.Minute += _currnetCommericalBuildng.GetwWorkingHoursOfEmployeesInMinute() % 60;
        }
        else
        {
            _releaseTime.Hour += _currnetCommericalBuildng.GetAverageTimeInBuilding() / 60;
            _releaseTime.Minute += _currnetCommericalBuildng.GetAverageTimeInBuilding() % 60;
        }
    }

    public void SetCurrnetTime(DateTime dateTime)
    {
        _currentTime = dateTime;

        if (_currentTime.Hour >= _activeTimeStart.Hour
           && _currentTime.Hour < _activeTimeEnd.Hour)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
    }

    public void SetPlanForDay(List<BuildingConfig> planForDay)
    {
        _buildingCounter = 0;
        _planForDay.Clear();
        _planForDay.AddRange(planForDay);

    }

    public bool CheckingForTransitionToNextBuilding()
    {
        if (_isActive == true && 
            _isWorth == true && CheckingIfBusy() == false && 
            _buildingCounter < _planForDay.Count - 1 == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveToNextBuilding()
    {
        _buildingCounter++;

        _agentPath.SendHumanToBuilding(this,  _planForDay[_buildingCounter - 1].GetBuidingType(), _planForDay[_buildingCounter].GetBuidingType());

        if (_planForDay[_buildingCounter].GetBuidingType() != BuidingType.NONE && 
            _planForDay[_buildingCounter].GetBuidingType() != BuidingType.RESIDENTIAL)
        {
            _currnetCommericalBuildng = (CommericalBuildingConfig)_planForDay[_buildingCounter];
        }
        else
        {
            _currnetCommericalBuildng = null;
        }

    }
}
