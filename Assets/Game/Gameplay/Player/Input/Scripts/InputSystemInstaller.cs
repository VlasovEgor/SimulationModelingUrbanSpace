using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class InputSystemInstaller : MonoInstaller
{
    [ShowInInspector]
    private InputStateManager _stateManager = new();

    [SerializeField] private ManipulationInput _manipulationInput;

    public override void InstallBindings()
    {
        BindManipulationInput();
    }

    private void BindManipulationInput()
    {
        Container.Bind<ManipulationInput>().
            FromInstance(_manipulationInput).
            AsSingle();
    }



    private void ConstructStateManager()
    {
       // var states = new List<InputStateManager.StateHolder>
       //     {
       //         new()
       //         {
       //             key = InputStateType.BASE,
       //             state = new StateComposite(
       //                 new InputState_Joystick(this.joystick)
       //             )
       //         },
       //         new()
       //         {
       //             key = InputStateType.LOCK,
       //             state = new State()
       //         },
       //         new()
       //         {
       //             key = InputStateType.DIALOG,
       //             state = new State()
       //         }
       //     };
       //
       // this._stateManager.Setup(states);
    }
}
