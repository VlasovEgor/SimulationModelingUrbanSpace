using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorldInstaller : MonoInstaller
{
    [SerializeField] private UnityEntity _character;

    public override void InstallBindings()
    {
        BindCharacter();
    }

    private void BindCharacter()
    {
        Container.Bind<IEntity>().
            To<UnityEntity>().
            FromInstance(_character).
            AsSingle();
    }
}
