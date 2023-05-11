using System.Collections.Generic;
using UnityEngine;

public class PlanForDay
{
    public List<BuildingConfig> DefiningPlanForDay(CitizenCommander citizen)
    {
        List<BuildingConfig> buildingsPlanForDay = new();

        var needs = citizen.GetDictionaryNeeds();

        var startDay = citizen.GetActiveTimeStart();
        var work = (CommericalBuildingConfig)citizen.GetPlaceActivity(BuidingType.WORK);

        var startWork = work.GetStartWork();
        var endWork = work.GetFinishWork();
        var timeStartWork = Random.Range((startWork.Hour * 60 + startWork.Minute), (endWork.Hour * 60 + endWork.Minute - work.GetwWorkingHoursOfEmployeesInMinute()));
        var timeEndWork = timeStartWork + work.GetwWorkingHoursOfEmployeesInMinute();

        var freeMorningTimeInMinute = timeStartWork - (startDay.Hour * 60 + startDay.Minute);

        List<CommericalBuildingConfig> buildingsOpenAtThisTime = new();

        buildingsPlanForDay.Add(citizen.GetPlaceActivity(BuidingType.RESIDENTIAL));

        foreach (var keyValue in citizen.GetDictionaryPlacesActivity())
        {   
            if(keyValue.Key != BuidingType.NONE && keyValue.Key != BuidingType.RESIDENTIAL)
            {
                var currnetBuildng = (CommericalBuildingConfig)keyValue.Value;
                if (currnetBuildng != null)
                {
                    if (currnetBuildng.GetStartWork().Hour < startWork.Hour)
                    {
                        buildingsOpenAtThisTime.Add(currnetBuildng);
                    }
                }
            }
           
        }

        List<Item> items = new();

        items.AddRange(CreateItems(needs, buildingsOpenAtThisTime));

        AddingCommercialBuildingsToPlan(freeMorningTimeInMinute, items, buildingsPlanForDay);

        int timeSpentInBuildings = 30;

        for (int i = 1; i < buildingsPlanForDay.Count; i++)
        {
            var currentBuilding = (CommericalBuildingConfig)buildingsPlanForDay[i];
            timeSpentInBuildings += currentBuilding.GetAverageTimeInBuilding();
        }

        citizen.ChangeActiveTimeStart(timeSpentInBuildings);

        buildingsPlanForDay.Add(work);

        var endDay = citizen.GetActiveTimeEnd();
        var freeEveningTimeInMinute = (endDay.Hour * 60 + endDay.Minute) - timeEndWork;

        buildingsOpenAtThisTime.Clear();

        foreach (var keyValue in citizen.GetDictionaryPlacesActivity())
        {
            if (keyValue.Key != BuidingType.NONE && keyValue.Key != BuidingType.RESIDENTIAL)
            {
                var currnetBuildng = (CommericalBuildingConfig)keyValue.Value;
                if (currnetBuildng != null && buildingsPlanForDay.Contains(currnetBuildng) == false)
                {
                    if (currnetBuildng.GetStartWork().Hour < endDay.Hour)
                    {
                        buildingsOpenAtThisTime.Add(currnetBuildng);
                    }
                }
            }
        }

        items.Clear();

        items.AddRange(CreateItems(needs, buildingsOpenAtThisTime));

        AddingCommercialBuildingsToPlan(freeEveningTimeInMinute, items, buildingsPlanForDay);

        buildingsPlanForDay.Add(citizen.GetPlaceActivity(BuidingType.RESIDENTIAL));

        return buildingsPlanForDay;
    }

    private List<Item> CreateItems(Dictionary<Needs, int> needs, List<CommericalBuildingConfig> buildingsOpenAtThisTime)
    {
        List<Item> items = new();

        foreach (var buildingConfig in buildingsOpenAtThisTime)
        {
            Item item = null;

            if (buildingConfig.GetBuidingType() == BuidingType.FOOD)
            {
                item = new(buildingConfig, buildingConfig.GetAverageTimeInBuilding(), 100 - needs[Needs.FOOD]);
            }
            else if (buildingConfig.GetBuidingType() == BuidingType.SPORT)
            {
                item = new(buildingConfig, buildingConfig.GetAverageTimeInBuilding(), 100 - needs[Needs.SPORT]);
            }
            else if (buildingConfig.GetBuidingType() == BuidingType.RELAX)
            {
                item = new(buildingConfig, buildingConfig.GetAverageTimeInBuilding(), 100 - needs[Needs.REST]);
            }
            else if (buildingConfig.GetBuidingType() == BuidingType.HEALTH)
            {
                item = new(buildingConfig, buildingConfig.GetAverageTimeInBuilding(), 100 - needs[Needs.HEALTH]);
            }

            items.Add(item);
        }

        return items;
    }

    private void AddingCommercialBuildingsToPlan(int freeTimeInMinute, List<Item> items, List<BuildingConfig> buildingsPlanForDay)
    {
        Backpack backpack = new(freeTimeInMinute - 30);
        backpack.MakeAllSets(items);

        var solve = new List<Item>();
        solve.AddRange(backpack.GetBestSet());

        if (solve != null)
        {
            foreach (var item in solve)
            {
                buildingsPlanForDay.Add((CommericalBuildingConfig)item.Config);
            }
        }
    }
}