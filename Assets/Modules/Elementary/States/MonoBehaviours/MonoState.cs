using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/States/State")]
    public class MonoState : MonoBehaviour
    {
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}