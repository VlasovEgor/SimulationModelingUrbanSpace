using Entities;
using UnityEngine;
using Zenject;

public class WorldInstaller : MonoInstaller
{
    [SerializeField] private UnityEntity _character;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        BindCharacter();
        BindCamera();
    }

    private void BindCharacter()
    {
        Container.Bind<IEntity>().
            To<UnityEntity>().
            FromInstance(_character).
            AsSingle();
    }

    private void BindCamera()
    {
        Container.Bind<Camera>().
            FromInstance(_camera).
            AsSingle();
    }
}
