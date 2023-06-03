using UnityEngine;
using Zenject;

public class BuildingConfigFactory : MonoBehaviour
{
    [Inject] private CommericalBuildingConfigFactory _commericalBuildingConfigFactory;
    [Inject] private ResidentialBuildingConfigFactory _residentialBuildingConfigFactory;
}
