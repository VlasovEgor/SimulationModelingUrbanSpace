using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CitizensManager : IInitializable, IDisposable
{
    public event Action<Citizen> CitizenRemoved;
    public event Action CitizenAdded;

    public CitizenFactory CitizenFactory
    {
        get => default;
        set
        {
        }
    }

    public Citizen Citizen
    {
        get => default;
        set
        {
        }
    }

    [Inject] private CommericalBuildingConfigRedactor _commericalBuildingConfigRedactor;
    [Inject] private ResidentialBuildingConfigRedactor _residentialBuildingConfigRedactor;
    [Inject] private PlacementManager _placementManager;
    [Inject] private CitizenFactory _citizenFactory;

    private List<Citizen> _citizens = new();

    public void Initialize()
    {
        _residentialBuildingConfigRedactor.NumberResidentsChanged += ChangingParametersHouse;
        _commericalBuildingConfigRedactor.BuildingRedacted += GettingActivitePlace;
    }

    public void Dispose()
    {
        _residentialBuildingConfigRedactor.NumberResidentsChanged -= ChangingParametersHouse;
        _commericalBuildingConfigRedactor.BuildingRedacted -= GettingActivitePlace;
    }

    public List<Citizen> GetCitizensList()
    {
        return _citizens;
    }

    public int GetCitizensCount()
    {
        return _citizens.Count;
    }

    public int GetOverallLevelHappiness()
    {
        int levelHappiness = 0;

        foreach (var citizen in _citizens)
        {
            levelHappiness += citizen.GetHappinessLevel();
        }

        return levelHappiness / _citizens.Count;
    }

    public void ChangingParametersHouse(Vector3 position, int HEvalue, int SEvalue, int WEvalue)
    {
        var totalNumber = HEvalue + SEvalue + WEvalue;

        List<Citizen> residentsHouse = new();
        int residentsHouseWithHE = 0;
        int residentsHouseWithSE = 0;
        int residentsHouseWE = 0;

        foreach (var citizen in _citizens)
        {
            if (citizen.GetPlaceActivity(BuidingType.RESIDENTIAL).GetPosition() == position)
            {
                residentsHouse.Add(citizen);

                if (citizen.GetEducation() == Education.HIGHER_EDUCATION)
                {
                    residentsHouseWithHE++;
                }
                else if (citizen.GetEducation() == Education.SECOND_EDUCATION)
                {
                    residentsHouseWithSE++;
                }
                else
                {
                    residentsHouseWE++;
                }
            }
        }

        if (residentsHouse.Count > totalNumber)
        {
            while (residentsHouse.Count > totalNumber)
            {
                var indexRandom = UnityEngine.Random.Range(0, residentsHouse.Count - 1);

                DeleteCitizen(residentsHouse[indexRandom]);
                residentsHouse.RemoveAt(indexRandom);
            }
        }
        else
        {
            while (residentsHouseWithHE < HEvalue)
            {
                var citizen = _citizenFactory.CreateCitizen(Education.HIGHER_EDUCATION,
                    _placementManager.GetBuildingInCertainPosition(BuidingType.RESIDENTIAL, position));
                residentsHouseWithHE++;
                _citizens.Add(citizen);
                CitizenAdded?.Invoke();
            }

            while (residentsHouseWithSE < SEvalue)
            {
                var citizen = _citizenFactory.CreateCitizen(Education.SECOND_EDUCATION,
                    _placementManager.GetBuildingInCertainPosition(BuidingType.RESIDENTIAL, position));
                residentsHouseWithSE++;
                _citizens.Add(citizen);
                CitizenAdded?.Invoke();
            }
            while (residentsHouseWE < WEvalue)
            {
                var citizen = _citizenFactory.CreateCitizen(Education.WITOUT_EDUCATION,
                    _placementManager.GetBuildingInCertainPosition(BuidingType.RESIDENTIAL, position));
                residentsHouseWE++;
                _citizens.Add(citizen);
                CitizenAdded?.Invoke();
            }
        }

    }

    private void DeleteCitizen(Citizen citizen)
    {
        _citizens.Remove(citizen);
        CitizenRemoved?.Invoke(citizen);
    }

    private void GettingActivitePlace(BuidingType buidingType)
    {
        if (buidingType == BuidingType.NONE || buidingType == BuidingType.RESIDENTIAL)
        {
            return;
        }

        if (buidingType == BuidingType.WORK)
        {
            foreach (var citizen in _citizens)
            {
                if (citizen.TryGetPlaceActivity(BuidingType.WORK) == false)
                {
                    if (_placementManager.TryGetRandomBuildingPositionWithFreeWorkplace(citizen.GetEducation()) == true)
                    {
                        citizen.SetPlaceActivity(BuidingType.WORK, _placementManager.GetBuildingWithFreeWorkplace(citizen.GetEducation()));
                    }
                }
            }

            return;
        }

        foreach (var citizen in _citizens)
        {
            if (citizen.TryGetPlaceActivity(BuidingType.WORK) == false)
            {
                if (_placementManager.TryGetRandomBuildingPositionWithFreeWorkplace(citizen.GetEducation()) == true)
                {
                    citizen.SetPlaceActivity(BuidingType.WORK, _placementManager.GetBuildingWithFreeWorkplace(citizen.GetEducation()));
                }
            }

            citizen.SetPlaceActivity(buidingType, _placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(buidingType, citizen.GetPlaceActivity(BuidingType.RESIDENTIAL).GetPosition()));
        }
    }

    public int GetNumberUnemployed()
    {
        int number = 0;
        foreach (var citizen in _citizens)
        {
            if (citizen.TryGetPlaceActivity(BuidingType.WORK) == false)
            {
                number++;
            }
        }

        return number;
    }

    public Citizen GetRandomCitizen()
    {
        int index = UnityEngine.Random.Range(0, _citizens.Count);
        return _citizens[index];
    }
}