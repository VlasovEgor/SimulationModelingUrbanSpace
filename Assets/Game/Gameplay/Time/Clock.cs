using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class Clock : ITickable
{
    public event Action<DateTime> Tick;
    private DateTime _dateTime = new(2000, 01, 01, 00, 00, 00);

    [SerializeField] private float secPerMin = 1;

    private int _hour;
    private int _minute;

    private int _day;
    private int _month;
    private int _year;

    private const int HOUR_OF_DAY = 24;
    private const int MINUTE_OF_HOUR = 60;

    private const int DAY_OF_MONTH = 30;
    private const int MONTH_OF_YEAR = 12;

    float _timer = 0;

    void ITickable.Tick()
    {
        TimeCounting();
    }

    private void TimeCounting()
    {
        if (_timer >= secPerMin)
        {
            _minute++;
            _dateTime = _dateTime.AddMinutes(1);

            if (_minute >= MINUTE_OF_HOUR)
            {
                _minute = 0;
                _hour++;
                _dateTime = _dateTime.AddHours(1);
                if (_hour >= HOUR_OF_DAY)
                {
                    _hour = 0;
                    _day++;
                    _dateTime = _dateTime.AddDays(1);
                    if (_day >= DAY_OF_MONTH)
                    {
                        _day = 1;
                        _month++;
                        _dateTime = _dateTime.AddMonths(1);
                        if (_month >= MONTH_OF_YEAR)
                        {
                            _month = 1;
                            _year++;
                            _dateTime = _dateTime.AddYears(1);
                        }
                    }
                }
            }
            _timer = 0;
            Tick?.Invoke(_dateTime);
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }
}
