using UnityEngine;
using Zenject;

public class UICrutch : MonoBehaviour
{
    [SerializeField] private GameObject _commericalBuildingPopup;
    [SerializeField] private GameObject _residentialBuildingPopup;

    [SerializeField] private GameObject _commericalBuildingButton;
    [SerializeField] private GameObject _residentialBuildingButton;

    [Inject] private RoadSystem _roadSystem;

    private void Start()
    {
        _roadSystem.RoadPaved += SwitchUI;
    }

    private void SwitchUI()
    {
        _commericalBuildingPopup.SetActive(true);
        _residentialBuildingPopup.SetActive(true);
        _commericalBuildingButton.SetActive(true);
        _residentialBuildingButton.SetActive(true);

        _roadSystem.RoadPaved -= SwitchUI;
    }   
}
