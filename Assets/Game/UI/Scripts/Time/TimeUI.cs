using System;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _timeText;

    public void UpdateTime(DateTime dateTime)
    {
        _dateText.text= dateTime.ToShortDateString();
        _timeText.text= dateTime.ToShortTimeString();
    }
}
