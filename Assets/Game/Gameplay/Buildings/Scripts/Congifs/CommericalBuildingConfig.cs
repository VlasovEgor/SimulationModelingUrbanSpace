using System;
using UnityEngine;

public class CommericalBuildingConfig : BuildingConfig
{
    [Space]
    [SerializeField] private string _name;

    [Space]
    [SerializeField] private CommericalBuidingType _buidingType;

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
    [SerializeField] private HourMinute _startWork;
    [SerializeField] private HourMinute _finishWork;

    public string GetName()
    {
        return _name;
    }

    public CommericalBuidingType GetCommericalBuidingType()
    {
        return _buidingType;
    }

    public int GetMaximumNumberEmployeesWithHigherEducation()
    {
        return _maximumNumberEmployeesWithHigherEducation;
    }

    public int GetMaximumNumberEmployeesWithSecondaryEducation()
    {
        return _maxiuimNumberEmployeesWithSecondaryEducation;
    }

    public int GetMaximumNumberEmployeesWithoutEducation()
    {
        return _maximumNumberEmployeesWithoutEducation;
    }

    public int GetCurrentNumberEmployeesWithHigherEducation()
    {
        return _currentNumberEmployeesWithHigherEducation;
    }

    public int GetCurrentNumberEmployeesWithSecondaryEducation()
    {
        return _currentNumberEmployeesWithSecondaryEducation;
    }

    public int GetCurrentumberEmployeesWithoutEducation()
    {
        return _currnetNumberEmployeesWithoutEducation;
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

    public void SetType(CommericalBuidingType buidingType)
    {
        _buidingType = buidingType;
    }

    public void SetType(int index)
    {
        _buidingType = (CommericalBuidingType)Enum.GetValues(typeof(CommericalBuidingType)).GetValue(index);
    }

    public void SetMaximumNumberEmployeesWithHigherEducation(int number)
    {
        _maximumNumberEmployeesWithHigherEducation = number;
    }

    public void SetMaximumNumberEmployeesWithSecondaryEducation(int number)
    {
        _maxiuimNumberEmployeesWithSecondaryEducation = number;
    }

    public void SetMaximumNumberEmployeesWithoutEducation(int number)
    {
        _maximumNumberEmployeesWithoutEducation = number;
    }

    public void AddEmployeWithHigherEducation()
    {
        _currentNumberEmployeesWithHigherEducation++;
    }

    public void AddEmployeWithSecondaryEducation()
    {
        _currentNumberEmployeesWithSecondaryEducation++;
    }

    public void AddEmployeWithoutEducation()
    {
        _currnetNumberEmployeesWithoutEducation++;
    }

    public void RemoveEmployeWithHigherEducation()
    {
        _currentNumberEmployeesWithHigherEducation--;
    }

    public void RemoveEmployeWithSecondaryEducation()
    {
        _currentNumberEmployeesWithSecondaryEducation--;
    }

    public void RemoveEmployeWithoutEducation()
    {
        _currnetNumberEmployeesWithoutEducation--;
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
}
