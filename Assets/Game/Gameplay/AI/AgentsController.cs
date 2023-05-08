using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AgentsController : MonoBehaviour, IInitializable, IDisposable
{
    [Inject] private TimeManager _timeManager;
    [Inject] private CitizensManager _citizensManager;
    [Inject] private Clock _clock;

    private List<Citizen> _citizens = new();
    private List<CitizenCommander> _citizenCommanders = new();

    public void Dispose()
    {
        _clock.DayPassed += InitializationCitizenCommanders;
    }

    public void Initialize()
    {
        _clock.DayPassed -= InitializationCitizenCommanders;
    }

    private void InitializationCitizenCommanders()
    {
        GetListOfCitizens();
        ChangeNumberCitizenCommanders();
        DataTransferfFromCitizenToCitizenCommanders();
    }


    private void GetListOfCitizens()
    {
        _citizens.Clear();
        _citizens.AddRange(_citizensManager.GetCitizensList());
    }

    private void ChangeNumberCitizenCommanders()
    {   
        if(_citizens.Count > _citizenCommanders.Count)
        {
            while (_citizens.Count > _citizenCommanders.Count)
            {
                CitizenCommander citizenCommander = new();
                _citizenCommanders.Add(citizenCommander);
            }
        }
        else if (_citizens.Count < _citizenCommanders.Count)
        {
            while (_citizens.Count < _citizenCommanders.Count)
            {
                _citizenCommanders.Remove(_citizenCommanders.Last());
            }
        }

    }

    private void DataTransferfFromCitizenToCitizenCommanders()
    {
        for (int i = 0; i < _citizens.Count; i++)
        {
            _citizenCommanders[i].GetData(_citizens[i]);
        }
    }

}
