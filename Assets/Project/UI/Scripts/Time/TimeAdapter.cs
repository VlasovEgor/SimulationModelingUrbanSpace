using System;
using UnityEngine;
using Zenject;

public class TimeAdapter : MonoBehaviour
{
    [SerializeField] private TimeUI _timeUI;

    [Inject] private Clock _clock;

    public TimeUI TimeUI
    {
        get => default;
        set
        {
        }
    }

    public Clock Clock
    {
        get => default;
        set
        {
        }
    }

    private void Start()
    {
        _clock.Tick += OnTimeChanged;
    }

    private void OnDestroy()
    {
        _clock.Tick -= OnTimeChanged;
    }

    private void OnTimeChanged(DateTime dateTime)
    {
        _timeUI.UpdateTime(dateTime);
    }
}
