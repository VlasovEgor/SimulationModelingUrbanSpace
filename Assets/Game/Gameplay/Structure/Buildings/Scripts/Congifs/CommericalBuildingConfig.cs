using System;
using UnityEngine;

public class CommericalBuildingConfig : BuildingConfig
{
    [SerializeField] private string _name;

    [Space]
    [SerializeField] private int _maximumNumberEmployeesWithHigherEducation;
    [SerializeField] private int _maxiuimNumberEmployeesWithSecondaryEducation;
    [SerializeField] private int _maximumNumberEmployeesWithoutEducation;

    [Space]
    [SerializeField] private int _currentNumberEmployeesWithHigherEducation;
    [SerializeField] private int _currentNumberEmployeesWithSecondaryEducation;
    [SerializeField] private int _currnetNumberEmployeesWithoutEducation;

    [Space]
    [SerializeField] private int _maximumNumberVisitors;
    [SerializeField] private int _currentNumberVisitors;
    [SerializeField] private int _amountOfSatisfactionOfNeed;

    [Space]
    [SerializeField] private int _averageTimeInBuilding;

    [Space]
    [SerializeField] private HourMinute _startWork;
    [SerializeField] private HourMinute _finishWork;

    public string GetName()
    {
        return _name;
    }


    public int GetMaximumNumberEmployeesOfCertainEducation(Education education)
    {
        if (education == Education.HIGHER_EDUCATION)
        {
            return _maximumNumberEmployeesWithHigherEducation;
        }
        else if (education == Education.SECOND_EDUCATION)
        {
            return _maxiuimNumberEmployeesWithSecondaryEducation;
        }
        else
        {
            return _maximumNumberEmployeesWithoutEducation;
        }
    }

    public int GetCurrentNumberEmployeesOfCertainEducation(Education education)
    {
        if(education == Education.HIGHER_EDUCATION)
        {
            return _currentNumberEmployeesWithHigherEducation;
        }
        else if (education == Education.SECOND_EDUCATION)
        {
            return _currentNumberEmployeesWithSecondaryEducation;
        }
        else
        {
            return _currnetNumberEmployeesWithoutEducation;
        }
    }

    public int GetMaximumNumberVisitors()
    {
        return _maximumNumberVisitors;
    }

    public int GetCurrnetNumberVisitors()
    {
        return _currentNumberVisitors;
    }

    public int GetAmountOfSatisfactionOfNeed()
    {
        return _amountOfSatisfactionOfNeed;
    }

    public int GetAverageTimeInBuilding()
    {
        return _averageTimeInBuilding;
    }

    public HourMinute GetStartWork()
    {
        return _startWork;
    }

    public HourMinute GetFinishWork()
    {
        return _finishWork;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetType(int index)
    {
        _buidingType = (BuidingType)Enum.GetValues(typeof(BuidingType)).GetValue(index);
    }

    public void SetMaximumNumberEmployeesOfCertainEducation(Education education, int number)
    {
        if (education == Education.HIGHER_EDUCATION)
        {
            _maximumNumberEmployeesWithHigherEducation = number;
        }
        else if (education == Education.SECOND_EDUCATION)
        {
            _maxiuimNumberEmployeesWithSecondaryEducation = number;
        }
        else
        {
            _maximumNumberEmployeesWithoutEducation = number;
        }
    }

    public void AddEmployeeOfCertainEducation(Education education)
    {
        if (education == Education.HIGHER_EDUCATION)
        {
            _currentNumberEmployeesWithHigherEducation++;
        }
        else if (education == Education.SECOND_EDUCATION)
        {
            _currentNumberEmployeesWithSecondaryEducation++;
        }
        else
        {
            _currnetNumberEmployeesWithoutEducation++;
        }
    }

    public void RemoveEmployeeOfCertainEducation(Education education)
    {
        if (education == Education.HIGHER_EDUCATION)
        {
            _currentNumberEmployeesWithHigherEducation--;
        }
        else if (education == Education.SECOND_EDUCATION)
        {
            _currentNumberEmployeesWithSecondaryEducation--;
        }
        else if(education == Education.WITOUT_EDUCATION)
        {
            _currnetNumberEmployeesWithoutEducation--;
        }
    }
   

    public void SetMaximumNumberVisitors(int number)
    {
        _maximumNumberVisitors = number;
    }

    public void AddVisitor()
    {
        _currentNumberVisitors++;
    }

    public void RemoveVisitor()
    {
        _currentNumberVisitors--;
    }

    public void SetAmountOfSatisfactionOfNeed(int amount)
    {
        _amountOfSatisfactionOfNeed = amount;
    }

    public void SetStartWork(HourMinute hourMinute)
    {
        _startWork = hourMinute;
    }

    public void SetFinishWork(HourMinute hourMinute)
    {
        _finishWork = hourMinute;
    }

    public void SetAverageTimeInBuilding(int minute)
    {
        _averageTimeInBuilding = minute;
    }

    
}
