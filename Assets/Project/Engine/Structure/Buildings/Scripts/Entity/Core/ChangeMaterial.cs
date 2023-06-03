using System;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private CollisionCheck _collisionCheck;

    [SerializeField] private MeshRenderer _meshRenderer1;
    [SerializeField] private MeshRenderer _meshRenderer2;

    public CollisionCheck CollisionCheck
    {
        get => default;
        set
        {
        }
    }

    private void OnEnable()
    {
        _collisionCheck.BuildingCollided += SetConflictMaterial;
        _collisionCheck.CollidedBuildingsHasPassed += SetDefaultMaterial;
    }

    private void OnDisable()
    {
        _collisionCheck.BuildingCollided -= SetConflictMaterial;
        _collisionCheck.CollidedBuildingsHasPassed -= SetDefaultMaterial;
    }

    private void SetConflictMaterial()
    {
        _meshRenderer1.material.color = Color.red;
        _meshRenderer2.material.color = Color.red;
    }

    private void SetDefaultMaterial()
    {
        _meshRenderer1.material.color = Color.white;
        _meshRenderer2.material.color = Color.white;
    }
}
