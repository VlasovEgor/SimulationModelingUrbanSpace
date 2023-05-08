using System;
using Zenject;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

public class TimeManager : IInitializable, IDisposable
{
    [Inject] private Clock _clock;
    [Inject] private CitizensManager _citizensManager;
    [Inject] private PlacementManager _placementManager;

    private List<Citizen> _citizens = new();
    private PlanForDay _planForDay = new();

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
        _citizens = _citizensManager.GetCitizensList();

        for (int i = 0; i < _citizens.Count; i++)
        {
            var citizen = _citizens[i];

            citizen.SetCurrnetTime(dateTime);

            if (citizen.CheckingForTransitionToNextBuilding() == true)
            {
                citizen.MoveToNextBuilding();
            }

            _citizens[i] = citizen;
        }


    }

    private void OnDayPassed()
    {
        foreach (var citizen in _citizens)
        {
            citizen.SelectNewNearestActivityLocations(_placementManager);
            citizen.IncreasingNeeds();
            var planForDay = _planForDay.MakePlanForDay(citizen);
            citizen.SetPlanForDay(planForDay);
        }
    }
}
