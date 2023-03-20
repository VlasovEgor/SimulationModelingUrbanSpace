using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "BuildingsTypeList",
    menuName = "Buildings/newBuildingsTypeList"
)]
public class BuildingsTypeList : ScriptableObject
{
    [SerializeField] public List<BuildingsList> BuildingsType;

    public static BuildingsTypeList BuildingsTypeInstance
    {
        get { return Resources.Load<BuildingsTypeList>("BuildingsTypeList"); }
    }
}
