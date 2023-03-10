using UnityEngine;

public class CanBuild_Component : MonoBehaviour, ICanBuild_Component
{
    [SerializeField] private BuildingEngine _buildingEngine;

    public bool CanBuild()
    {
        return _buildingEngine.CanBuild();
    }
}
