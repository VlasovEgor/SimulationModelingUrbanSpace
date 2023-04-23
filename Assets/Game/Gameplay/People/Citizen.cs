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

    public Citizen(Education education) : this()
    {
        _education = education;

        var values = Enum.GetValues(typeof(Needs)).Cast<Needs>();

        _citizenNeeds = new Dictionary<Needs, int>();
        _placesActivity = new Dictionary<BuidingType, BuildingConfig>();

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
        int indexRandom = UnityEngine.Random.Range(0, values.Count()-1);
        int i=0;

        foreach (var type in values)
        {
            if(indexRandom == i)
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
  
    public void ReduceNeed(Needs need,int value)
    {
        _citizenNeeds[need] += value;

        if(_citizenNeeds[need] > 100)
        {
            _citizenNeeds[need] = 100;
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
}
