using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class Period : IPeriod
    {
        public event Action OnStarted;

        public event Action OnPeriodEvent;

        public event Action OnStoped;

        [PropertyOrder(-10)]
        [PropertySpace]
        [ReadOnly]
        [ShowInInspector]
        public bool IsActive
        {
            get { return this.coroutine != null; }
        }
        
        public MonoBehaviour CoroutineDispatcher
        {
            set { this.coroutineDispatcher = value; }
        }

        [SerializeField]
        private float period;

        [SerializeField]
        private MonoBehaviour coroutineDispatcher;

        private Coroutine coroutine;

        public Period()
        {
        }

        public Period(MonoBehaviour coroutineDispatcher, float period)
        {
            this.coroutineDispatcher = coroutineDispatcher;
            this.period = period;
        }

        public void Play()
        {
            if (this.coroutine == null)
            {
                this.coroutine = this.coroutineDispatcher.StartCoroutine(this.PeriodRoutine());
                this.OnStarted?.Invoke();
            }
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.coroutineDispatcher.StopCoroutine(this.coroutine);
                this.coroutine = null;
                this.OnStoped?.Invoke();
            }
        }

        private IEnumerator PeriodRoutine()
        {
            var period = new WaitForSeconds(this.period);
            while (true)
            {
                yield return period;
                this.OnPeriodEvent?.Invoke();
            }
        }
    }
}