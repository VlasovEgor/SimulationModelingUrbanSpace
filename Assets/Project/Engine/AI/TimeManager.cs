using System;
using Zenject;
using System.Collections.Generic;

public class TimeManager : IInitializable, IDisposable
{
    [Inject] private Clock _clock;
    [Inject] private AgentsController _agentsController;
    [Inject] private CitizensManager _citizensManager;
    [Inject] private PlacementManager _placementManager;

    private List<CitizenCommander> _citizensCommander = new();
    private List<Citizen> _citizens = new();

    public Clock Clock
    {
        get => default;
        set
        {
        }
    }

    public AgentsController AgentsController
    {
        get => default;
        set
        {
        }
    }

    public CitizenCommander CitizenCommander
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

    public void Initialize()
    {
        _clock.MinutePassed += OnMinutePassed;
        _clock.DayPassed += OnDayPassed;
    }

    public void Dispose()
    {
        _clock.MinutePassed -= OnMinutePassed;
        _clock.DayPassed -= OnDayPassed;
    }

    private void OnMinutePassed(DateTime dateTime)
    {
        _citizensCommander = _agentsController.GetCitizenCommanderList();

        for (int i = 0; i < _citizensCommander.Count; i++)
        {
            var citizenCommander = _citizensCommander[i];

            citizenCommander.SetCurrnetTime(dateTime);

            if (citizenCommander.CheckingForTransitionToNextBuilding() == true)
            {
                citizenCommander.MoveToNextBuilding();
            }

            _citizensCommander[i] = citizenCommander;
        }
    }

    private void OnDayPassed()
    {
        _citizens = _citizensManager.GetCitizensList();

        for (int i = 0; i < _citizensCommander.Count; i++)
        {
            var citizen = _citizens[i];
            citizen.SelectNewNearestActivityLocations(_placementManager);
            citizen.IncreasingNeeds();
            _citizens[i] = citizen;
        }

        _agentsController.InitializationCitizenCommanders();
    }
}
