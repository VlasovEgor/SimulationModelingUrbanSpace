using System;
using Zenject;
using System.Collections.Generic;
using System.Diagnostics;

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


        foreach (var citizen in _citizens)
        {
            citizen.SetCurrnetTime(dateTime);
            //citizen.CheckingIfBusy();
        }
    }

    private void OnDayPassed()
    {
        foreach (var citizen in _citizens)
        {
            citizen.SelectNewNearestActivityLocations(_placementManager);
            citizen.IncreasingNeeds();
           // citizen.SetPlanForDay(_planForDay.DefiningPlanForDay(citizen));
        }
    }
}
