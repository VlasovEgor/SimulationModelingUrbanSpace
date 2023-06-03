using UnityEngine;
using Zenject;

public class UIInstallers : MonoInstaller
{
    [SerializeField] private PopupManager _popupManager;

    public override void InstallBindings()
    {
        BindPopupManager();
    }

    private void BindPopupManager()
    {
        Container.Bind<PopupManager>().
            FromInstance(_popupManager).
            AsSingle().
            NonLazy();
    }
}
