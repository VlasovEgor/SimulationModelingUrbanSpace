using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zenject;

public class AgentsController
{
    [Inject] private CitizensManager _citizensManager;
    [Inject] private AgentPath _agentPath;

    private PlanForDay _planForDay = new();

    private List<Citizen> _citizens = new();
    private List<CitizenCommander> _citizenCommanders = new();

    public AgentPath AgentPath
    {
        get => default;
        set
        {
        }
    }

    public PlanForDay PlanForDay
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
        _citizens = _citizensManager.GetCitizensList();
    }

    private void ChangeNumberCitizenCommanders()
    {
        if (_citizens.Count > _citizenCommanders.Count)
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
            var citizen = _citizens[i];
            _citizenCommanders[i].GetData(citizen);
            var planForDay = _planForDay.DefiningPlanForDay(_citizenCommanders[i]);
            _citizenCommanders[i].SetPlanForDay(planForDay);
            citizen.AccrualNeed(planForDay);
            _citizens[i] = citizen;
        }
    }
}
