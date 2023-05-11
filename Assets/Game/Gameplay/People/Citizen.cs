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

    private HourMinute _activeTimeStart;
    private HourMinute _activeTimeEnd;


    public Citizen(Education education) : this()
    {

        _education = education;

        var values = Enum.GetValues(typeof(Needs)).Cast<Needs>();

        _citizenNeeds = new Dictionary<Needs, int>();
        _placesActivity = new Dictionary<BuidingType, BuildingConfig>();

        _activeTimeStart.Hour = 8;
        _activeTimeStart.Minute = 0;

        _activeTimeEnd.Hour = 23;
        _activeTimeEnd.Minute = 0;

        foreach (var type in values)
        {
            _citizenNeeds.Add(type, 100);
        }

        SetHealth();
        SetPersonalityType();
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

    private void SetPersonalityType()
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

    public Dictionary<Needs, int> GetDictionaryNeeds()
    {
        return _citizenNeeds;
    }


   public void AccrualNeed(List<BuildingConfig> buildingConfigs)
   {
        foreach (var buildingConfig in buildingConfigs)
        {
            if(buildingConfig.GetBuidingType() != BuidingType.NONE && buildingConfig.GetBuidingType() != BuidingType.RESIDENTIAL)
            {   
                var currentBuildingConfig = (CommericalBuildingConfig)buildingConfig;
                if (currentBuildingConfig.GetBuidingType() == BuidingType.FOOD)
                {
                    IncreaseNeed(Needs.FOOD, currentBuildingConfig.GetAmountOfSatisfactionOfNeed());
                }
                else if (currentBuildingConfig.GetBuidingType() == BuidingType.RELAX)
                {
                    IncreaseNeed(Needs.REST, currentBuildingConfig.GetAmountOfSatisfactionOfNeed());
                }
                else if (currentBuildingConfig.GetBuidingType() == BuidingType.SPORT)
                {
                    IncreaseNeed(Needs.SPORT, currentBuildingConfig.GetAmountOfSatisfactionOfNeed());
                }
                else if (currentBuildingConfig.GetBuidingType() == BuidingType.HEALTH)
                {
                    IncreaseNeed(Needs.HEALTH, currentBuildingConfig.GetAmountOfSatisfactionOfNeed());
                }
            }
        }
       
   }


}
