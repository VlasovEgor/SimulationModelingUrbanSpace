using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BuildingsManager : MonoBehaviour
{
    [SerializeReference] private List<Building> _buildingsList;

    [ReadOnly, ShowInInspector]
    private Dictionary<string, Building> _buildings = new();

    public void Start()
    {
        Setup();
    }

    public void Setup()
    {
        _buildings = new Dictionary<string, Building>();
        for (int i = 0, count = _buildingsList.Count; i < count; i++)
        {
            var upgrade = _buildingsList[i];
            _buildings[upgrade.Id] = upgrade;
        }
    }

    public Building GetBuilding(string id)
    {
        return _buildings[id];
    }

    public Building[] GetAllBuildings()
    {
        return _buildings.Values.ToArray();
    }

    public int GetCount()
    {
        return _buildings.Count;
    }
}
