using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AgentsController
{
    [Inject] private CitizensManager _citizensManager;
    [Inject] private AgentPath _agentPath;
    //[Inject] private PlanForDay _planForDay;

    private PlanForDay _planForDay = new();

    private List<Citizen> _citizens = new();
    private List<CitizenCommander> _citizenCommanders = new();

    public List<CitizenCommander> GetCitizenCommanderList()
    {
        return _citizenCommanders;
    }

    public void InitializationCitizenCommanders()
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
                CitizenCommander citizenCommander = new(_agentPath);
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
            var planForDay = _planForDay.DefiningPlanForDay(_citizenCommanders[i]);
            _citizenCommanders[i].SetPlanForDay(planForDay);
            _citizens[i].AccrualNeed(planForDay);

        }
    }

}
