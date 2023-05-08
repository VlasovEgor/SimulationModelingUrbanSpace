using System;
using System.Collections.Generic;
using UnityEngine;

public class CitizenCommander : MonoBehaviour
{
    private Dictionary<Needs, int> _citizenNeeds;
    private Dictionary<BuidingType, BuildingConfig> _placesActivity;

    private bool _isWorth;
    private bool _isActive;

    private DateTime _releaseTime;
    private DateTime _currentTime;
    private CommericalBuildingConfig _currnetCommericalBuildng;
    private Agent _currentAgent;

    private HourMinute _activeTimeStart;
    private HourMinute _activeTimeEnd;

    private List<BuildingConfig> _planForDay;

    public int _buildingCounter;
    private AgentPath _agentPath;

    public void GetData(Citizen citizen)
    {
        _citizenNeeds = citizen.GetDictionartNeeds();
        _placesActivity = citizen.GetDictionaryPlacesActivity();

        _activeTimeStart = citizen.GetActiveTimeStart();
        _activeTimeEnd= citizen.GetActiveTimeEnd();
    }
}
