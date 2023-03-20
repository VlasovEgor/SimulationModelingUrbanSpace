using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

[Serializable]
public class BuildingsManager: IInitializable
{
    [ReadOnly, ShowInInspector]
    private Dictionary<string, Dictionary<string, Building>> _buildingsTypeDictionaty = new();

    public void Initialize()
    {
        var buildingsTypeList = BuildingsTypeList.BuildingsTypeInstance;
        foreach (var buildingType in buildingsTypeList.BuildingsType)
        {
            Setup(buildingType);
        }
    }

    public void Setup(BuildingsList buildingsList)
    {
        Dictionary<string, Building> keyValues = new();

        for (int i = 0, count = buildingsList.Buildings.Count; i < count; i++)
        {
            var upgrade = buildingsList.Buildings[i];
            keyValues[upgrade.Id] = upgrade;
        }

       _buildingsTypeDictionaty.Add(buildingsList.ID, keyValues);
    }

    public Building[] GetAllBuildings(string builidngListId)
    {
        foreach (var keyValue in _buildingsTypeDictionaty)
        {
            if(keyValue.Key == builidngListId)
            {
               return keyValue.Value.Values.ToArray();
            }
        }

        throw new Exception($"there are no buildings of this type: {builidngListId}");
    }
}
