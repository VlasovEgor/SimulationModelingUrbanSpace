using UnityEngine;

public class ButtonOpenCloseStats : MonoBehaviour
{
    [SerializeField] private PopupManager _popupManager;
    [SerializeField] private PopupName _buildingPopup;
    [Space]
    [SerializeField] private GameObject _buidlingsButtons;

    private bool _isOpen = false;

    public void OpenCloseStats()
    {
        if(_isOpen  == false)
        {
            _popupManager.ShowPopup(_buildingPopup);
            _buidlingsButtons.SetActive(false);
            _isOpen = true;
        }
        else
        {
            _popupManager.HidePopup(_buildingPopup);
            _buidlingsButtons.SetActive(true);
            _isOpen = false;
        }
        
    }
}
