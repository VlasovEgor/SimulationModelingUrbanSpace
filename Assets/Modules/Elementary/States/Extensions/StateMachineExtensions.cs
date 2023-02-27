namespace Elementary.Extensions
{
    public static class StateMachineExtensions
    {
        public static void SwitchStateSafely<T>(this IStateMachine<T> it, T key)
        {
            if (!it.CurrentState.Equals(key))
            {
                it.SwitchState(key);
            }
        }
    }
}