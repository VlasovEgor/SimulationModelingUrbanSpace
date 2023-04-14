using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private DateTime _dateTime = new();

    [SerializeField] private float secPerMin = 1;

    [ShowInInspector, ReadOnly] private int _hour;
    [ShowInInspector, ReadOnly] private int _minute;

    [ShowInInspector, ReadOnly] private int _day;
    [ShowInInspector, ReadOnly] private int _month;
    [ShowInInspector, ReadOnly] private int _year;

    [ShowInInspector, ReadOnly] private const int HOUR_OF_DAY = 24;
    [ShowInInspector, ReadOnly] private const int MINUTE_OF_HOUR = 60;

    [ShowInInspector, ReadOnly] private const int DAY_OF_MONTH = 30;
    [ShowInInspector, ReadOnly] private const int MONTH_OF_YEAR = 12;

    [ShowInInspector, ReadOnly] float _timer = 0;

    void Update()
    {
        TimeCounting();
    }

    private void TimeCounting()
    {
        if (_timer >= secPerMin)
        {
            _minute++;
            _dateTime = _dateTime.AddMinutes(1);

            if (_minute >=MINUTE_OF_HOUR)
            {
                _minute = 0;
                _hour++;
                _dateTime = _dateTime.AddHours(1);
                if(_hour >= HOUR_OF_DAY) 
                {
                    _hour = 0;
                    _day++;
                    _dateTime = _dateTime.AddDays(1);
                    if(_day >= DAY_OF_MONTH) 
                    {
                        _day= 1;
                        _month++;
                        _dateTime = _dateTime.AddMonths(1);
                        if(_month >= MONTH_OF_YEAR)
                        {
                            _month = 1;
                            _year++;
                            _dateTime = _dateTime.AddYears(1);
                        }
                    }
                }
            }
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    
}
