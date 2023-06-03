using Sirenix.OdinInspector;
using UnityEngine;

public class BuildingEngine : MonoBehaviour
{
    [SerializeField] private CollisionCheck _collisionCheck;

    [ShowInInspector, ReadOnly]
    private bool _canBuild = true;

    public CollisionCheck CollisionCheck
    {
        get => default;
        set
        {
        }
    }

    public bool CanBuild()
    {
        return _canBuild;
    }

    private void OnEnable()
    {
        _collisionCheck.BuildingCollided += SetCanBuildFalse;
        _collisionCheck.CollidedBuildingsHasPassed += SetCanBuildTrue;
    }

    private void OnDisable()
    {
        _collisionCheck.BuildingCollided += SetCanBuildFalse;
        _collisionCheck.CollidedBuildingsHasPassed += SetCanBuildTrue;
    }

    private void SetCanBuildTrue()
    {
        _canBuild = true;
    }

    private void SetCanBuildFalse()
    {
        _canBuild = false;
    }

    
}
