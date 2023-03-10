using UnityEngine;

[CreateAssetMenu (fileName = "New BuildingConfig",
    menuName = "Buildings/New BuildingConfig")
    ]
public class Building: ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] private string _title;
    [SerializeField] private Sprite _icon;

    public string Id 
    {
        get { return _id; } 
    }

    public string Title 
    { 
        get { return _title; } 
    }

    public Sprite Icon
    { 
        get { return _icon; } 
    }

    public GameObject BuildingPrefab
    { 
        get { return _buildingPrefab; } 
    }
}
