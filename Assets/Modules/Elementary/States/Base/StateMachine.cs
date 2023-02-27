using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public class StateMachine<T> : State, IStateMachine<T>
    {
        public event Action<T> OnStateSwitched;

        public T CurrentState
        {
            get { return this.key; }
        }

        [OnValueChanged("SwitchState")]
        [Space]
        [SerializeField]
        private T key;

        [SerializeField]
        private List<StateHolder> states = new();

        private IState currentState;

        public virtual void SwitchState(T key)
        {
            if (!ReferenceEquals(this.currentState, null))
            {
                this.currentState.Exit();
            }

            this.key = key;
            if (this.FindState(this.key, out this.currentState))
            {
                this.currentState.Enter();
            }

            this.OnStateSwitched?.Invoke(key);
        }

        [Title("Methods")]
        [GUIColor(0, 1, 0)]
        [Button]
        public override void Enter()
        {
            if (ReferenceEquals(this.currentState, null) && this.FindState(this.key, out this.currentState))
            {
                this.currentState.Enter();
            }
        }

        [GUIColor(0, 1, 0)]
        [Button]
        public override void Exit()
        {
            if (!ReferenceEquals(this.currentState, null))
            {
                this.currentState.Exit();
                this.currentState = null;
            }
        }

        public bool AddState(T key, IState state)
        {
            if (this.FindState(key, out _))
            {
                return false;
            }

            this.states.Add(new StateHolder
            {
                key = key,
                state = state
            });

            return true;
        }

        public bool RemoveState(T key)
        {
            for (int i = 0, count = this.states.Count; i < count; i++)
            {
                StateHolder holder = this.states[i];
                if (holder.key.Equals(key))
                {
                    this.states.Remove(holder);
                    return true;
                }
            }

            return false;
        }

        public void Setup(params StateHolder[] states)
        {
            this.states = new List<StateHolder>(states);
        }

        public void Setup(List<StateHolder> states)
        {
            this.states = states;
        }

        private bool FindState(T type, out IState state)
        {
            for (int i = 0, count = this.states.Count; i < count; i++)
            {
                StateHolder holder = this.states[i];
                if (holder.key.Equals(type))
                {
                    state = holder.state;
                    return true;
                }
            }

            state = default;
            return false;
        }

        [Serializable]
        public struct StateHolder
        {
            [SerializeField]
            public T key;

            [SerializeReference]
            public IState state;
        }
    }
}