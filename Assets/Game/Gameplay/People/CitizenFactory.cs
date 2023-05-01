using Zenject;

public class CitizenFactory
{
    [Inject] private PlacementManager _placementManager;
    [Inject] private AgentPath _agentPath;

    public Citizen CreateCitizen(Education education, BuildingConfig homeConfig)
    {
        var citizen = new Citizen(education, _agentPath);

        citizen.SetEducation(education);

        citizen.SetPlaceActivity(BuidingType.RESIDENTIAL, homeConfig);

        if(_placementManager.TryGetRandomBuildingPositionWithFreeWorkplace(education) == true)
        {   
            citizen.SetPlaceActivity(BuidingType.WORK, _placementManager.GetBuildingWithFreeWorkplace(education));
        }

        citizen.SelectNewNearestActivityLocations(_placementManager);

        return citizen;
    }
}
