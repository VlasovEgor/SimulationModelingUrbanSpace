using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public class StateComposite : State
    {
        [SerializeReference]
        protected List<IState> states = new();

        public StateComposite()
        {
        }

        public StateComposite(params IState[] states)
        {
            this.states = new List<IState>(states);
        }
        
        public override void Enter()
        {
            for (int i = 0, count = this.states.Count; i < count; i++)
            {
                var state = this.states[i];
                state.Enter();
            }
        }

        public override void Exit()
        {
            for (int i = 0, count = this.states.Count; i < count; i++)
            {
                var state = this.states[i];
                state.Exit();
            }
        }

        public void AddState(IState state)
        {
            this.states.Add(state);
        }

        public void RemoveState(IState state)
        {
            this.states.Remove(state);
        }
    }
}