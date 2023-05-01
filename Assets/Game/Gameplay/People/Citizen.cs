using System;
using System.Collections.Generic;
using System.Linq;

public struct Citizen
{
    private Health _health;
    private Education _education;
    private PersonalityType _personalityType;

    private Dictionary<Needs, int> _citizenNeeds;
    private Dictionary<BuidingType, BuildingConfig> _placesActivity;

    private bool _isWorth;

    private DateTime _releaseTime;
    private DateTime _currentTime;
    private CommericalBuildingConfig _currnetBuildng;
    private Agent _currentAgent;

    private HourMinute _activeTimeStart;
    private HourMinute _activeTimeEnd;

    private List<CommericalBuildingConfig> _planForDay;

    private int _buildingCounter;
    private AgentPath _agentPath;

    public Citizen(Education education, AgentPath agentPath) : this()
    {
        _agentPath = agentPath;

        _isWorth = false;

        _education = education;

        var values = Enum.GetValues(typeof(Needs)).Cast<Needs>();

        _citizenNeeds = new Dictionary<Needs, int>();
        _placesActivity = new Dictionary<BuidingType, BuildingConfig>();

        _activeTimeStart.Hour = 8;
        _activeTimeStart.Minute = 0;

        _activeTimeEnd.Hour = 23;
        _activeTimeEnd.Minute = 0;

        _buildingCounter = 0;

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


        foreach (var need in _citizenNeeds)
        {
            if (need.Key == Needs.FOOD)
            {
                if (_personalityType == PersonalityType.GLUTTON)
                {
                    IncreaseNeed(need.Key, 3);
                }
                else
                {
                    IncreaseNeed(need.Key, 2);
                }
            }
            else if (need.Key == Needs.SPORT)
            {
                if (_personalityType == PersonalityType.SPORTSMAN)
                {
                    IncreaseNeed(need.Key, 3);
                }
                else if (_personalityType == PersonalityType.LAZY_PERSON)
                {
                    IncreaseNeed(need.Key, 1);
                }
                else
                {
                    IncreaseNeed(need.Key, 2);
                }
            }
            else if (need.Key == Needs.REST)
            {
                if (_personalityType == PersonalityType.PARTY_BOY)
                {
                    IncreaseNeed(need.Key, 3);
                }
                else if (_personalityType == PersonalityType.HOMEBODY)
                {
                    IncreaseNeed(need.Key, 1);
                }
                else
                {
                    IncreaseNeed(need.Key, 2);
                }
            }
            else if (need.Key == Needs.HEALTH)
            {
                if (_personalityType == PersonalityType.WORKAHOLIC)
                {
                    IncreaseNeed(need.Key, 3);
                }
                else
                {
                    IncreaseNeed(need.Key, 2);
                }
            }


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

    public void SelectNewNearestActivityLocations(PlacementManager placementManager)
    {
        var values = Enum.GetValues(typeof(BuidingType)).Cast<BuidingType>();

        foreach (var buidingType in values)
        {
            if (placementManager.CheckingWhetherThereBuildingsOfType(buidingType) == true)
            {
                //SetPlaceActivity(buidingType, placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(buidingType, this));
            }
        }
    }

    public void SetWorth(bool value)
    {
        _isWorth = value;

        if (_isWorth == true)
        {
            _currentAgent.ArrivedAtDestination -= SetWorth;
            _currentAgent = null;

            SetReleaseTime();
        }
    }

    public void SetAgent(Agent agent)
    {
        _currentAgent = agent;
        _currentAgent.ArrivedAtDestination += SetWorth;
    }

    public bool CheckingWhetherItIsWorthIt()
    {
        return _isWorth;
    }

    public bool CheckingIfBusy()
    {
        if(_releaseTime > _currentTime) 
        {
            return true;
        }
        else
        {
            AccrualNeed();
            SetNewBuilding();

            return false;
        }
    }

    private void AccrualNeed()
    {
        if (_currnetBuildng.GetBuidingType() == BuidingType.FOOD)
        {
            IncreaseNeed(Needs.FOOD, _currnetBuildng.GetAmountOfSatisfactionOfNeed());
        }
        else if (_currnetBuildng.GetBuidingType() == BuidingType.RELAX)
        {
            IncreaseNeed(Needs.REST, _currnetBuildng.GetAmountOfSatisfactionOfNeed());
        }
        else if (_currnetBuildng.GetBuidingType() == BuidingType.SPORT)
        {
            IncreaseNeed(Needs.SPORT, _currnetBuildng.GetAmountOfSatisfactionOfNeed());
        }
        else if (_currnetBuildng.GetBuidingType() == BuidingType.HEALTH)
        {
            IncreaseNeed(Needs.HEALTH, _currnetBuildng.GetAmountOfSatisfactionOfNeed());
        }
    }

    private void SetNewBuilding()
    {   
        if(_buildingCounter == 0)
        {
            _agentPath.SendHumanToBuilding(this, BuidingType.RESIDENTIAL, _planForDay[0].GetBuidingType());
            _buildingCounter++;
        }
        else if (_buildingCounter < _planForDay.Count)
        {
            _currnetBuildng = _planForDay[_buildingCounter];
            _buildingCounter++;
        }
        else
        {
            _agentPath.SendHumanToBuilding(this, _planForDay.Last().GetBuidingType(), BuidingType.RESIDENTIAL);
        }

    }

    public void SetReleaseTime()
    {
        _releaseTime = _currentTime;

        if(_currnetBuildng.GetBuidingType() != BuidingType.RESIDENTIAL || _currnetBuildng.GetBuidingType() != BuidingType.NONE) 
        {
            _releaseTime.AddHours(_currnetBuildng.GetAverageTimeInBuilding() / 60);
            _releaseTime.AddMinutes(_currnetBuildng.GetAverageTimeInBuilding() % 60);
        }
        else
        {
            _releaseTime = _currentTime;
        }
       
    }

    public void SetCurrnetTime(DateTime dateTime)
    {
        _currentTime = dateTime;
    }

    public void SetCurrentBuidling(BuildingConfig BuildingConfig)
    {
        _currnetBuildng = (CommericalBuildingConfig)BuildingConfig;
    }

    public HourMinute GetActiveTimeStart()
    {
        return _activeTimeStart;
    }

    public HourMinute GetActiveTimeEnd()
    {
        return _activeTimeEnd;
    }

    public Dictionary<BuidingType, BuildingConfig> GetDictionartPlacesActivity()
    {
        return _placesActivity;
    }

    public Dictionary<Needs, int> GetDictionartNeeds()
    {
        return _citizenNeeds ;
    }

    public void SetPlanForDay(List<CommericalBuildingConfig> planForDay)
    {
        _buildingCounter = 0;
        _planForDay.Clear();
        _planForDay = planForDay;
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
}
