using System;
using System.Collections.Generic;
using Zenject;

public class StatisticsCollector : IInitializable , IDisposable
{
    [Inject] private Clock _clock;
    [Inject] private CitizensManager _citizensManager;
    [Inject] private PlacementManager _placementManager;
    [Inject] private AgentPath _agentPath;

    private List<int> _populationSize = new();
    private List<int> _levelHappiness = new();
    private List<int> _numberResidentialBuildings = new();
    private List<int> _numberBuildingsTypeWork = new();
    private List<int> _numberBuildingsTypeFood = new();
    private List<int> _numberBuildingsTypeSport = new();
    private List<int> _numberBuildingsTypeHealth = new();
    private List<int> _numberBuildingsTypeRelax = new();
    private List<int> _numberVacancies = new();
    private List<int> _numberUnemployed = new();
    private List<int> _percentageCarSelection = new();

    public void Initialize()
    {
        _clock.DayPassed += CollectStatistics;
    }

    public void Dispose()
    {
        _clock.DayPassed -= CollectStatistics;
    }

    private void CollectStatistics()
    {
        _populationSize.Add(_citizensManager.GetCitizensCount());
        _levelHappiness.Add(_citizensManager.GetOverallLevelHappiness());
        _numberResidentialBuildings.Add(_placementManager.GetNumberBuildingsOfCertainType(BuidingType.RESIDENTIAL));
        _numberBuildingsTypeWork.Add(_placementManager.GetNumberBuildingsOfCertainType(BuidingType.WORK));
        _numberBuildingsTypeFood.Add(_placementManager.GetNumberBuildingsOfCertainType(BuidingType.FOOD));
        _numberBuildingsTypeSport.Add(_placementManager.GetNumberBuildingsOfCertainType(BuidingType.SPORT));
        _numberBuildingsTypeHealth.Add(_placementManager.GetNumberBuildingsOfCertainType(BuidingType.HEALTH));
        _numberBuildingsTypeRelax.Add(_placementManager.GetNumberBuildingsOfCertainType(BuidingType.RELAX));
        _numberVacancies.Add(_placementManager.GetTotalNumberOfVacancies());
        _numberUnemployed.Add(_citizensManager.GetNumberUnemployed());
        _percentageCarSelection.Add(_agentPath.GetPercentageCarSelection());
    }

    public List<int> GetPoulationSize()
    {
       return _populationSize;
    }

    public List<int> GetLevelHappiness()
    {
        return _levelHappiness;
    }

    public List<int> GetNumberResidentialBuildings()
    {
        return _numberResidentialBuildings;
    }

    public List<int> GetNumberBuildingsTypeWork()
    {
        return _numberBuildingsTypeWork;
    }

    public List<int> GetBuildingsTypeFood()
    {
        return _numberBuildingsTypeFood;
    }

    public List<int> GetBuildingsTypeSport()
    {
        return _numberBuildingsTypeSport;
    }

    public List<int> GetBuildingsTypeHealth()
    {
        return _numberBuildingsTypeHealth;
    }

    public List<int> GetBuildingsTypeRelax()
    {
        return _numberBuildingsTypeRelax;
    }


    public List<int> GetNumberVacancies()
    {
        return _numberVacancies;
    }

    public List<int> GetNumberUnemployed()
    {
        return _numberUnemployed;
    }

    public List<int> GetPercentageCarSelection()
    {
        throw new NotImplementedException();
    }
}
