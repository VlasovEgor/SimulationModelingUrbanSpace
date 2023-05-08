using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Citizen
{
    private Health _health;
    private Education _education;
    private PersonalityType _personalityType;

    private Dictionary<Needs, int> _citizenNeeds;
    private Dictionary<BuidingType, BuildingConfig> _placesActivity;

    private bool _isWorth;
    private bool _isActive;

    private DateTime _releaseTime;
    private DateTime _currentTime;
    private CommericalBuildingConfig _currnetCommericalBuildng;
    private Agent _currentAgent;

    private HourMinute _activeTimeStart;
    private HourMinute _activeTimeEnd;

    private List<BuildingConfig> _planForDay;

    public int _buildingCounter;
    private AgentPath _agentPath;

    public int BuildingCounter
    {
        get { return _buildingCounter; }
        set { _buildingCounter = value; }
    }

    public Citizen(Education education, AgentPath agentPath) : this()
    {
        _agentPath = agentPath;

        _isWorth = true;
        _isActive = false;

        _education = education;

        var values = Enum.GetValues(typeof(Needs)).Cast<Needs>();

        _citizenNeeds = new Dictionary<Needs, int>();
        _placesActivity = new Dictionary<BuidingType, BuildingConfig>();

        _activeTimeStart.Hour = 8;
        _activeTimeStart.Minute = 0;

        _activeTimeEnd.Hour = 23;
        _activeTimeEnd.Minute = 0;

        _buildingCounter = 0;
        _planForDay = new List<BuildingConfig>();

        foreach (var type in values)
        {
            _citizenNeeds.Add(type, 100);
        }

        SetHealth();
        SetPersonalityTypeh();

    }

    private void SetHealth()
    {
        var values = Enum.GetValues(typeof(Health)).Cast<Health>();
        int indexRandom = UnityEngine.Random.Range(0, values.Count() - 1);
        int i = 0;

        foreach (var type in values)
        {
            if (indexRandom == i)
            {
                _health = type;
            }
            i++;
        }
    }

    private void SetPersonalityTypeh()
    {
        var values = Enum.GetValues(typeof(PersonalityType)).Cast<PersonalityType>();
        int indexRandom = UnityEngine.Random.Range(0, values.Count() - 1);
        int i = 0;

        foreach (var type in values)
        {
            if (indexRandom == i)
            {
                _personalityType = type;
            }
            i++;
        }
    }

    public Health GetHealth()
    {
        return _health;
    }

    public Education GetEducation()
    {
        return _education;
    }

    public PersonalityType GetPersonalityType()
    {
        return _personalityType;
    }

    public void SetEducation(Education education)
    {
        _education = education;
    }

    public void SetPlaceActivity(BuidingType placesActivity, BuildingConfig buildingConfig)
    {
        if (_placesActivity.TryGetValue(placesActivity, out _) == false)
        {
            _placesActivity.Add(placesActivity, buildingConfig);
        }
        else
        {
            _placesActivity[placesActivity] = buildingConfig;
        }
    }

    public bool TryGetPlaceActivity(BuidingType placesActivity)
    {
        return _placesActivity.TryGetValue(placesActivity, out var place);
    }

    public BuildingConfig GetPlaceActivity(BuidingType placesActivity)
    {
        return _placesActivity[placesActivity];
    }

    public void ReduceNeed(Needs need, int value)
    {
        _citizenNeeds[need] += value;

        if (_citizenNeeds[need] > 100)
        {
            _citizenNeeds[need] = 100;
        }
    }

    public void IncreasingNeeds()
    {
        if (_personalityType == PersonalityType.GLUTTON)
        {
            IncreaseNeed(Needs.FOOD, 3);
        }
        else
        {
            IncreaseNeed(Needs.FOOD, 2);
        }

        if (_personalityType == PersonalityType.SPORTSMAN)
        {
            IncreaseNeed(Needs.SPORT, 3);
        }
        else if (_personalityType == PersonalityType.LAZY_PERSON)
        {
            IncreaseNeed(Needs.SPORT, 1);
        }
        else
        {
            IncreaseNeed(Needs.SPORT, 2);
        }

        if (_personalityType == PersonalityType.PARTY_BOY)
        {
            IncreaseNeed(Needs.REST, 3);
        }
        else if (_personalityType == PersonalityType.HOMEBODY)
        {
            IncreaseNeed(Needs.REST, 1);
        }
        else
        {
            IncreaseNeed(Needs.REST, 2);
        }

        if (_personalityType == PersonalityType.WORKAHOLIC)
        {
            IncreaseNeed(Needs.HEALTH, 3);
        }
        else
        {
            IncreaseNeed(Needs.HEALTH, 2);
        }

    }

    public void IncreaseNeed(Needs need, int value)
    {
        _citizenNeeds[need] -= value;

        if (_citizenNeeds[need] < 0)
        {
            _citizenNeeds[need] = 0;
        }
    }

    public int GetHappinessLevel()
    {
        var happiness = 0;

        foreach (var keyValuePair in _citizenNeeds)
        {
            happiness += keyValuePair.Value;
        }

        return happiness / _citizenNeeds.Count;
    }

    public void SelectNewNearestActivityLocations(PlacementManager placementManager)
    {
        var values = Enum.GetValues(typeof(BuidingType)).Cast<BuidingType>();

        foreach (var buidingType in values)
        {
            if (buidingType == BuidingType.NONE || buidingType == BuidingType.RESIDENTIAL || buidingType == BuidingType.WORK)
            {
                continue;
            }
            if (placementManager.CheckingWhetherThereBuildingsOfType(buidingType) == true)
            {
                SetPlaceActivity(buidingType, placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(buidingType, _placesActivity[BuidingType.RESIDENTIAL].GetPosition()));
            }
        }
    }

    public void SetWorth(bool value)
    {
        _isWorth = value;
        Debug.Log("Worth " + _isWorth);

        //_currentAgent.ArrivedAtDestination -= SetWorth;

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
        if (_releaseTime > _currentTime)
        {
            return true;
        }
        else
        {
            // AccrualNeed();
            return false;
        }
    }

    private void AccrualNeed()
    {
        if (_currnetCommericalBuildng.GetBuidingType() == BuidingType.FOOD)
        {
            IncreaseNeed(Needs.FOOD, _currnetCommericalBuildng.GetAmountOfSatisfactionOfNeed());
        }
        else if (_currnetCommericalBuildng.GetBuidingType() == BuidingType.RELAX)
        {
            IncreaseNeed(Needs.REST, _currnetCommericalBuildng.GetAmountOfSatisfactionOfNeed());
        }
        else if (_currnetCommericalBuildng.GetBuidingType() == BuidingType.SPORT)
        {
            IncreaseNeed(Needs.SPORT, _currnetCommericalBuildng.GetAmountOfSatisfactionOfNeed());
        }
        else if (_currnetCommericalBuildng.GetBuidingType() == BuidingType.HEALTH)
        {
            IncreaseNeed(Needs.HEALTH, _currnetCommericalBuildng.GetAmountOfSatisfactionOfNeed());
        }
    }

    public void SetReleaseTime()
    {
        _releaseTime = _currentTime;

        _releaseTime.AddHours(_currnetCommericalBuildng.GetAverageTimeInBuilding() / 60);
        _releaseTime.AddMinutes(_currnetCommericalBuildng.GetAverageTimeInBuilding() % 60);
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

    public HourMinute GetActiveTimeStart()
    {
        return _activeTimeStart;
    }

    public HourMinute GetActiveTimeEnd()
    {
        return _activeTimeEnd;
    }

    public Dictionary<BuidingType, BuildingConfig> GetDictionaryPlacesActivity()
    {
        return _placesActivity;
    }

    public Dictionary<Needs, int> GetDictionartNeeds()
    {
        return _citizenNeeds;
    }

    public void SetPlanForDay(List<BuildingConfig> planForDay)
    {
        _buildingCounter = 0;
        _planForDay.Clear();
        _planForDay.AddRange(planForDay);
    }


    public bool CheckingForTransitionToNextBuilding()
    {
       // if(_isActive == true)
       // {
       //     Debug.Log("_isActive == true");
       //
       // }
       // else
       // {
       //     Debug.Log("_isActive == false");
       // }

        if (_isWorth == true)
        {
            Debug.Log(" _isWorth == true");
        }
        else
        {
            Debug.Log(" _isWorth == false");
        }

       // if(CheckingIfBusy() == false)
       // {
       //     Debug.Log("CheckingIfBusy() == false");
       // }
       // else
       // {
       //     Debug.Log("CheckingIfBusy() == true");
       // }
       //
       // if(_buildingCounter < _planForDay.Count == true)
       // {
       //     Debug.Log("_buildingCounter < _planForDay.Count == true");
       // }
       // else
       // {
       //     Debug.Log("_buildingCounter < _planForDay.Count == false");
       // }

        if (_isActive == true && _isWorth == true && CheckingIfBusy() == false && _buildingCounter < _planForDay.Count == true)
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
        Debug.Log(_buildingCounter);
        _buildingCounter++;
        Debug.Log(_buildingCounter);
        _agentPath.SendHumanToBuilding(ref this, _planForDay[_buildingCounter - 1].GetBuidingType(), _planForDay[_buildingCounter].GetBuidingType());

        if (_planForDay[_buildingCounter].GetBuidingType() != BuidingType.NONE && _planForDay[_buildingCounter].GetBuidingType() != BuidingType.RESIDENTIAL)
        {
            _currnetCommericalBuildng = (CommericalBuildingConfig)_planForDay[_buildingCounter];
        }
        else
        {
            _currnetCommericalBuildng = null;
        }

    }
}
