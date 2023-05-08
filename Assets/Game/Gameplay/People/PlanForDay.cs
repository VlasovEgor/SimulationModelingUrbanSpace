using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class PlanForDay
{
    public List<CommericalBuildingConfig> DefiningPlanForDay(Citizen citizen)
    {
        List<CommericalBuildingConfig> buildingsPlanForDay = new List<CommericalBuildingConfig>();
        var needs = citizen.GetDictionartNeeds();

        var startDay = citizen.GetActiveTimeStart();
        var work = (CommericalBuildingConfig)citizen.GetPlaceActivity(BuidingType.WORK);

        var startWork = work.GetStartWork();

        var freeMorningTimeInMinute = (startWork.Hour * 60 + startWork.Minute) - (startDay.Hour * 60 + startDay.Minute);

        List<CommericalBuildingConfig> buildingsOpenAtThisTime = new();

        foreach (var keyValue in citizen.GetDictionaryPlacesActivity())
        {
            var currnetBuildng = (CommericalBuildingConfig)keyValue.Value;
            if(currnetBuildng !=null)
            {
                if (currnetBuildng.GetStartWork().Hour < startWork.Hour)
                {
                    buildingsOpenAtThisTime.Add(currnetBuildng);
                }

            }
        }

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

        Backpack backpack = new Backpack(freeMorningTimeInMinute - 30); // 30 minutes on average on the road

        backpack.MakeAllSets(items);

        List<Item> solve = backpack.GetBestSet();

        if (solve != null)
        {
            foreach (var item in solve)
            {
                buildingsPlanForDay.Add((CommericalBuildingConfig)item.Config);
            }
        }
        solve.Clear();
        buildingsPlanForDay.Add(work);



        var endWork = work.GetFinishWork();
        var endDay = citizen.GetActiveTimeEnd();
        var freeEveningTimeInMinute = (endDay.Hour * 60 + endDay.Minute) - (endWork.Hour * 60 + endWork.Minute);

        buildingsOpenAtThisTime.Clear();

        foreach (var keyValue in citizen.GetDictionaryPlacesActivity())
        {
            var currnetBuildng = (CommericalBuildingConfig)keyValue.Value;
            if (currnetBuildng.GetStartWork().Hour < endDay.Hour && buildingsPlanForDay.Contains(currnetBuildng) == false)
            {
                buildingsOpenAtThisTime.Add(currnetBuildng);
            }
        }

        items = new();

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

        backpack = new Backpack(freeEveningTimeInMinute - 30); // 30 minutes on average on the road

        backpack.MakeAllSets(items);

        solve = backpack.GetBestSet();

        if (solve != null)
        {
            foreach (var item in solve)
            {
                buildingsPlanForDay.Add((CommericalBuildingConfig)item.Config);
            }
        }

        return buildingsPlanForDay;
    }

    public List<BuildingConfig> MakePlanForDay(Citizen citizen)
    {

        var startTime = citizen.GetActiveTimeStart().Hour * 60 + citizen.GetActiveTimeStart().Minute;
        var endTime = citizen.GetActiveTimeEnd().Hour * 60 + citizen.GetActiveTimeEnd().Minute;

        var plan = new List<BuildingConfig>();

        var currentTime = startTime;

        var buildings = citizen.GetDictionaryPlacesActivity().ToList();
        var commericalBuildings = new List<CommericalBuildingConfig>();

        plan.Add(citizen.GetPlaceActivity(BuidingType.RESIDENTIAL));

        foreach (var buidling in buildings)
        {
            if(buidling.Value.GetBuidingType() != BuidingType.NONE && buidling.Value.GetBuidingType() != BuidingType.RESIDENTIAL)
            {
                commericalBuildings.Add((CommericalBuildingConfig)buidling.Value);
            }
        }

        for (int i = 0; i < commericalBuildings.Count; i++)
        {
            plan.Add(commericalBuildings[i]);
        }

        plan.Add(citizen.GetPlaceActivity(BuidingType.RESIDENTIAL));

        return plan;
    }
}