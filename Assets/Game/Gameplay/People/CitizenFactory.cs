using UnityEngine;
using Zenject;

public class CitizenFactory
{
    [Inject] private PlacementManager _placementManager;

    public Citizen CreateCitizen(Education education, BuildingConfig homeConfig)
    {
        var citizen = new Citizen(education);

        citizen.SetEducation(education);

        citizen.SetPlaceActivity(BuidingType.RESIDENTIAL, homeConfig);

        if(_placementManager.TryGetRandomBuildingPositionWithFreeWorkplace(education) == true)
        {   
            citizen.SetPlaceActivity(BuidingType.WORK, _placementManager.GetBuildingWithFreeWorkplace(education));
        }

        if(_placementManager.CheckingWhetherThereBuildingsOfType(BuidingType.SPORT) == true)
        {
            citizen.SetPlaceActivity(BuidingType.SPORT, _placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(BuidingType.SPORT, citizen));
        }

        if (_placementManager.CheckingWhetherThereBuildingsOfType(BuidingType.FOOD) == true)
        {
            citizen.SetPlaceActivity(BuidingType.FOOD, _placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(BuidingType.FOOD, citizen));
        }

        if (_placementManager.CheckingWhetherThereBuildingsOfType(BuidingType.RELAX) == true)
        {
            citizen.SetPlaceActivity(BuidingType.RELAX, _placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(BuidingType.RELAX, citizen));
        }

        if (_placementManager.CheckingWhetherThereBuildingsOfType(BuidingType.HEALTH) == true)
        {
            citizen.SetPlaceActivity(BuidingType.HEALTH, _placementManager.GetBuildingNearestBuildingToHouseCertainBuildingsType(BuidingType.HEALTH, citizen));
        }

        return citizen;
    }
}
