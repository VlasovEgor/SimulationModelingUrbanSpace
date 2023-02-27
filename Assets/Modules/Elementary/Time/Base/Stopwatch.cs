using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class Stopwatch : IStopwatch
    {
        public event Action OnStarted;

        public event Action OnTimeChanged;

        public event Action OnCanceled;

        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.currentTime = Mathf.Max(value, 0); }
        }
        
        public MonoBehaviour CoroutineDispatcher
        {
            set { this.coroutineDispatcher = value; }
        }

        [SerializeField]
        private MonoBehaviour coroutineDispatcher;

        private float currentTime;

        private Coroutine coroutine;

        public Stopwatch()
        {
            this.currentTime = 0;
        }

        public Stopwatch(MonoBehaviour coroutineDispatcher)
        {
            this.coroutineDispatcher = coroutineDispatcher;
            this.currentTime = 0;
        }

        public void Play()
        {
            if (this.IsPlaying)
            {
                return;
            }

            this.IsPlaying = true;
            this.OnStarted?.Invoke();
            this.coroutine = this.coroutineDispatcher.StartCoroutine(this.TimerRoutine());
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.coroutineDispatcher.StopCoroutine(this.coroutine);
            }

            if (this.IsPlaying)
            {
                this.IsPlaying = false;
                this.OnCanceled?.Invoke();
            }
        }

        public void ResetTime()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }

        private IEnumerator TimerRoutine()
        {
            while (true)
            {
                yield return null;
                this.currentTime += Time.deltaTime;
                this.OnTimeChanged?.Invoke();
            }
        }
    }
}