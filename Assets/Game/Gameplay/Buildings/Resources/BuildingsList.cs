using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "BuildingsList",
    menuName = "Buildings/newBuildingsList"
)]
public class BuildingsList : ScriptableObject
{
    [SerializeField] public List<Building> Buildings;
    [SerializeField] public string ID;

   // public static BuildingsList BuildingsInstance
   // {
   //     get { return Resources.Load<BuildingsList>("CommericalBuildingsList"); }
   // }
}
