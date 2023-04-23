using UnityEngine;

public class ResidentialBuildingConfig : BuildingConfig
{
    [SerializeField] private int _numberResidentsWithHigherEducation;
    [SerializeField] private int _numberResidentsWithSecondaryEducation;
    [SerializeField] private int _numberResidentsWithoutEducation;

    public int GetNumberResidentsWithHigherEducation()
    {
        return _numberResidentsWithHigherEducation;
    }

    public int GetNumberResidentsWithSecondaryEducation()
    {
        return _numberResidentsWithSecondaryEducation;

    }

    public int GetNumberResidentsWithoutEducation()
    {
        return _numberResidentsWithoutEducation;
    }

    public void SetNumberResidentsWithHigherEducation(int number)
    {
        _numberResidentsWithHigherEducation = number;
    }

    public void SetNumberResidentsWithSecondaryEducation(int number)
    {
        _numberResidentsWithSecondaryEducation = number;
    }

    public void SetNumberResidentsWithoutEducation(int number)
    {
        _numberResidentsWithoutEducation = number;
    }
}
